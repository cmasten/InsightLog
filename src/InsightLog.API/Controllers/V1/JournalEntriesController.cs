namespace InsightLog.API.Controllers.V1;

/// <summary>
/// Handles operations related to user journal entries.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Produces("application/json")]
[Consumes("application/json")]
public class JournalEntriesController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Retrieves all journal entries for the specified user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A list of journal entries.</returns>
    /// <response code="200">Returns the user's journal entries</response>
    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(List<JournalEntryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<JournalEntryDto>>> GetByUser([FromRoute] Guid userId)
    {
        var result = await mediator.Send(new GetJournalEntries.Query(new UserId(userId)));
        return Ok(result);
    }

    /// <summary>
    /// Retrieves a journal entry by its identifier.
    /// </summary>
    /// <param name="id">The journal entry ID.</param>
    /// <returns>The matching journal entry.</returns>
    /// <response code="200">Returns the journal entry</response>
    /// <response code="404">If no journal entry is found</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(JournalEntryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JournalEntryDto>> GetById([FromRoute] Guid id)
    {
        var result = await mediator.Send(new GetJournalEntryById.Query(id));
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new journal entry.
    /// </summary>
    /// <param name="command">The journal entry creation details.</param>
    /// <returns>The created journal entry.</returns>
    /// <response code="200">Returns the created journal entry</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost]
    [ProducesResponseType(typeof(JournalEntryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JournalEntryDto>> Create([FromBody] CreateJournalEntry.Command command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
