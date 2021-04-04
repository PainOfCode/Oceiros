using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oceiros.Bot
{
    public class Database
    {
        private static MySqlConnection MySqlConnection = new MySqlConnection("Server = localhost; Uid =  root; Pwd=; Port=3306; Database = reddit");
        private static string Table = "subscribedreddit";

        public ulong GID;
        public ulong CID;
        public string SubName;
        public string LastPostID = null;
        public int ID;
        
        public Database()
        {

        }
        public Database(string Subreddit, ulong Guild, ulong Channel)
        {
            GID = Guild;
            CID = Channel;
            SubName = Subreddit;
        }
        public Database(string Subreddit, ulong Guild, ulong Channel, string LastID)
        {
            GID = Guild;
            CID = Channel;
            SubName = Subreddit;
            LastPostID = LastID;
        }
        public Database(string Subreddit, ulong Guild, ulong Channel, string LastID, int DID)
        {
            GID = Guild;
            CID = Channel;
            SubName = Subreddit;
            LastPostID = LastID;
            ID = DID;
        }
        public async Task Save()
        {
            MySqlCommand Command = new MySqlCommand($"insert into {Table} (gid, redditname, cid, lastid) VALUES ({GID}, '{SubName}' , {CID}, '{LastPostID}')",MySqlConnection);
            try
            {
                MySqlConnection.Open();
                Command.ExecuteNonQuery();
                MySqlConnection.Close();
            } catch (Exception e)
            {
                MySqlConnection.Close();
                MessageBox.Show(e.ToString());
            }
        }
        
        public async Task<List<Database>> GetEverything()
        {
            List<Database> Everything = new List<Database>();

            MySqlCommand Command = new MySqlCommand("select * from subscribedreddit;");

            try
            {
                MySqlConnection.Open();
                MySqlDataReader Reader = Command.ExecuteReader();

                while (Reader.Read() != false)
                {
                    if (Reader.GetString("id") == null)
                    {
                        break;
                    }

                    string Subreddit = (string)Reader.GetString("redditname");
                    ulong Guild = Convert.ToUInt64(Reader.GetString("gid"));
                    ulong Channel = Convert.ToUInt64(Reader.GetString("cid")); ;
                    string LastID = Reader.GetString("lastid");
                    int DID = Convert.ToInt32(Reader.GetString("id")); ;

                    Database CurData = new Database(Subreddit, Guild, Channel, LastID, DID);
                    Everything.Add(CurData);

                    Reader.Read();
                }

                MySqlConnection.Close();
            }
            catch (Exception e)
            {
                MySqlConnection.Close();
            }

            return Everything;
        }
        public async Task GetID()
        {
            MySqlCommand Command = new MySqlCommand($"select * from {Table} where (gid = {GID} AND cid = {CID} AND redditname = '{SubName}')", MySqlConnection);

            try
            {
                MySqlConnection.Open();

                MySqlDataReader Reader = Command.ExecuteReader();
                Reader.Read();

                ID = Convert.ToInt32(Reader.GetString("id"));

                MySqlConnection.Close();
            }
            catch (Exception e)
            {
                MySqlConnection.Close();
                MessageBox.Show(e.ToString());
            }
        }
        public async Task UpdateLastID(string LastID, int ID)
        {
            MySqlCommand Command = new MySqlCommand($"update {Table} SET lastid = '{LastID}' where id = {ID}", MySqlConnection);
            
            MySqlConnection.Open();
            Command.ExecuteNonQuery();
            MySqlConnection.Close();
        }
        public async Task<List<Database>> GetDataForGuild(ulong Guild)
        {
            List<Database> GuildData = new List<Database>();
            MySqlCommand Command = new MySqlCommand($"select * from {Table} where (gid = {Guild})", MySqlConnection);
            Database CurGuild;
            try
            {
                MySqlConnection.Open();

                MySqlDataReader Reader = Command.ExecuteReader();
                while (Reader.Read())
                {
                    try
                    {
                        var Subreddit = Convert.ToString(Reader.GetString("redditname"));
                        var ChannelID = Convert.ToUInt64(Reader.GetString("CID"));
                        var GuildID = Convert.ToUInt64(Reader.GetString("GID"));
                        string Last = Convert.ToString(Reader.GetString("lastid"));
                        int Did = Convert.ToInt32(Reader.GetString("id"));

                    
                        CurGuild = new Database(Subreddit,GuildID, ChannelID, Last, Did);
                        GuildData.Add(CurGuild);
                    }catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    
                }

                MySqlConnection.Close();
            }
            catch (Exception e)
            {
                MySqlConnection.Close();
                MessageBox.Show(e.ToString());
            }
            return GuildData;
        }

        
    }
}
