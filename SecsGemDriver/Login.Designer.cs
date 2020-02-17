namespace SecsGemDriver
{
    partial class Login
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.txtID = new MetroFramework.Controls.MetroTextBox();
            this.txtPW = new MetroFramework.Controls.MetroTextBox();
            this.btnLOGIN = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(30, 95);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(21, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "ID";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(30, 138);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(64, 19);
            this.metroLabel2.TabIndex = 0;
            this.metroLabel2.Text = "Password";
            // 
            // txtID
            // 
            // 
            // 
            // 
            this.txtID.CustomButton.Image = null;
            this.txtID.CustomButton.Location = new System.Drawing.Point(155, 1);
            this.txtID.CustomButton.Name = "";
            this.txtID.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtID.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtID.CustomButton.TabIndex = 1;
            this.txtID.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtID.CustomButton.UseSelectable = true;
            this.txtID.CustomButton.Visible = false;
            this.txtID.Lines = new string[] {
        "admin"};
            this.txtID.Location = new System.Drawing.Point(118, 91);
            this.txtID.MaxLength = 32767;
            this.txtID.Name = "txtID";
            this.txtID.PasswordChar = '\0';
            this.txtID.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtID.SelectedText = "";
            this.txtID.SelectionLength = 0;
            this.txtID.SelectionStart = 0;
            this.txtID.ShortcutsEnabled = true;
            this.txtID.Size = new System.Drawing.Size(177, 23);
            this.txtID.TabIndex = 1;
            this.txtID.Text = "admin";
            this.txtID.UseSelectable = true;
            this.txtID.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtID.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtID_KeyDown);
            this.txtID.Leave += new System.EventHandler(this.txtID_Leave);
            // 
            // txtPW
            // 
            // 
            // 
            // 
            this.txtPW.CustomButton.Image = null;
            this.txtPW.CustomButton.Location = new System.Drawing.Point(155, 1);
            this.txtPW.CustomButton.Name = "";
            this.txtPW.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtPW.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtPW.CustomButton.TabIndex = 1;
            this.txtPW.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPW.CustomButton.UseSelectable = true;
            this.txtPW.CustomButton.Visible = false;
            this.txtPW.Lines = new string[] {
        "admin"};
            this.txtPW.Location = new System.Drawing.Point(118, 134);
            this.txtPW.MaxLength = 32767;
            this.txtPW.Name = "txtPW";
            this.txtPW.PasswordChar = '*';
            this.txtPW.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPW.SelectedText = "";
            this.txtPW.SelectionLength = 0;
            this.txtPW.SelectionStart = 0;
            this.txtPW.ShortcutsEnabled = true;
            this.txtPW.Size = new System.Drawing.Size(177, 23);
            this.txtPW.TabIndex = 1;
            this.txtPW.Text = "admin";
            this.txtPW.UseSelectable = true;
            this.txtPW.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPW.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtPW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPW_KeyDown);
            this.txtPW.Leave += new System.EventHandler(this.txtPW_Leave);
            // 
            // btnLOGIN
            // 
            this.btnLOGIN.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnLOGIN.Location = new System.Drawing.Point(118, 175);
            this.btnLOGIN.Name = "btnLOGIN";
            this.btnLOGIN.Size = new System.Drawing.Size(176, 49);
            this.btnLOGIN.TabIndex = 2;
            this.btnLOGIN.Text = "LOGIN";
            this.btnLOGIN.UseSelectable = true;
            this.btnLOGIN.Click += new System.EventHandler(this.btnLOGIN_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 273);
            this.Controls.Add(this.btnLOGIN);
            this.Controls.Add(this.txtPW);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Name = "Login";
            this.Text = "Login SecsGEM Configuration";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox txtID;
        private MetroFramework.Controls.MetroTextBox txtPW;
        private MetroFramework.Controls.MetroButton btnLOGIN;
    }
}