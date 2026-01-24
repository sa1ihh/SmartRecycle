using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartRecycle
{
    public class LoginForm : Form
    {
        private TextBox txtUser;
        private TextBox txtPass;
        private Label lblMsg;
        private Button btnLogin;
        private Button btnCancel;

        public string? LoggedInUser { get; private set; }

        public LoginForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "Login";
            Size = new Size(380, 230);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblUser = new Label
            {
                Text = "Username:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            txtUser = new TextBox
            {
                Location = new Point(110, 18),
                Width = 220
            };

            var lblPass = new Label
            {
                Text = "Password:",
                Location = new Point(20, 60),
                AutoSize = true
            };

            txtPass = new TextBox
            {
                Location = new Point(110, 58),
                Width = 220,
                UseSystemPasswordChar = true
            };

            lblMsg = new Label
            {
                Location = new Point(20, 95),
                Size = new Size(310, 30),
                ForeColor = Color.DarkRed
            };

            btnLogin = new Button
            {
                Text = "Login",
                Location = new Point(110, 140),
                Width = 100
            };

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(230, 140),
                Width = 100
            };

            btnLogin.Click += BtnLogin_Click;
            btnCancel.Click += BtnCancel_Click;

            Controls.Add(lblUser);
            Controls.Add(txtUser);
            Controls.Add(lblPass);
            Controls.Add(txtPass);
            Controls.Add(lblMsg);
            Controls.Add(btnLogin);
            Controls.Add(btnCancel);
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            var username = txtUser.Text.Trim();
            var password = txtPass.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblMsg.Text = "Username and password required.";
                return;
            }

            // DEMO login
            if (username == "admin" && password == "1234")
            {
                LoggedInUser = username;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                lblMsg.Text = "Invalid username or password.";
            }
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}