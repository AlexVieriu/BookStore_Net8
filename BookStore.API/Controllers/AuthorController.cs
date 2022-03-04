namespace BookStore.API.Controllers;
[Route("api/[controller]")]
[ApiController]
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
    [HttpGet()]
    public async Task<ActionResult<VirtualizeResponse<AuthorReadDto>>> GetAuthorsWithPg(int id)
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    // GET: api/authors
    [HttpGet("authors")]
    public async Task<ActionResult<List<AuthorReadDto>>> GetAuthors()
    {
        try
        {
            var authors = await _authorRepo.GetAllAsync();
            var authorsDto = _mapper.Map<List<AuthorReadDto>>(authors);

            return authorsDto;

        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(GetAuthors)} - {ErrorMsg.StatusCode500}");
            return StatusCode(500, ErrorMsg.StatusCode500);
        }
    }


    // GET: api/author/5
    [HttpGet("{id:int}")]
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
            _logger.LogError($"Error at {nameof(GetAuthor)} - {ErrorMsg.StatusCode500}");
            return StatusCode(500, ErrorMsg.StatusCode500);
        }
    }

    // POST: api/author
    [HttpPost()]
    public async Task<ActionResult<bool>> CreateAuthor([FromBody] AuthorCreateDto authorCreateDto)
    {
        try
        {
            if(authorCreateDto == null)
                return BadRequest(ModelState);

            var author = _mapper.Map<Author>(authorCreateDto);
            var isCreated = await _authorRepo.CreateAsync(author);
            if (isCreated == false)
                return NotFound();

            return isCreated;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error at {nameof(CreateAuthor)} - {ErrorMsg.StatusCode500}");
            return StatusCode(500, ErrorMsg.StatusCode500);
        }
    }

    // PUT: api/author
    [HttpPut()]
    public async Task<IActionResult> UpdateAuthor(AuthorUpdateDto authorCreateDto)
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    // DELETE: api/author/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
