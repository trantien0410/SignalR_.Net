using Microsoft.AspNetCore.SignalR;

namespace SignalRNetLab;

public class ChatHub : Hub
{
    // 1. Broadcast to all connected clients
    public async Task SendBroadcast(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", new
        {
            user,
            message,
            timestamp = DateTime.UtcNow.ToString("HH:mm:ss"),
            type = "broadcast"
        });
    }

    // 2. Join a specific room/group
    public async Task JoinRoom(string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        
        await Clients.Group(roomName).SendAsync("ReceiveMessage", new
        {
            user = "System",
            message = $"{Context.ConnectionId} joined room [{roomName}]",
            timestamp = DateTime.UtcNow.ToString("HH:mm:ss"),
            type = "system"
        });
    }

    // 3. Send message exclusively to a room/group
    public async Task SendToRoom(string roomName, string user, string message)
    {
        await Clients.Group(roomName).SendAsync("ReceiveMessage", new
        {
            user,
            message,
            roomName,
            timestamp = DateTime.UtcNow.ToString("HH:mm:ss"),
            type = "group"
        });
    }
}