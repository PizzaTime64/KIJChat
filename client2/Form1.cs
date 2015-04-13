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
using System.Threading;

namespace KIJChat
{
    public partial class Form1 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        bool isConnected = false;
        public Form1()
        {
            InitializeComponent();
            txtInput.ReadOnly = true;
            btnDiscon.Enabled = false;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = null;
            message = "message " + txtUsername.Text + " " + (string)listOnlineUser.SelectedItem + " " + txtInput.Text;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(message);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            rtxtDisplay.Text = rtxtDisplay.Text + Environment.NewLine + " << : " + (string)listOnlineUser.SelectedItem +" " + txtInput.Text;
            txtInput.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            clientSocket.Connect("192.168.1.111", 8888);
            serverStream = clientSocket.GetStream();

            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("login "+txtUsername.Text + "\0");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();      

            Thread ctThread = new Thread(getMessage);
            ctThread.Start();

            Thread ctThread1 = new Thread(sendPing);
            ctThread1.Start();

            isConnected = true;
            txtUsername.ReadOnly = true;
            btnConnect.Enabled = false;
            btnDiscon.Enabled = true;
        }
        private void sendPing()
        {
            while (true)
            {
                if (isConnected == false) return;
                byte[] outStream1 = System.Text.Encoding.ASCII.GetBytes("ping " + txtUsername.Text);
                serverStream.Write(outStream1, 0, outStream1.Length);
                serverStream.Flush();
                Thread.Sleep(3000);
                
                if (this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.listOnlineUser.Items.Clear();
                    });
                }
                
            }
            
        }
        private void getMessage()
        {
            while (true)
            {
                if (isConnected == false) return;
                serverStream = clientSocket.GetStream();
                int buffersize = 0;
                byte[] inStream = new byte[1000000];
                buffersize = clientSocket.ReceiveBufferSize;
                serverStream.Read(inStream, 0, buffersize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                readData = "" + returndata.Trim();
                string[] command = readData.Split(' ');

               
                if (command[0] == "online")
                {
                    for (int i = 1; i < command.Length; i++ )
                    {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                if(command[i] != txtUsername.Text)
                                    this.listOnlineUser.Items.Add(command[i]);
                            });
                        }
                    }
                }
                
                if (command[1].Trim() == "ok")
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.txtInput.ReadOnly = false;
                    });
                }

                if (command[0] == "message")
                {
                    if (command.Length < 3)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            this.rtxtDisplay.Text = this.rtxtDisplay.Text + Environment.NewLine + " >> " + command[0] + " " + command[1];
                        });
                        continue;
                    }
                    string output = string.Join(" ", command, 3, command.Length - 3);
                    output = command[1] + ": " + output;
                    this.Invoke((MethodInvoker)delegate
                    {
                        this.rtxtDisplay.Text = this.rtxtDisplay.Text + Environment.NewLine + " >> " + output;
                    });
                    
                }

            }
        }

        private void btnDiscon_Click(object sender, EventArgs e)
        {
            clientSocket.Close();
            txtInput.ReadOnly = true;
            btnSend.Enabled = false;
            txtUsername.ReadOnly = false;
            btnConnect.Enabled = true;
            btnDiscon.Enabled = false;
            isConnected = false;
        }
        
       
       
    }
}
