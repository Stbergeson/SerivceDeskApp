using Microsoft.AspNetCore.Components;
using ModelsLibrary.Models;
using MudBlazor;
using ServiceDeskClient.Pages.Tickets.Components;

namespace ServiceDeskClient.Pages.Tickets;

public partial class NewTicket
{
    private Ticket ticket = new();
    private TextEditorComponent? textEditor;
    private string[] errors = { };
    private MudForm? form;
    private List<string> statuses;
    private List<User> requesters, technicians;
    private User requester;
    [Inject] ISnackbar Snackbar { get; set; }

    protected override void OnInitialized()
    {
        requester = new() { Id = -1 };
        statuses = new();
        requesters = new();
        for (int i = 0; i < 10; i++)
        {
            User req = new() { Id = i, FirstName = $"Name{i}", LastName = "LastName" };
            requesters.Add(req);
        }
        technicians = new();
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

    private async Task Submit()
    {
        ticket.Body = await textEditor.GetHTML();
        StateHasChanged();
        await form.Validate();

        if (form.IsValid)
        {
            Snackbar.Add("Submited!");
        }
    }
}
