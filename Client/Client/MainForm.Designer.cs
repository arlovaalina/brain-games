namespace Client
{
    partial class MainForm
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
            this.clientListView = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clientContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.invitationListView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.titleLabel = new System.Windows.Forms.Label();
            this.startGameButton = new System.Windows.Forms.Button();
            this.invitationFromListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // clientListView
            // 
            this.clientListView.BackColor = System.Drawing.SystemColors.Window;
            this.clientListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clientListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1});
            this.clientListView.Font = new System.Drawing.Font("Gadugi", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientListView.FullRowSelect = true;
            this.clientListView.GridLines = true;
            this.clientListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.clientListView.Location = new System.Drawing.Point(398, 17);
            this.clientListView.Name = "clientListView";
            this.clientListView.Size = new System.Drawing.Size(202, 147);
            this.clientListView.TabIndex = 1;
            this.clientListView.UseCompatibleStateImageBehavior = false;
            this.clientListView.View = System.Windows.Forms.View.Details;
            this.clientListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.clientListView_MouseClick);
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Users online";
            this.ColumnHeader1.Width = 200;
            // 
            // clientContextMenu
            // 
            this.clientContextMenu.Name = "clientContextMenu";
            this.clientContextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // invitationListView
            // 
            this.invitationListView.BackColor = System.Drawing.SystemColors.Window;
            this.invitationListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.invitationListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.invitationListView.Font = new System.Drawing.Font("Gadugi", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invitationListView.FullRowSelect = true;
            this.invitationListView.GridLines = true;
            this.invitationListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.invitationListView.Location = new System.Drawing.Point(398, 170);
            this.invitationListView.Name = "invitationListView";
            this.invitationListView.Size = new System.Drawing.Size(202, 147);
            this.invitationListView.TabIndex = 5;
            this.invitationListView.UseCompatibleStateImageBehavior = false;
            this.invitationListView.View = System.Windows.Forms.View.Details;
            this.invitationListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.invitationListView_MouseClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Invitations for you";
            this.columnHeader2.Width = 200;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Gadugi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(67, 51);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(238, 28);
            this.titleLabel.TabIndex = 6;
            this.titleLabel.Text = "Let\'s train your Brain!";
            // 
            // startGameButton
            // 
            this.startGameButton.BackColor = System.Drawing.Color.YellowGreen;
            this.startGameButton.FlatAppearance.BorderColor = System.Drawing.Color.YellowGreen;
            this.startGameButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startGameButton.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startGameButton.Location = new System.Drawing.Point(12, 439);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(374, 31);
            this.startGameButton.TabIndex = 7;
            this.startGameButton.Text = "Start Game";
            this.startGameButton.UseVisualStyleBackColor = false;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // invitationFromListView
            // 
            this.invitationFromListView.BackColor = System.Drawing.SystemColors.Window;
            this.invitationFromListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.invitationFromListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.invitationFromListView.Font = new System.Drawing.Font("Gadugi", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invitationFromListView.FullRowSelect = true;
            this.invitationFromListView.GridLines = true;
            this.invitationFromListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.invitationFromListView.Location = new System.Drawing.Point(398, 323);
            this.invitationFromListView.Name = "invitationFromListView";
            this.invitationFromListView.Size = new System.Drawing.Size(202, 147);
            this.invitationFromListView.TabIndex = 8;
            this.invitationFromListView.UseCompatibleStateImageBehavior = false;
            this.invitationFromListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Invitations from you";
            this.columnHeader3.Width = 200;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Gadugi", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(107, 17);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(80, 28);
            this.nameLabel.TabIndex = 9;
            this.nameLabel.Text = "Hello, ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(618, 482);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.invitationFromListView);
            this.Controls.Add(this.startGameButton);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.invitationListView);
            this.Controls.Add(this.clientListView);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView clientListView;
        private System.Windows.Forms.ColumnHeader ColumnHeader1;
        private System.Windows.Forms.ContextMenuStrip clientContextMenu;
        private System.Windows.Forms.ListView invitationListView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.ListView invitationFromListView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label nameLabel;
    }
}