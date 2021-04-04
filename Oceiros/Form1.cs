using Oceiros.Bot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using RedditSharp.Things;

namespace Oceiros
{
    public partial class Form1 : Form
    {
        public static Base Base = new Base();
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            Base.PropertyChanged += Base_PropertyChanged;
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            Base.StartBot();   
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            Base.StopBot();
        }
        
        private void Base_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            StatusBox.Text = Base.Status;
        }

        private async void DonwloadBTN_Click(object sender, EventArgs e)
        {
            Webclient web = new Webclient();
            await web.DownloadFile(DownloadUrl.Text, "testing", "mp4");
        }
    }
}

