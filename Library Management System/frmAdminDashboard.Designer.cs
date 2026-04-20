namespace Library_Management_System
{
    partial class frmAdminDashboard
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
            this.menuAdmin = new System.Windows.Forms.MenuStrip();
            this.booksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UsersMenus = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelDashboard = new System.Windows.Forms.Panel();
            this.panelOverdue = new System.Windows.Forms.Panel();
            this.lblOverdue = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panelPendingReturns = new System.Windows.Forms.Panel();
            this.lblPendingReturns = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelBorrowedToday = new System.Windows.Forms.Panel();
            this.lblBorrowedToday = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelTotalLibrarians = new System.Windows.Forms.Panel();
            this.lblTotalLibrarians = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelTotalBorrowers = new System.Windows.Forms.Panel();
            this.lblTotalBorrowers = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelTotalBooks = new System.Windows.Forms.Panel();
            this.lblTotalBooks = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuAdmin.SuspendLayout();
            this.panelDashboard.SuspendLayout();
            this.panelOverdue.SuspendLayout();
            this.panelPendingReturns.SuspendLayout();
            this.panelBorrowedToday.SuspendLayout();
            this.panelTotalLibrarians.SuspendLayout();
            this.panelTotalBorrowers.SuspendLayout();
            this.panelTotalBooks.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuAdmin
            // 
            this.menuAdmin.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuAdmin.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuAdmin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.booksToolStripMenuItem,
            this.UsersMenus,
            this.logoutToolStripMenuItem});
            this.menuAdmin.Location = new System.Drawing.Point(0, 0);
            this.menuAdmin.Name = "menuAdmin";
            this.menuAdmin.Size = new System.Drawing.Size(800, 33);
            this.menuAdmin.TabIndex = 0;
            this.menuAdmin.Text = "menuAdmin";
            // 
            // booksToolStripMenuItem
            // 
            this.booksToolStripMenuItem.Name = "booksToolStripMenuItem";
            this.booksToolStripMenuItem.Size = new System.Drawing.Size(77, 29);
            this.booksToolStripMenuItem.Text = "Books";
            this.booksToolStripMenuItem.Click += new System.EventHandler(this.booksToolStripMenuItem_Click);
            // 
            // UsersMenus
            // 
            this.UsersMenus.Name = "UsersMenus";
            this.UsersMenus.Size = new System.Drawing.Size(71, 29);
            this.UsersMenus.Text = "Users";
            this.UsersMenus.Click += new System.EventHandler(this.UsersMenus_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(85, 29);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // panelDashboard
            // 
            this.panelDashboard.BackColor = System.Drawing.Color.LightGray;
            this.panelDashboard.Controls.Add(this.panelOverdue);
            this.panelDashboard.Controls.Add(this.panelPendingReturns);
            this.panelDashboard.Controls.Add(this.panelBorrowedToday);
            this.panelDashboard.Controls.Add(this.panelTotalLibrarians);
            this.panelDashboard.Controls.Add(this.panelTotalBorrowers);
            this.panelDashboard.Controls.Add(this.panelTotalBooks);
            this.panelDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDashboard.Location = new System.Drawing.Point(0, 33);
            this.panelDashboard.Name = "panelDashboard";
            this.panelDashboard.Size = new System.Drawing.Size(800, 417);
            this.panelDashboard.TabIndex = 1;
            // 
            // panelOverdue
            // 
            this.panelOverdue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(181)))));
            this.panelOverdue.Controls.Add(this.lblOverdue);
            this.panelOverdue.Controls.Add(this.label8);
            this.panelOverdue.Location = new System.Drawing.Point(569, 213);
            this.panelOverdue.Name = "panelOverdue";
            this.panelOverdue.Size = new System.Drawing.Size(200, 100);
            this.panelOverdue.TabIndex = 4;
            // 
            // lblOverdue
            // 
            this.lblOverdue.AutoSize = true;
            this.lblOverdue.Location = new System.Drawing.Point(23, 55);
            this.lblOverdue.Name = "lblOverdue";
            this.lblOverdue.Size = new System.Drawing.Size(18, 20);
            this.lblOverdue.TabIndex = 1;
            this.lblOverdue.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Overdue";
            // 
            // panelPendingReturns
            // 
            this.panelPendingReturns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(181)))));
            this.panelPendingReturns.Controls.Add(this.lblPendingReturns);
            this.panelPendingReturns.Controls.Add(this.label6);
            this.panelPendingReturns.Location = new System.Drawing.Point(303, 213);
            this.panelPendingReturns.Name = "panelPendingReturns";
            this.panelPendingReturns.Size = new System.Drawing.Size(200, 100);
            this.panelPendingReturns.TabIndex = 4;
            // 
            // lblPendingReturns
            // 
            this.lblPendingReturns.AutoSize = true;
            this.lblPendingReturns.Location = new System.Drawing.Point(23, 55);
            this.lblPendingReturns.Name = "lblPendingReturns";
            this.lblPendingReturns.Size = new System.Drawing.Size(18, 20);
            this.lblPendingReturns.TabIndex = 1;
            this.lblPendingReturns.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Pending Returns";
            // 
            // panelBorrowedToday
            // 
            this.panelBorrowedToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(181)))));
            this.panelBorrowedToday.Controls.Add(this.lblBorrowedToday);
            this.panelBorrowedToday.Controls.Add(this.label5);
            this.panelBorrowedToday.Location = new System.Drawing.Point(31, 213);
            this.panelBorrowedToday.Name = "panelBorrowedToday";
            this.panelBorrowedToday.Size = new System.Drawing.Size(200, 100);
            this.panelBorrowedToday.TabIndex = 3;
            // 
            // lblBorrowedToday
            // 
            this.lblBorrowedToday.AutoSize = true;
            this.lblBorrowedToday.Location = new System.Drawing.Point(23, 55);
            this.lblBorrowedToday.Name = "lblBorrowedToday";
            this.lblBorrowedToday.Size = new System.Drawing.Size(18, 20);
            this.lblBorrowedToday.TabIndex = 1;
            this.lblBorrowedToday.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Borrowed Today";
            // 
            // panelTotalLibrarians
            // 
            this.panelTotalLibrarians.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(181)))));
            this.panelTotalLibrarians.Controls.Add(this.lblTotalLibrarians);
            this.panelTotalLibrarians.Controls.Add(this.label4);
            this.panelTotalLibrarians.Location = new System.Drawing.Point(569, 44);
            this.panelTotalLibrarians.Name = "panelTotalLibrarians";
            this.panelTotalLibrarians.Size = new System.Drawing.Size(200, 100);
            this.panelTotalLibrarians.TabIndex = 2;
            // 
            // lblTotalLibrarians
            // 
            this.lblTotalLibrarians.AutoSize = true;
            this.lblTotalLibrarians.Location = new System.Drawing.Point(23, 55);
            this.lblTotalLibrarians.Name = "lblTotalLibrarians";
            this.lblTotalLibrarians.Size = new System.Drawing.Size(18, 20);
            this.lblTotalLibrarians.TabIndex = 1;
            this.lblTotalLibrarians.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Total Librarians";
            // 
            // panelTotalBorrowers
            // 
            this.panelTotalBorrowers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(181)))));
            this.panelTotalBorrowers.Controls.Add(this.lblTotalBorrowers);
            this.panelTotalBorrowers.Controls.Add(this.label3);
            this.panelTotalBorrowers.Location = new System.Drawing.Point(303, 44);
            this.panelTotalBorrowers.Name = "panelTotalBorrowers";
            this.panelTotalBorrowers.Size = new System.Drawing.Size(200, 100);
            this.panelTotalBorrowers.TabIndex = 1;
            // 
            // lblTotalBorrowers
            // 
            this.lblTotalBorrowers.AutoSize = true;
            this.lblTotalBorrowers.Location = new System.Drawing.Point(23, 55);
            this.lblTotalBorrowers.Name = "lblTotalBorrowers";
            this.lblTotalBorrowers.Size = new System.Drawing.Size(18, 20);
            this.lblTotalBorrowers.TabIndex = 1;
            this.lblTotalBorrowers.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Total Borrowers";
            // 
            // panelTotalBooks
            // 
            this.panelTotalBooks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(181)))));
            this.panelTotalBooks.Controls.Add(this.lblTotalBooks);
            this.panelTotalBooks.Controls.Add(this.label1);
            this.panelTotalBooks.Location = new System.Drawing.Point(31, 44);
            this.panelTotalBooks.Name = "panelTotalBooks";
            this.panelTotalBooks.Size = new System.Drawing.Size(200, 100);
            this.panelTotalBooks.TabIndex = 0;
            // 
            // lblTotalBooks
            // 
            this.lblTotalBooks.AutoSize = true;
            this.lblTotalBooks.Location = new System.Drawing.Point(23, 55);
            this.lblTotalBooks.Name = "lblTotalBooks";
            this.lblTotalBooks.Size = new System.Drawing.Size(18, 20);
            this.lblTotalBooks.TabIndex = 1;
            this.lblTotalBooks.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Books";
            // 
            // frmAdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelDashboard);
            this.Controls.Add(this.menuAdmin);
            this.MainMenuStrip = this.menuAdmin;
            this.Name = "frmAdminDashboard";
            this.Text = "Admin Dashboard";
            this.Load += new System.EventHandler(this.frmAdminDashboard_Load);
            this.menuAdmin.ResumeLayout(false);
            this.menuAdmin.PerformLayout();
            this.panelDashboard.ResumeLayout(false);
            this.panelOverdue.ResumeLayout(false);
            this.panelOverdue.PerformLayout();
            this.panelPendingReturns.ResumeLayout(false);
            this.panelPendingReturns.PerformLayout();
            this.panelBorrowedToday.ResumeLayout(false);
            this.panelBorrowedToday.PerformLayout();
            this.panelTotalLibrarians.ResumeLayout(false);
            this.panelTotalLibrarians.PerformLayout();
            this.panelTotalBorrowers.ResumeLayout(false);
            this.panelTotalBorrowers.PerformLayout();
            this.panelTotalBooks.ResumeLayout(false);
            this.panelTotalBooks.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuAdmin;
        private System.Windows.Forms.ToolStripMenuItem UsersMenus;
        private System.Windows.Forms.ToolStripMenuItem booksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.Panel panelDashboard;
        private System.Windows.Forms.Panel panelTotalBooks;
        private System.Windows.Forms.Panel panelBorrowedToday;
        private System.Windows.Forms.Label lblBorrowedToday;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelTotalLibrarians;
        private System.Windows.Forms.Label lblTotalLibrarians;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panelTotalBorrowers;
        private System.Windows.Forms.Label lblTotalBorrowers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalBooks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelOverdue;
        private System.Windows.Forms.Label lblOverdue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panelPendingReturns;
        private System.Windows.Forms.Label lblPendingReturns;
        private System.Windows.Forms.Label label6;
    }
}