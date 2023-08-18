using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace SoundAdmin
{
    public partial class Form1 : Form
    {
        private UdpClient udpClient;
        private IPEndPoint endPoint;

        static int port = 12345;

       
        public Form1()
        {
            InitializeComponent();

            udpClient = new UdpClient(port);
            endPoint = new IPEndPoint(IPAddress.Any, 0);

            wplayer = new WMPLib.WindowsMediaPlayer();
            //
            Thread listenThread = new Thread(new ThreadStart(ListenForHorn));
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        WMPLib.WindowsMediaPlayer wplayer;

        private void ListenForHorn() {
            try
            {
                while (true)
                {
                    byte[] data = udpClient.Receive(ref endPoint);
                    string message = System.Text.Encoding.ASCII.GetString(data);

                    if (message == "PlaySound")
                    {
                        wplayer.controls.stop();
                        wplayer.URL = @"C:\Users\GIANGPHAN\source\repos\SoundAdmin\SoundAdmin\src\coihu.mp3";
                        wplayer.controls.play();
                    }
                    else if (message == "StopSound") {
                        wplayer.controls.stop();
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SSSS");
        }
    }
}
