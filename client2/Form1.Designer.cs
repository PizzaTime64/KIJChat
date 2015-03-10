namespace KIJChat
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listOnlineUser = new System.Windows.Forms.ListBox();
            this.rtxtDisplay = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDiscon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(80, 24);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(114, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(212, 22);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(60, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // listOnlineUser
            // 
            this.listOnlineUser.FormattingEnabled = true;
            this.listOnlineUser.Location = new System.Drawing.Point(388, 50);
            this.listOnlineUser.Name = "listOnlineUser";
            this.listOnlineUser.Size = new System.Drawing.Size(120, 264);
            this.listOnlineUser.TabIndex = 3;
            // 
            // rtxtDisplay
            // 
            this.rtxtDisplay.Location = new System.Drawing.Point(26, 62);
            this.rtxtDisplay.Name = "rtxtDisplay";
            this.rtxtDisplay.ReadOnly = true;
            this.rtxtDisplay.Size = new System.Drawing.Size(299, 153);
            this.rtxtDisplay.TabIndex = 4;
            this.rtxtDisplay.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(238, 255);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(26, 245);
            this.txtInput.Multiline = true;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(201, 50);
            this.txtInput.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(407, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Online Users";
            // 
            // btnDiscon
            // 
            this.btnDiscon.Location = new System.Drawing.Point(278, 22);
            this.btnDiscon.Name = "btnDiscon";
            this.btnDiscon.Size = new System.Drawing.Size(75, 23);
            this.btnDiscon.TabIndex = 10;
            this.btnDiscon.Text = "Disconnect";
            this.btnDiscon.UseVisualStyleBackColor = true;
            this.btnDiscon.Click += new System.EventHandler(this.btnDiscon_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 366);
            this.Controls.Add(this.btnDiscon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtxtDisplay);
            this.Controls.Add(this.listOnlineUser);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtUsername);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.RichTextBox rtxtDisplay;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListBox listOnlineUser;
        private System.Windows.Forms.Button btnDiscon;
    }
}

