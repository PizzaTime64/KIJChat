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
using System.IO;
using KIJChat;
using System.Collections;


namespace KIJChat
{
    public partial class Form1 : Form
    {
        System.Net.Sockets.TcpClient clientSocket;
        NetworkStream serverStream = default(NetworkStream);
        bool isConnected = false;
        string username = null;
        Thread ctThread = null;
        Thread ctThread1 = null;
        string ServerAddress = null;
        string ServerPort = null;

        public Form1()
        {
            InitializeComponent();
            pnlHome.Show();

            foreach (string line in File.ReadLines(@"KIJChat.conf"))
            {
                string[] words = line.Split(' ');
                if (words[0] == "ServerAddress") ServerAddress = words[1];
                if (words[0] == "ServerPort") ServerPort = words[1];
            }
           
        }

        //buttton send
        private void button2_Click(object sender, EventArgs e)  
        {
            if (isConnected == false)
            {
                clientSocket = new System.Net.Sockets.TcpClient();
                clientSocket.Connect(ServerAddress, int.Parse(ServerPort));
                serverStream = clientSocket.GetStream();
                isConnected = true;

                Thread ctThread = new Thread(getMessage);
                ctThread.Start();
            }

            string plaintext = txtInput.Text;
            string iv = RandomUtil.GetRandomString();
            string key = "bbbbaaaa";
            BitArray chiperbit = DES.ofbDES(DES.strToBit(iv), DES.strToBit(key), DES.strToBit(plaintext));
            string chipertext = DES.bitToHexa(chiperbit);
            //writeDisplay("chipertext = " + chipertext +".");
            string message = "message " + username.Trim() + " " + ((string)listOnlineUser.SelectedItem).Trim() + " chiper " + chipertext + " " + iv;
            //writeDisplay("pesan terenkripsi = " + message);
            Console.WriteLine("sebelum enkripsi = " + plaintext);
            Console.WriteLine("sesudah enkripsi = " + chipertext);
            //message <usersumber> <usertujuan> <chiper/key> <chiper: chipertext vi; key: key>
            /*/message = txtInput.Text;
            
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(txtInput.Text);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();*/
            sendToServer(message);
            writeDisplay("<<" + txtInput.Text);
            //writeDisplay(" << " + (string)listOnlineUser.SelectedItem +" : " + txtInput.Text);
            txtInput.Text = "";
        }

