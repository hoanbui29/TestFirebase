using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;

namespace TestFirebase.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("private-key.json"),
        });
    }

    [HttpPost("send-message")]
    public async Task SendMessageAsync(string message)
    {
        var msg = new Message
        {
            Topic = "NewMessage",
            Notification = new Notification
            {
                Title = "Send Message",
                Body = message
            }
        };
        var response = await FirebaseMessaging.DefaultInstance.SendAsync(msg);
    }
}