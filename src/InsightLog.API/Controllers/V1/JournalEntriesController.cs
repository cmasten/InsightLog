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

    /// <summary>
    /// Edits an existing journal entry.
    /// </summary>
    /// <param name="id">The ID of the journal entry.</param>
    /// <param name="command">Updated values.</param>
    /// <returns>The updated journal entry.</returns>
    /// <response code="200">Returns the updated journal entry</response>
    /// <response code="404">If the entry does not exist</response>
    /// <response code="400">If validation fails</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(JournalEntryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JournalEntryDto>> Edit([FromRoute] Guid id, [FromBody] EditJournalEntry.Command command)
    {
        if (command is null)
        {
            return BadRequest();
        }

        var fullCommand = command with { Id = new JournalEntryId(id) };

        try
        {
            var result = await mediator.Send(fullCommand);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
