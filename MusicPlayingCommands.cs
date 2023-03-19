using DSharpPlus;
using DSharpPlus.Lavalink;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
// Please save your eyes and don't watch it.
// Методы включения музыки по ссылке и без, и её останавливания. (опасно для зрения)
namespace DSBot
{
    public class MusicPlayer : BaseCommandModule
    {
        public async static Task _PlayWithURL(CommandContext ctx, Uri url)
        {
            await VoiceChannelCommands._Join(ctx);

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx!.Member!.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink не подключен.");
                return;
            }
            var loadResult = await node.Rest.GetTracksAsync(url);
            if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed 
                || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {
                await ctx.RespondAsync($"Ошибка поиска по: {url}.");
                return;
            }
            var track = loadResult.Tracks.First();  
            await conn.PlayAsync(track);

            await ctx.RespondAsync($"Сейчас играет: {track.Title}!");
        }
        public async static Task _Play(CommandContext ctx, [RemainingText] string search)
        {
            await VoiceChannelCommands._Join(ctx);

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx!.Member!.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink is not connected.");
                return;
            }

            var loadResult = await node.Rest.GetTracksAsync(search);
            if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed 
                || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {
                await ctx.RespondAsync($"Ошибка поиска: {search}.");
                return;
            }
            var track = loadResult.Tracks.First();  
            await conn.PlayAsync(track);

            await ctx.RespondAsync($"Сейчас играет: {track.Title}!");
        }

        public async static Task _Stop(CommandContext ctx)
        {
            if (ctx!.Member!.VoiceState == null || ctx.Member.VoiceState.Channel == null)
            {
                await ctx.RespondAsync("Ты не в голосовом канале.");
                return;
            }

            var lava = ctx.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(ctx.Member.VoiceState.Guild);

            if (conn == null)
            {
                await ctx.RespondAsync("Lavalink не подключен.");
                return;
            }

            if (conn.CurrentState.CurrentTrack == null)
            {
                await ctx.RespondAsync("Сейчас ничего не играет.");
                return;
            }

            await conn.StopAsync();
        }
    }
}