using Discord.Commands;
using Discord.WebSocket;
using Gfycat;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Crmf;
using RedditSharp;
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oceiros.Bot
{
    public class Base : INotifyPropertyChanged
    {
        private static String Username = "placeholder"; 
        private static String Password = "123";
        private string StatusValue = "offline";
        public event PropertyChangedEventHandler PropertyChanged;
        public static DiscordSocketClient _client = new DiscordSocketClient(new DiscordSocketConfig());
        public static Reddit reddit = new Reddit(Username, Password);
        private static CommandService _commands = new CommandService();
        
        
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task StartBot()
        {
            await _client.LoginAsync(Discord.TokenType.Bot, "");
            await _client.SetGameAsync("Reddit", null, Discord.ActivityType.Watching);
            await _client.StartAsync();
            this.Status = "Online";
            
        }

        public async Task StopBot()
        {
            await _client.LogoutAsync();
            await _client.StopAsync();
            this.Status = "Offline";
        }

        public Base()
        {
            _client.MessageReceived += _client_MessageReceived;
            var services = new ServiceCollection();
            CommandSetup();
            
        }

        public Base(string yeet)
        {

        }

        private async Task _client_MessageReceived(SocketMessage Msg)
        {
            var Message = Msg as SocketUserMessage;

            if (Message == null) return;

            int ArgPos = 0;

            if (!Message.Author.IsBot && (!Message.HasCharPrefix('!', ref ArgPos) || !Message.HasMentionPrefix(_client.CurrentUser, ref ArgPos)))
            {
                var context = new SocketCommandContext(_client, Message);

                await _commands.ExecuteAsync(context: context, argPos: ArgPos, services: null);
            }
        }

        private async Task CommandSetup()
        {
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        public string Status
        {
            get { return this.StatusValue; }
            set
            {
                if (value != this.StatusValue)
                {
                    this.StatusValue = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}