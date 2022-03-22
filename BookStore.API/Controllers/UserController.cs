namespace BookStore.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _config;
    private readonly UserManager<ApiUser> _userManager;

    public UserController(IMapper mapper,
                          ILogger<UserController> logger,
                          IConfiguration config,
                          UserManager<ApiUser> userManager)
    {
        _mapper = mapper;
        _logger = logger;
        _config = config;
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserReponse>> Login([FromBody] UserLogin login)
    {
        try
        {
            if (login == null || ModelState.IsValid == false)
                return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(login.UserName);
            var passValid = await _userManager.CheckPasswordAsync(user, login.Password);

            if (user == null || passValid == false)
                return Unauthorized();

            string tokenString = await GenerateJWT(user);

            var response = new UserReponse()
            {
                UserId = user.Id,
                UserName = login.UserName,
                Token = tokenString
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(Login)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }

    [HttpPost("register")]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult<bool>> Register([FromBody] UserRegister register)
    {
        try
        {
            if (register == null || ModelState.IsValid == false)
                return BadRequest();

            var user = _mapper.Map<ApiUser>(register);
            var isCreated = await _userManager.CreateAsync(user, register.Password);
            if (isCreated.Succeeded == false)
            {
                foreach (var error in isCreated.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRoleAsync(user, register.Role);

            return isCreated.Succeeded;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(Register)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }

    private async Task<string> GenerateJWT(ApiUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        var credencials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var userClaims = await _userManager.GetClaimsAsync(user);

        var role = await _userManager.GetRolesAsync(user);
        var roleClaims = role.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }
        .Union(userClaims)
        .Union(roleClaims);

        var token = new JwtSecurityToken
            (
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credencials
            );

        var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenHandler;
    }
}
