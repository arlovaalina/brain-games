using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class GameForm : Form
    {
        public Form mainForm;

        public GameForm()
        {
            InitializeComponent();
        }

        public GameForm(Form form)
        {
            mainForm = form;
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            questionLabel.MaximumSize = new Size(rectangleShape1.Width - 10, rectangleShape1.Height - 10);
        }
    }
}
