using MassTransit.Configuration;
using Microsoft.AspNetCore.SignalR;

namespace HospitalManagementSystem.Infrastructure.SignalR;

public sealed class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
    }

    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");
    }
}
