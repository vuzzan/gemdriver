namespace SecsGemDriver.Model
{
    partial class EquipmentForm
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
            this.btnNew = new MetroFramework.Controls.MetroButton();
            this.btn_sts = new MetroFramework.Controls.MetroToggle();
            this.btnSave = new MetroFramework.Controls.MetroButton();
            this.txt_eqp_info = new MetroFramework.Controls.MetroTextBox();
            this.txt_eqp_name = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnNew.Location = new System.Drawing.Point(24, 200);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(113, 41);
            this.btnNew.TabIndex = 14;
            this.btnNew.Text = "NEW";
            this.btnNew.UseSelectable = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btn_sts
            // 
            this.btn_sts.AutoSize = true;
            this.btn_sts.Checked = true;
            this.btn_sts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btn_sts.Location = new System.Drawing.Point(178, 165);
            this.btn_sts.Name = "btn_sts";
            this.btn_sts.Size = new System.Drawing.Size(80, 17);
            this.btn_sts.TabIndex = 13;
            this.btn_sts.Text = "On";
            this.btn_sts.UseSelectable = true;
            // 
            // btnSave
            // 
            this.btnSave.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.btnSave.Location = new System.Drawing.Point(178, 200);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(324, 41);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseSelectable = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txt_eqp_info
            // 
            // 
            // 
            // 
            this.txt_eqp_info.CustomButton.Image = null;
            this.txt_eqp_info.CustomButton.Location = new System.Drawing.Point(286, 2);
            this.txt_eqp_info.CustomButton.Name = "";
            this.txt_eqp_info.CustomButton.Size = new System.Drawing.Size(35, 35);
            this.txt_eqp_info.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_eqp_info.CustomButton.TabIndex = 1;
            this.txt_eqp_info.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_eqp_info.CustomButton.UseSelectable = true;
            this.txt_eqp_info.CustomButton.Visible = false;
            this.txt_eqp_info.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.txt_eqp_info.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.txt_eqp_info.Lines = new string[0];
            this.txt_eqp_info.Location = new System.Drawing.Point(178, 110);
            this.txt_eqp_info.MaxLength = 32767;
            this.txt_eqp_info.Name = "txt_eqp_info";
            this.txt_eqp_info.PasswordChar = '\0';
            this.txt_eqp_info.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_eqp_info.SelectedText = "";
            this.txt_eqp_info.SelectionLength = 0;
            this.txt_eqp_info.SelectionStart = 0;
            this.txt_eqp_info.ShortcutsEnabled = true;
            this.txt_eqp_info.Size = new System.Drawing.Size(324, 40);
            this.txt_eqp_info.TabIndex = 10;
            this.txt_eqp_info.UseSelectable = true;
            this.txt_eqp_info.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_eqp_info.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txt_eqp_name
            // 
            // 
            // 
            // 
            this.txt_eqp_name.CustomButton.Image = null;
            this.txt_eqp_name.CustomButton.Location = new System.Drawing.Point(286, 2);
            this.txt_eqp_name.CustomButton.Name = "";
            this.txt_eqp_name.CustomButton.Size = new System.Drawing.Size(35, 35);
            this.txt_eqp_name.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txt_eqp_name.CustomButton.TabIndex = 1;
            this.txt_eqp_name.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txt_eqp_name.CustomButton.UseSelectable = true;
            this.txt_eqp_name.CustomButton.Visible = false;
            this.txt_eqp_name.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txt_eqp_name.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.txt_eqp_name.Lines = new string[] {
        "Eqp 1"};
            this.txt_eqp_name.Location = new System.Drawing.Point(178, 63);
            this.txt_eqp_name.MaxLength = 32767;
            this.txt_eqp_name.Name = "txt_eqp_name";
            this.txt_eqp_name.PasswordChar = '\0';
            this.txt_eqp_name.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txt_eqp_name.SelectedText = "";
            this.txt_eqp_name.SelectionLength = 0;
            this.txt_eqp_name.SelectionStart = 0;
            this.txt_eqp_name.ShortcutsEnabled = true;
            this.txt_eqp_name.Size = new System.Drawing.Size(324, 40);
            this.txt_eqp_name.TabIndex = 11;
            this.txt_eqp_name.Text = "Eqp 1";
            this.txt_eqp_name.UseSelectable = true;
            this.txt_eqp_name.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txt_eqp_name.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(24, 63);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(112, 19);
            this.metroLabel1.TabIndex = 15;
            this.metroLabel1.Text = "Equipment Name";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(24, 110);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(140, 19);
            this.metroLabel2.TabIndex = 16;
            this.metroLabel2.Text = "Equipment description";
            // 
            // EquipmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 283);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btn_sts);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txt_eqp_info);
            this.Controls.Add(this.txt_eqp_name);
            this.Name = "EquipmentForm";
            this.Text = "Equipment";
            this.Load += new System.EventHandler(this.EquipmentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnNew;
        private MetroFramework.Controls.MetroToggle btn_sts;
        private MetroFramework.Controls.MetroButton btnSave;
        private MetroFramework.Controls.MetroTextBox txt_eqp_info;
        private MetroFramework.Controls.MetroTextBox txt_eqp_name;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
    }
}