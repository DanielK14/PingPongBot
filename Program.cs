using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Configuration;

namespace PingPongBot
{
    public class Program
    {
        private DiscordSocketClient _client;
        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot,
                Environment.GetEnvironmentVariable("DiscordToken"));
            await _client.StartAsync();

            _client.MessageReceived += async e =>
            {
                if (e.Content.ToLower().StartsWith("ping"))
                {
                    ulong id = ulong.Parse(Environment.GetEnvironmentVariable("DiscordChannel"));
                    var chnl = _client.GetChannel(id) as IMessageChannel;
                    await chnl.SendMessageAsync("pong");
                }

            };
            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
