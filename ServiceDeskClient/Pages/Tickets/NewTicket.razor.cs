using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using ModelsLibrary.Models;
using MudBlazor;
using ServiceDeskClient.Pages.Tickets.Components;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ServiceDeskClient.Pages.Tickets;

public partial class NewTicket
{
    private Ticket ticket = new();
    private TextEditorComponent? textEditor;
    private MudForm? form;
    private List<string> statuses;
    private List<User> requesters, technicians;
    private User requester;
    private User technician;
    [Inject] ISnackbar Snackbar { get; set; }
    [Inject] IHttpClientFactory _factory { get; set; }
    [Inject] AuthenticationStateProvider _authenticationStateProvider { get; set; }
    private HttpClient? client;
    private HttpContextAccessor _httpContextAccessor = new();

    protected override void OnInitialized()
    {
        
        statuses = new();
        requesters = new();
        technicians = new();
        for (int i = 0; i < 10; i++)
        {
            User req = new() { Id = i, FirstName = $"Name{i}", LastName = "LastName" };
            requesters.Add(req);
            technicians.Add(req);
        }
        statuses.Add("Open");
        statuses.Add("In Progress");
        statuses.Add("Cancelled");
        statuses.Add("Closed");
    }

    private async void HandleValidSubmit()
    {
        ticket.Body = await textEditor.GetHTML();
    }

    private async Task<IEnumerable<string>> SearchStatus(string value)
    {
        if (string.IsNullOrEmpty(value))
            return statuses;
        return statuses.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private async Task<IEnumerable<User>> SearchRequester(string value)
    {
        if (string.IsNullOrEmpty(value))
            return requesters;
        return requesters.Where(x => x.Name().Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private async Task<IEnumerable<User>> SearchTechnician(string value)
    {
        if (string.IsNullOrEmpty(value))
            return technicians;
        return technicians.Where(x => x.Name().Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    private async Task Submit()
    {
        ticket.Body = await textEditor.GetHTML();
        StateHasChanged();
        await form.Validate();

        if (form.IsValid)
        {

            string bearerToken = _httpContextAccessor.HttpContext!.Request.Cookies["apiBearerToken"]!;
            client = _factory.CreateClient("api");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            
            var claimsIdentity = (ClaimsIdentity) _authenticationStateProvider.GetAuthenticationStateAsync().Result.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ticket.CreateId = claim.Value;
            try
            {
                var result = await client!.PostAsJsonAsync<Ticket>("Tickets/Create", ticket);
            }
            catch (Exception ex)
            {
            }

            await InvokeAsync(StateHasChanged);
        }
    }
}
