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
            this.txtUsernameLogin = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listOnlineUser = new System.Windows.Forms.ListBox();
            this.rtxtDisplay = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDiscon = new System.Windows.Forms.Button();
            this.pnlHome = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSignup = new System.Windows.Forms.Button();
            this.txtConfirmPasswordSignup = new System.Windows.Forms.TextBox();
            this.txtPasswordSignup = new System.Windows.Forms.TextBox();
            this.txtUsernameSignup = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLogin = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPasswordLogin = new System.Windows.Forms.TextBox();
            this.pnlHome.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUsernameLogin
            // 
            this.txtUsernameLogin.Location = new System.Drawing.Point(63, 119);
            this.txtUsernameLogin.Name = "txtUsernameLogin";
            this.txtUsernameLogin.Size = new System.Drawing.Size(114, 20);
            this.txtUsernameLogin.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(77, 204);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(60, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Login";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
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
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(24, 103);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 7;
            this.lblUsername.Text = "Username";
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
            // pnlHome
            // 
            this.pnlHome.Controls.Add(this.label8);
            this.pnlHome.Controls.Add(this.btnSignup);
            this.pnlHome.Controls.Add(this.txtConfirmPasswordSignup);
            this.pnlHome.Controls.Add(this.txtPasswordSignup);
            this.pnlHome.Controls.Add(this.txtUsernameSignup);
            this.pnlHome.Controls.Add(this.label5);
            this.pnlHome.Controls.Add(this.label4);
            this.pnlHome.Controls.Add(this.label3);
            this.pnlHome.Controls.Add(this.label1);
            this.pnlHome.Controls.Add(this.lblLogin);
            this.pnlHome.Controls.Add(this.lblPassword);
            this.pnlHome.Controls.Add(this.txtPasswordLogin);
            this.pnlHome.Controls.Add(this.lblUsername);
            this.pnlHome.Controls.Add(this.txtUsernameLogin);
            this.pnlHome.Controls.Add(this.btnConnect);
            this.pnlHome.Location = new System.Drawing.Point(12, 12);
            this.pnlHome.Name = "pnlHome";
            this.pnlHome.Size = new System.Drawing.Size(497, 342);
            this.pnlHome.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Showcard Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(157, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(156, 40);
            this.label8.TabIndex = 23;
            this.label8.Text = "KIJ Chat";
            // 
            // btnSignup
            // 
            this.btnSignup.Location = new System.Drawing.Point(346, 243);
            this.btnSignup.Name = "btnSignup";
            this.btnSignup.Size = new System.Drawing.Size(75, 23);
            this.btnSignup.TabIndex = 18;
            this.btnSignup.Text = "Signup";
            this.btnSignup.UseVisualStyleBackColor = true;
            this.btnSignup.Click += new System.EventHandler(this.btnSignup_Click);
            // 
            // txtConfirmPasswordSignup
            // 
            this.txtConfirmPasswordSignup.Location = new System.Drawing.Point(336, 208);
            this.txtConfirmPasswordSignup.Name = "txtConfirmPasswordSignup";
            this.txtConfirmPasswordSignup.Size = new System.Drawing.Size(137, 20);
            this.txtConfirmPasswordSignup.TabIndex = 17;
            this.txtConfirmPasswordSignup.UseSystemPasswordChar = true;
            // 
            // txtPasswordSignup
            // 
            this.txtPasswordSignup.Location = new System.Drawing.Point(336, 164);
            this.txtPasswordSignup.Name = "txtPasswordSignup";
            this.txtPasswordSignup.Size = new System.Drawing.Size(137, 20);
            this.txtPasswordSignup.TabIndex = 16;
            this.txtPasswordSignup.UseSystemPasswordChar = true;
            // 
            // txtUsernameSignup
            // 
            this.txtUsernameSignup.Location = new System.Drawing.Point(336, 119);
            this.txtUsernameSignup.Name = "txtUsernameSignup";
            this.txtUsernameSignup.Size = new System.Drawing.Size(137, 20);
            this.txtUsernameSignup.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(297, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Confirm Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(299, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(297, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Text", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(341, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 30);
            this.label1.TabIndex = 11;
            this.label1.Text = "Signup";
            // 
            // lblLogin
            // 
            this.lblLogin.Font = new System.Drawing.Font("Sitka Text", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogin.Location = new System.Drawing.Point(72, 70);
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Size = new System.Drawing.Size(78, 36);
            this.lblLogin.TabIndex = 10;
            this.lblLogin.Text = "Login";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(24, 147);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 9;
            this.lblPassword.Text = "Password";
            // 
            // txtPasswordLogin
            // 
            this.txtPasswordLogin.Location = new System.Drawing.Point(63, 164);
            this.txtPasswordLogin.Name = "txtPasswordLogin";
            this.txtPasswordLogin.Size = new System.Drawing.Size(114, 20);
            this.txtPasswordLogin.TabIndex = 1;
            this.txtPasswordLogin.UseSystemPasswordChar = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 366);
            this.Controls.Add(this.pnlHome);
            this.Controls.Add(this.btnDiscon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtxtDisplay);
            this.Controls.Add(this.listOnlineUser);
            this.Name = "Form1";
            this.Text = "Form1";
            this.pnlHome.ResumeLayout(false);
            this.pnlHome.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsernameLogin;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.RichTextBox rtxtDisplay;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListBox listOnlineUser;
        private System.Windows.Forms.Button btnDiscon;
        private System.Windows.Forms.Panel pnlHome;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPasswordLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSignup;
        private System.Windows.Forms.TextBox txtConfirmPasswordSignup;
        private System.Windows.Forms.TextBox txtPasswordSignup;
        private System.Windows.Forms.TextBox txtUsernameSignup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

