using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Oceiros.Bot
{
    public class Webclient
    {
        public static WebClient WebClient = new WebClient();
        public async Task DeleteDir()
        {
            try
            {
                string[] Files = Directory.GetFiles("./pics");
                foreach (string file in Files)
                {
                    File.Delete(file);    
                }
            }catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            
        }
        public async Task<string> DownloadFile(string URL, string FileName, string FileType)
        {
            if (!CheckDirectory("pics"))
            {
                Directory.CreateDirectory("pics");
            }
            while (WebClient.IsBusy) { };
            WebClient.DownloadFile(URL, $"./pics/{FileName}.{FileType}");

            return $"./pics/{FileName}.{FileType}";
        }

        private static bool CheckDirectory(string DirName)
        {
            var AllDirs = Directory.GetDirectories("./");
            var FullDirName = $"./{DirName}";
            foreach (var Dir in AllDirs)
            {
                if (Dir == null)
                    break;

                if (Dir.ToString() == FullDirName)
                    return true;
            }
            return false;
        }
    }
}
