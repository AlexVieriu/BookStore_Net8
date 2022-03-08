namespace BookStore.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepo;
    private readonly ILogger<AuthorController> _logger;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorRepository authorRepo,
                            ILogger<AuthorController> logger,
                            IMapper mapper)
    {
        _authorRepo = authorRepo;
        _logger = logger;
        _mapper = mapper;
    }

    // GET: api/Author/?startindex=0&pagesize=15
    [HttpGet]
    [Route("/api/AuthorsWithPg")]
    public async Task<ActionResult<VirtualizeResponse<AuthorReadDto>>> GetAuthorsWithPg([FromQuery] QueryParameters queryParams)
    {
        try
        {
            return await _authorRepo.GetAllWithPg<AuthorReadDto>(queryParams);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(GetAuthorsWithPg)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }

    // GET: api/authors
    [HttpGet()]
    [Route("/api/Authors")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<List<AuthorReadDto>>> GetAuthors()
    {
        try
        {
            return await _authorRepo.GetAllAsync<AuthorReadDto>();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(GetAuthors)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }


    // GET: api/author/5
    [HttpGet("{id:int}")]
    [Authorize(Roles = "User,Administrator")]
    public async Task<ActionResult<AuthorReadDto>> GetAuthor(int id)
    {
        try
        {
            if (id == 0)
                return BadRequest();

            var author = await _authorRepo.GetAuthorAsync(id);
            if (author == null)
                return NotFound();

            return author;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(GetAuthor)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }

    // POST: api/author
    [HttpPost()]
    public async Task<ActionResult<bool>> CreateAuthor([FromBody] AuthorCreateDto authorCreateDto)
    {
        try
        {
            if (authorCreateDto == null)
                return BadRequest(ModelState);

            var isCreated = await _authorRepo.CreateAsync(authorCreateDto);
            if (isCreated == false)
                return NotFound();

            return isCreated;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(CreateAuthor)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }

    // PUT: api/author
    [HttpPut()]
    public async Task<ActionResult<bool>> UpdateAuthor(AuthorUpdateDto authorCreateDto)
    {
        try
        {
            if (authorCreateDto == null || ModelState.IsValid == false)
                return BadRequest(ModelState);

            var isUpdated = await _authorRepo.UpdateAsync(authorCreateDto);
            if (isUpdated == false)
                return NotFound(ModelState);

            return isUpdated;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(UpdateAuthor)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }

    // DELETE: api/author/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteAuthor(int id)
    {
        try
        {
            if (id == null)
                BadRequest();

            var isDeleted = await _authorRepo.DeleteAsync(id);
            if (isDeleted == false)
                return NotFound(ModelState);

            return isDeleted;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(DeleteAuthor)} - {ErrorMsg.StatusCode500(ex)}");
            return StatusCode(500, ErrorMsg.StatusCode500(ex));
        }
    }
}
