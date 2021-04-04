using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using RedditSharp.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oceiros.Bot.Commands
{
    public class Redoot : ModuleBase<SocketCommandContext>
    {

        [Command("add")]
        [Summary("Adds a subreddit, usage: !add r/aww or !add r/aww [Channel name]")]
        public async Task AddSubreddit(string Subreddit)
        {
            var GuildChannel = Context.Guild.Channels;
            Database Db = new Database();
            var AllGChannel = await Db.GetDataForGuild(Context.Guild.Id);
            bool NewChannelNeeded = true;
            Subreddit = await CutSubredditName(Subreddit);

            if (AllGChannel != null || AllGChannel != new List<Database>())

                foreach (SocketGuildChannel Channel in GuildChannel)
                {
                    if (Channel == null)
                        break;

                    foreach (Database GChannel in AllGChannel)
                    {
                        if (GChannel == null)
                            break;
                        if ((ulong)GChannel.CID == Channel.Id && GChannel.SubName.ToLower() == Subreddit.ToLower())
                        {
                            NewChannelNeeded = false;
                        }
                    }
                }
            if (!NewChannelNeeded)
            {
                await Context.Channel.SendMessageAsync($"Community {Subreddit} has already been added");
            }
            else
            {
                var NewChannel = await Context.Guild.CreateTextChannelAsync($"{Subreddit}");
                Db = new Database(Subreddit, Context.Guild.Id, NewChannel.Id, null);
                await Db.Save();
                await Db.GetID();
                await Context.Channel.SendMessageAsync($"{Subreddit} has been created in it's own channel [{NewChannel.Mention}]");
                await FirstStock(Subreddit, NewChannel, Db);
            }
        }

        [Command("add")]
        [Summary("Adds a subreddit to a collection, usage: !add r/aww [Channel name]")]
        public async Task AddSubreddit(string Subreddit, string ChannelName)
        {
            var GuildChannel = Context.Guild.Channels;
            Database Db = new Database();
            var AllGChannel = await Db.GetDataForGuild(Context.Guild.Id);
            bool NewChannelNeeded = true;
            bool SubAlreadyAdded = false;
            ulong AddToChannel = 0;
            Subreddit = await CutSubredditName(Subreddit);

            if (AllGChannel != null || AllGChannel != new List<Database>())

                foreach (SocketGuildChannel Channel in GuildChannel)
                {
                    if (Channel == null)
                        break;

                    foreach (Database GChannel in AllGChannel)
                    {
                        if (GChannel == null)
                            break;

                        if (GChannel.CID == Channel.Id)
                        {
                            NewChannelNeeded = false;

                            if (GChannel.SubName.ToLower() == Subreddit.ToLower())
                                SubAlreadyAdded = true;
                            else
                                AddToChannel = Channel.Id;
                        }
                    }
                }
            if (!NewChannelNeeded)
            {
                if (SubAlreadyAdded)
                    await Context.Channel.SendMessageAsync($"Community {Subreddit} has already been added to {ChannelName}");
                else
                {
                    var GChannel = Context.Guild.GetTextChannel(AddToChannel);
                    Db = new Database(Subreddit, Context.Guild.Id, AddToChannel);
                    await Db.Save();
                    await Context.Channel.SendMessageAsync($"{Subreddit} has been added to [{GChannel.Mention}]");
                }
            }
            else
            {
                var NewChannel = await Context.Guild.CreateTextChannelAsync($"{ChannelName}");
                Db = new Database(Subreddit, Context.Guild.Id, NewChannel.Id, null);
                await Db.Save();
                await Context.Channel.SendMessageAsync($"{Subreddit} has been added to channel [{NewChannel.Mention}]");
            }
        }

        private async Task<string> CutSubredditName(string LongName)
        {
            int ISub = 0;

            if (LongName.IndexOf("/r/") != -1)
                ISub = 3;
            if (LongName.IndexOf("r/") != -1)
                ISub = 2;

            LongName.Remove(0, ISub);
            return LongName;
        }

        private async Task FirstStock(string Subreddit, RestTextChannel Channel, Database Db)
        {
            SubredditFetcher Fetcher = new SubredditFetcher();
            var Posts = await Fetcher.GetNew("/r/" + Subreddit);
            Webclient webclient = new Webclient();
            await Db.UpdateLastID(Posts.First().Id, Db.ID);

            foreach (Post Post in Posts)
            {
                if (Post == null)
                    break;

                var Length = Post.Url.ToString().Length;
                
                string PicType = "";
                PicType += Post.Url.ToString()[Length - 3];
                PicType += Post.Url.ToString()[Length - 2];
                PicType += Post.Url.ToString()[Length - 1];
                
                if (Fetcher.CheckType(PicType))
                {
                    var File = await webclient.DownloadFile(Post.Url.ToString(), Post.Id.ToString(), PicType);
                    try
                    {
                        await Channel.SendFileAsync(File, Post.Title);
                    }
                    catch (Exception e)
                    {
                    }
                }
            }

            await webclient.DeleteDir();
        }
    }
}