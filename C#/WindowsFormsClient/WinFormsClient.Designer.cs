namespace WinFormsClient
{
    partial class WinFormsClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.SignInButton = new System.Windows.Forms.Button();
            this.StatusText = new System.Windows.Forms.Label();
            this.SignInPanel = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ChatPanel = new System.Windows.Forms.Panel();
            this.SignInPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.ChatPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Enter user name:";
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(7, 24);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(401, 20);
            this.UserNameTextBox.TabIndex = 4;
            // 
            // SignInButton
            // 
            this.SignInButton.Location = new System.Drawing.Point(414, 22);
            this.SignInButton.Name = "SignInButton";
            this.SignInButton.Size = new System.Drawing.Size(75, 23);
            this.SignInButton.TabIndex = 5;
            this.SignInButton.Text = "Sign In";
            this.SignInButton.UseVisualStyleBackColor = true;
            this.SignInButton.Click += new System.EventHandler(this.SignInButton_Click);
            // 
            // StatusText
            // 
            this.StatusText.Location = new System.Drawing.Point(12, 59);
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(477, 13);
            this.StatusText.TabIndex = 6;
            this.StatusText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.StatusText.Visible = false;
            // 
            // SignInPanel
            // 
            this.SignInPanel.Controls.Add(this.StatusText);
            this.SignInPanel.Controls.Add(this.SignInButton);
            this.SignInPanel.Controls.Add(this.UserNameTextBox);
            this.SignInPanel.Controls.Add(this.label1);
            this.SignInPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SignInPanel.Location = new System.Drawing.Point(0, 0);
            this.SignInPanel.Name = "SignInPanel";
            this.SignInPanel.Size = new System.Drawing.Size(817, 513);
            this.SignInPanel.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(26, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(621, 316);
            this.dataGridView1.TabIndex = 8;
            // 
            // ChatPanel
            // 
            this.ChatPanel.Controls.Add(this.dataGridView1);
            this.ChatPanel.Location = new System.Drawing.Point(0, 0);
            this.ChatPanel.Name = "ChatPanel";
            this.ChatPanel.Size = new System.Drawing.Size(805, 1009);
            this.ChatPanel.TabIndex = 4;
            // 
            // WinFormsClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 513);
            this.Controls.Add(this.ChatPanel);
            this.Controls.Add(this.SignInPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(517, 552);
            this.Name = "WinFormsClient";
            this.Text = "SAFE WINFORM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinFormsClient_FormClosing);
            this.SignInPanel.ResumeLayout(false);
            this.SignInPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ChatPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.Button SignInButton;
        private System.Windows.Forms.Label StatusText;
        private System.Windows.Forms.Panel SignInPanel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel ChatPanel;
    }
}

