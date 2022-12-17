using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ModelsLibrary.Models;
using ServiceDeskLibrary.DataAccess;
using System.Net.Sockets;

namespace ServiceDeskApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketData _data;
    private readonly ILogger<TicketsController> _logger;

    public TicketsController(ITicketData data, ILogger<TicketsController> logger)
    {
        _data = data;
        _logger = logger;
    }

    #region GET
    // GET: api/Tickets/Get/5
    [HttpGet("Get/{ticketId}")]
    public async Task<ActionResult<Ticket>> Get(int ticketId)
    {
        _logger.LogInformation("GET: api/Tickets/Get/{TicketId}", ticketId);
        try
        {
            var output = await _data.GetOne(ticketId);

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The GET call to {ApiPath} failed. The Id was {TicketId}",
                "api/Tickets/Get/Id", ticketId);
            return BadRequest();
        }
    }

    // GET api/Tickets/Get/All
    [HttpGet("Get/All")]
    public async Task<ActionResult<Ticket>> GetAll()
    {
        _logger.LogInformation("GET: api/Tickets/Get/All");
        try
        {
            var output = await _data.GetAllNonArchived();
            return Ok(output);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "The GET call to {ApiPath} failed.", "api/Tickets/Get/All");
            return BadRequest();
        }
    }

    // GET api/Tickets/Get/All/04720e88-50d4-47eb-87e9-f88a320ddc66
    [HttpGet("Get/All/{userId}")]
    public async Task<ActionResult<Ticket>> GetAllByUser(string userId)
    {
        _logger.LogInformation("GET: api/Tickets/Get/All/{UserId}",userId);
        try
        {
            var output = await _data.GetAllNonArchivedByUser(userId);
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The GET call to {ApiPath} failed. The userId was {userId}",
                "api/Tickets/Get/All/UserId", userId);
            return BadRequest();
        }
    }

    // GET api/Tickets/Get/Archived
    [HttpGet("Get/All/Archived")]
    public async Task<ActionResult<Ticket>> GetAllArchived()
    {
        _logger.LogInformation("GET: api/Tickets/Get/All/Archived");
        try
        {
            var output = await _data.GetAllArchived();
            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The GET call to {ApiPath} failed.", "api/Tickets/Get/All/Archived");
            return BadRequest();
        }
    }
    #endregion

    #region POST
    // POST api/Tickets/Create
    [HttpPost("Create")]
    public async Task<ActionResult> Post([FromBody] Ticket ticket)
    {
        _logger.LogInformation("POST: api/Tickets/Create");
        try
        {
            await _data.Create(ticket.Subject!, ticket.Body!, ticket.AssignedId.ToString(), ticket.RequesterId.ToString(), "Id of creator");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The POST call to {ApiPath} failed.", "api/Tickets/Create");
            return BadRequest();
        }
    }
    #endregion

    #region PUT
    // PUT api/Tickets/Update/5
    [HttpPut("Update/{ticketId}")]
    public async Task<ActionResult<Ticket>> Update(int ticketId, [FromBody] Ticket ticket)
    {
        _logger.LogInformation($"POST: api/Tickets/Update/{ticketId}");
        try
        {
            await _data.UpdateTicket(ticket.Subject!, ticket.Body!, ticket.AssignedId.ToString(),
                ticket.Status!, ticket.RequesterId.ToString(), ticket.Id);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The PUT call to {ApiPath} failed " + "when updating ticket ID {ticketId}",
                "api/Tickets/Update/{ticketId}");
            return BadRequest();
        }
    }

    // PUT api/Tickets/Update/5/Archive
    [HttpPut("Update/{ticketId}/Archive")]
    public async Task<ActionResult> Archive(int ticketId, [FromBody] bool archived)
    {
        _logger.LogInformation("POST: api/Tickets/{TicketId}/Archive", ticketId);
        try
        {
            await _data.ArchiveTicket(archived, ticketId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The PUT call to {ApiPath} failed."+$" Setting archive status to {archived} of ticket {ticketId}",
                $"api/Tickets/{ticketId}/Archive");
            return BadRequest();
        }
    }
    #endregion

    #region DELETE
    // DELETE api/Tickets/Delete/5
    [HttpDelete("Delete/{id}")]
    public async Task<ActionResult> Delete(int ticketId)
    {
        _logger.LogInformation("POST: api/Tickets/Delete");
        try
        {
            await _data.DeleteTicket(ticketId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The DELETE call to {ApiPath} failed. Failed trying to delete ticket {TicketId}",
                "api/Tickets/Archive", ticketId);
            return BadRequest();
        }
    }
    #endregion
}
