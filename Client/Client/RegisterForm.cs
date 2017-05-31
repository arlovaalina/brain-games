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
    public partial class RegisterForm : Form
    {
        public Form mainForm;
        public string login;
        bool isSubmitButtonClicked = false;

        public RegisterForm()
        {
            InitializeComponent();
        }

        public RegisterForm(Form form)
        {
            mainForm = form;
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            login = loginTextBox.Text;
            if (login != "")
            {
                try
                {
                    isSubmitButtonClicked = true;
                    Client client = new Client(login, this, mainForm);
                    client.ConnectToServer("192.168.1.6", 8888);
                    client.SendMessageToServer("ADD_USER" + login);
                    (mainForm as MainForm).InitializeClient(client);
                    Close();
                }
                catch
                {
                    PrintError("Ошибка соединения с сервером");
                }
            }
            else
                MessageBox.Show("Поле с именем должно быть заполнено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public void PrintError(string error)
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSubmitButtonClicked)
                mainForm.Close();
        }
    }
}
