using DSharpPlus;
using DSharpPlus.Lavalink;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

// Методы входа/выхода в голосовой канал
namespace DSBot
{
    public class VoiceChannelCommands : BaseCommandModule
    {
    public async static Task _Join(CommandContext ctx)
    {

        var lava = ctx.Client.GetLavalink();
        if (!lava.ConnectedNodes.Any())
        {
            await ctx.RespondAsync("Lavalink соединение не установлено");
            return;
        }

        var node = lava.ConnectedNodes.Values.First();

        if (ctx!.Member!.VoiceState.Channel.Type != ChannelType.Voice)
        {
            await ctx.RespondAsync("Не голосовой канал.");
            return;
        }

        await node.ConnectAsync(ctx!.Member!.VoiceState.Channel);
        await ctx.RespondAsync($"Joined {ctx.Member.VoiceState.Channel.Name}!");
    }

    public async static Task _Leave(CommandContext ctx)
    {
        var lava = ctx.Client.GetLavalink();
        if (!lava.ConnectedNodes.Any())
        {
            await ctx.RespondAsync("Lavalink соединение не установлено.");
            return;
        }

        var node = lava.ConnectedNodes.Values.First();

        if (ctx!.Member!.VoiceState.Channel.Type != ChannelType.Voice)
        {
            await ctx.RespondAsync("Не голосовой канал.");
            return;
        }

        var conn = node.GetGuildConnection(ctx!.Member!.VoiceState.Channel.Guild);

        if (conn == null)
        {
            await ctx.RespondAsync("Lavalink не подключен.");
            return;
        }

        await conn.DisconnectAsync();
        await ctx.RespondAsync($"Left {ctx!.Member!.VoiceState.Channel.Name}!");
    }
    }
}