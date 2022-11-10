﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ModelsLibrary.Models;
using ServiceDeskLibrary.DataAccess;

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
    public async Task<ActionResult<TicketModel>> Get(int ticketId)
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
    public async Task<ActionResult<TicketModel>> GetAll()
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
    public async Task<ActionResult<TicketModel>> GetAllByUser(string userId)
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
    public async Task<ActionResult<TicketModel>> GetAllArchived()
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
    public void Post([FromBody] string value)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region PUT
    // PUT api/Tickets/Update/5
    [HttpPut("Update/{ticketId}")]
    public void Update(int ticketId, [FromBody] string value)
    {
        throw new NotImplementedException();
    }

    // PUT api/Tickets/Update/5/Archive
    [HttpPut("Update/{ticketId}/Archive")]
    public void Archive(int ticketId, [FromBody] string value)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region DELETE
    // DELETE api/Tickets/Delete/5
    [HttpDelete("Delete/{id}")]
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
    #endregion
}