        //Button Connect
        private void btnConnect_Click(object sender, EventArgs e) 
        {
            try
            {
                if (isConnected == false)
                {
                    clientSocket = new System.Net.Sockets.TcpClient();
                    clientSocket.Connect(ServerAddress, int.Parse(ServerPort));
                    serverStream = clientSocket.GetStream();
                    isConnected = true;
                }
                sendToServer("login " + txtUsernameLogin.Text.Trim() + " " + txtPasswordLogin.Text +"\0");
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Could not connect to server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Wrong IP or Port format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string data = recvServer();
            string[] command = data.Split(' ');

            if (command[1] == "ok")
            {
                username = txtUsernameLogin.Text;// Trim();
                pnlHome.Hide();

                ctThread = new Thread(getMessage);
                ctThread1 = new Thread(sendPing);

                ctThread.Start();
                ctThread1.Start();
            }
            else if (command[1] == "failed")
            {
                MessageBox.Show("Wrong username or password", "Wrong!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (txtConfirmPasswordSignup.Text != txtPasswordSignup.Text)
            {
                MessageBox.Show("Password must be match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (isConnected == false)
                {
                    clientSocket = new System.Net.Sockets.TcpClient();
                    clientSocket.Connect(ServerAddress, int.Parse(ServerPort));
                    serverStream = clientSocket.GetStream();
                    isConnected = true;
                }
                sendToServer("signup " + txtUsernameSignup.Text + " " + txtPasswordSignup.Text + "\0");
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Could not connect to server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Wrong IP or Port format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string data = recvServer();
            string[] command = data.Split(' ');

            if (command[1] == "success")
            {
                MessageBox.Show("Signup Success\nPlease Login", "Signup Success", MessageBoxButtons.OK);
            }
            else if (command[1] == "failed")
            {
                MessageBox.Show("Signup Failed\nExsist Username", "Signup Failed", MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

            txtUsernameSignup.Text = "";
            txtPasswordSignup.Text = "";
            txtConfirmPasswordSignup.Text = "";

        }

        //buat ngirim pesan ke server
        void sendToServer(string text)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(text);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        //buat recieve
        string recvServer()
        {
            byte[] inStream = new byte[1000000];
            int buffersize = 0;
            try
            {
                serverStream = clientSocket.GetStream();
                buffersize = clientSocket.ReceiveBufferSize;
                serverStream.Read(inStream, 0, buffersize);
            }

            catch 
            {
                return "";
            }
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            returndata = ""+returndata.Trim();
            return returndata;
        }

        //thread buat ngirim ping
        private void sendPing()
        {
            try
            {
                while (true)
                {
                    if (isConnected == false) return;
                    byte[] outStream1 = System.Text.Encoding.ASCII.GetBytes("ping " + txtUsernameLogin.Text);
                    serverStream.Write(outStream1, 0, outStream1.Length);
                    serverStream.Flush();
                    Thread.Sleep(3000);
                }
            }
            catch
            {
                writeDisplay("ping exception");
                return;
            }
            
        }
        
        //thread buat nerima pesan
        private void getMessage()
        {
            while (true)
            {
                if (isConnected == false) return;
                string data = recvServer();
                string[] command = data.Split(' ');

                //writeDisplay(">>" + data);
               
                if (command[0] == "online")
                {
                    List<string> listusers = command.OfType<string>().ToList();
                    listusers.Remove("online");

                    List<string> listboxusers = new List<string>();
                    foreach (string user in listOnlineUser.Items)
                    {
                        listboxusers.Add(user);
                    }

                    foreach (string listboxuser in listboxusers)
                    {
                        bool isFound = false;
                        //klo ada yang sama di list, yg di list di hapus
                        foreach (string listuser in listusers)
                        {
                            if (listuser == listboxuser)
                            {
                                listusers.Remove(listuser);
                                isFound = true;
                                break;
                            }
                        }
                        
                        //klo ga ada yang sama di list, yg di listbox dihapus
                        if (isFound == false)
                        {
                            if (this.IsHandleCreated)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    this.listOnlineUser.Items.Remove(listboxuser);
                                });
                            }
                        }
                    }
                    foreach (string user in listusers)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                this.listOnlineUser.Items.Add(user);
                            });
                        }
                    }
                }

                if (command[0] == "message")
                {
                    if (command.Length < 3)
                    {
                        writeDisplay(" >> " + command[0] + " " + command[1]);
                        continue;
                    }
                    //string output = string.Join(" ", command, 3, command.Length - 3);
                    //output = command[1] + ": " + output;
                    //writeDisplay(" >> " + output);
                    if (command[3] == "chiper")
                    {
                        string iv = command[5];
                        string key = "bbbbaaaa";
                        BitArray plainbit = DES.ofbDES(DES.strToBit(iv), DES.strToBit(key), DES.hexToBit(command[4]));
                        string plaintext = DES.bitToStr(plainbit);
                        writeDisplay(" >> " + plaintext);
                        //Console.WriteLine("nerima = ", data);
                    }
                }

            }
        }

        //fungsi buat nampilin text ke rich box
        void writeDisplay(string text)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.rtxtDisplay.Text = this.rtxtDisplay.Text + Environment.NewLine + text;
                    this.rtxtDisplay.SelectionStart = this.rtxtDisplay.Text.Length;
                    this.rtxtDisplay.ScrollToCaret();
                });
            }
            catch
            {
            }
        }

        //buton disconnect
        private void btnDiscon_Click(object sender, EventArgs e) 
        {
            if(isConnected == true)clientSocket.Close();
            rtxtDisplay.Text = "";
            txtUsernameLogin.Text = "";
            txtPasswordLogin.Text = "";
            isConnected = false;
            pnlHome.Show();
        }

        //override fungsi close kanan atas
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (isConnected == true) clientSocket.Close();
            btnDiscon.PerformClick();
        }
        
       
    }
}
