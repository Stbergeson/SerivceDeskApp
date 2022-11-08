using Microsoft.AspNetCore.Mvc;

namespace ServiceDeskApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    #region GET
    // GET: api/Tickets/Get/5
    [HttpGet("Get/{ticketId}")]
    public IEnumerable<string> Get(int ticketId)
    {
        throw new NotImplementedException();
    }

    // GET api/Tickets/Get/All
    [HttpGet("Get/All")]
    public string GetAll()
    {
        throw new NotImplementedException();
    }

    // GET api/Tickets/Get/All/04720e88-50d4-47eb-87e9-f88a320ddc66
    [HttpGet("Get/All/{userId}")]
    public string GetAllByUser(string userId)
    {
        throw new NotImplementedException();
    }

    // GET api/Tickets/Get/Archived
    [HttpGet("Get/All/Archived")]
    public string GetAllArchived()
    {
        throw new NotImplementedException();
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
