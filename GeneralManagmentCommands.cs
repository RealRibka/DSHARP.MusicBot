using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Lavalink;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

// Общий каталог всех методов
namespace DSBot
{
public class CommandsModule : BaseCommandModule
{
    [Command("hello")]
    public async Task HelloCommand(CommandContext ctx)
    {
        await ctx.RespondAsync("Hi Бро. Че как? Тут все по дефолтычу - join, leave, play, stop,а префикс '!'. Но а тебе надо ли больше?");
    }

    [Command("join")]
    public async Task JoinCommand(CommandContext ctx)
    {
        await VoiceChannelCommands._Join(ctx);
    }

    [Command("leave")]
    public async Task LeaveCommand(CommandContext ctx)
    {
        await VoiceChannelCommands._Leave(ctx);
    }

    [Command("play")]
    public async Task PlayCommand(CommandContext ctx, [RemainingText] string search)
    {
        if (search.StartsWith("http"))
        {
            System.Uri uri = new System.Uri(search);
            await MusicPlayer._PlayWithURL(ctx, uri);
        }
        else
        {
            await MusicPlayer._Play(ctx, search);
        }
    }
    
    [Command("stop")]
    public async Task StopCommand(CommandContext ctx)
    {
        await MusicPlayer._Stop(ctx);
    }
}
}
