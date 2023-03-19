using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Net;
using DSharpPlus.Lavalink;

//Основной файл
namespace DSBot
{
    class Program : Settings
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        // Основная функция

        internal static async Task MainAsync(string[] args)
        {
            var discord = new DiscordClient(new DiscordConfiguration(){Token = token, TokenType = TokenType.Bot, Intents = intents});
            
            var endpoint = new ConnectionEndpoint{Hostname = "127.0.0.1", Port = 2333};
            var lavalinkConfig = new LavalinkConfiguration{Password = "youshallnotpass", RestEndpoint = endpoint, SocketEndpoint = endpoint};
            var lavalink = discord.UseLavalink();

            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            { 
                StringPrefixes = new[] { "!" }
            });

            commands.RegisterCommands<CommandsModule>();

            await discord.ConnectAsync();  
            await lavalink.ConnectAsync(lavalinkConfig);        
            await Task.Delay(-1);
        }
        
    }
}