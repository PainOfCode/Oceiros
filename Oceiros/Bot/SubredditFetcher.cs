using Discord.WebSocket;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RedditSharp;
using RedditSharp.Things;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Oceiros.Bot
{
    public class SubredditFetcher
    {
        private Reddit reddit = Base.reddit;
        private DiscordSocketClient _client = Base._client;
        private class ValidTypes : IEnumerable
        {
            private string[] types = { "gif", "jpg", "png", "mp4" };

            public IEnumerator GetEnumerator()
            {
                for (int index = 0; index < types.Length; index++)
                {
                    yield return types[index];
                }
            }
        }

        public async Task<List<Post>> GetNew(string SubName)
        {
            var Sub = reddit.GetSubreddit(SubName);
            var Posts = Sub.New.Take(30).ToList();

            return Posts;
        }
        public bool CheckType(string Type)
        {
            ValidTypes validTypes = new ValidTypes();
            foreach (var AllTypes in validTypes)
            {
                if (AllTypes == null)
                    break;

                if (AllTypes.ToString() == Type.ToString())
                    return true;
            }

            return false;
        }

        public async Task RestockContent()
        {
            
        }


    }
}
