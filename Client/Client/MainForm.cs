using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Client
{
    public partial class MainForm : Form
    {
        public RegisterForm registerForm;
        public GameForm gameForm;
        Client client;
        string opponentLogin = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Hide();
            registerForm = new RegisterForm(this);
            registerForm.ShowDialog();
            startGameButton.Enabled = false;
            nameLabel.Text = nameLabel.Text + registerForm.login + "!";
        }

        public void InitializeClient(Client registerClient)
        {
            client = registerClient;
        }

        public void PrintUserList(string userList)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => PrintUserList(userList)));
                return;
            }
            clientListView.Items.Clear();
            string[] userArray = userList.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string user in userArray)
            {
                ListViewItem userItem = new ListViewItem(user);
                clientListView.Items.Add(userItem);
            }
        }

        public void PrintInvitation(string invitationList, string listType)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => PrintInvitation(invitationList, listType)));
                return;
            }
            ListView invListView;
            if (listType == "forList")
                invListView = invitationListView;
            else
                invListView = invitationFromListView;
            invListView.Items.Clear();
            string[] invitationArray = invitationList.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string invitation in invitationArray)
            {
                ListViewItem invitationItem = new ListViewItem(invitation);
                invListView.Items.Add(invitationItem);
            }
        }

        public void PrintError(string error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public void HighlightAcceptedInvitation(string login)
        {
            for (int i = 0; i < invitationListView.Items.Count; i++)
            {
                invitationListView.Items[i].BackColor = Color.White;
            }

            for (int i = 0; i < invitationFromListView.Items.Count; i++)
            {
                if (invitationFromListView.Items[i].Text == login)
                {
                    invitationFromListView.Items[i].BackColor = Color.Crimson;
                    break;
                }
            }
            startGameButton.Enabled = true;
            opponentLogin = login;
        }

        private void sendInvitation_Click(object sender, EventArgs e)
        {
            client.SendMessageToServer("INV_USER" + clientListView.SelectedItems[0].SubItems[0].Text);
        }

        private void clientListView_MouseClick(object sender, MouseEventArgs e)
        {
            clientContextMenu.Items.Clear();
            ToolStripMenuItem sendInvitationItem = new ToolStripMenuItem("Send invitation");

            sendInvitationItem.Click += sendInvitation_Click;

            if (e.Button == MouseButtons.Right)
            {
                if (clientListView.SelectedItems.Count != 0)
                {
                    Point point = clientListView.PointToScreen(e.Location);
                    if (clientListView.SelectedItems[0].SubItems[0].Text != registerForm.login)
                    {
                        clientContextMenu.Items.Add(sendInvitationItem);
                    }
                    clientContextMenu.Show(point);
                }
            }
        }

        private void invitationListView_MouseClick(object sender, MouseEventArgs e)
        {
            ContextMenuStrip invitationContextMenu = new ContextMenuStrip();
            invitationContextMenu.Items.Clear();
            ToolStripMenuItem acceptInvitationItem = new ToolStripMenuItem("Accept invitation");

            acceptInvitationItem.Click += acceptInvitation_Click;

            if (e.Button == MouseButtons.Right)
            {
                if (invitationListView.SelectedItems.Count != 0)
                {
                    Point point = invitationListView.PointToScreen(e.Location);
                    invitationContextMenu.Items.Add(acceptInvitationItem);
                    invitationContextMenu.Show(point);
                }
            }
        }

        public void acceptInvitation_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < invitationListView.Items.Count; i++)
            {
                invitationListView.Items[i].BackColor = Color.White;
            }
            for (int i = 0; i < invitationFromListView.Items.Count; i++)
            {
                invitationFromListView.Items[i].BackColor = Color.White;
            }
            invitationListView.SelectedItems[0].BackColor = Color.LightSteelBlue;
            opponentLogin = invitationListView.SelectedItems[0].SubItems[0].Text;
            invitationListView.SelectedItems[0].Selected = false;
            startGameButton.Enabled = true;

        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            client.SendMessageToServer("START_GM" + opponentLogin);
            GameForm gameForm = new GameForm(this);
            gameForm.ShowDialog();
        }

        public void CloseForm()
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => CloseForm()));
                return;
            }
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //client.SendMessageToServer("CON_LOST");
            //client.Disconnect();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Image img = Image.FromFile(@"D:\Универ\4 сем\Курсач\Pictures\114161982_4552399_mozgovoi_shtyrm.jpg");
            Bitmap temp = new Bitmap(img);
            Graphics g = Graphics.FromImage(temp);

            g = this.CreateGraphics();
            int width = 320;
            int height = 320;
            int top = 90;
            int left = 25;

            g.DrawImage(img, left, top, width, height);
            g.Dispose();
        }
    }
}
