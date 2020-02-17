using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecsGemDriver.Model
{
    public partial class EquipmentForm : MetroFramework.Forms.MetroForm
    {
        public Equipment objEquipment = null;
		
        public EquipmentForm()
        {
            InitializeComponent();
        }

        private void EquipmentForm_Load(object sender, EventArgs e)
        {
            if (objEquipment.eqp_id>0)
            {
                txt_eqp_info.Text = objEquipment.eqp_info;
                txt_eqp_name.Text = objEquipment.eqp_name;
                btn_sts.Checked = objEquipment.sts==1;
                //
                btnNew.Enabled = true;
                btnNew.Text = "DELETE";
                btnSave.Text = "UPDATE";
                //
            }
            else
            {
                objEquipment.eqp_info = "Info";
                objEquipment.eqp_name = "new EQP";
                txt_eqp_info.Text = objEquipment.eqp_info;
                txt_eqp_name.Text = objEquipment.eqp_name;
                btn_sts.Checked = true;
                //
                btnNew.Enabled = true;
                btnSave.Text = "CREATE NEW";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            objEquipment.sts = btn_sts.Checked ? 1 : 0;
            objEquipment.eqp_name = txt_eqp_name.Text;
            objEquipment.eqp_info = txt_eqp_info.Text;
            if (objEquipment.eqp_id <= 0)
            {
                // Insert
                objEquipment.insert();
            }
            else {
                objEquipment.update();
            }
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (objEquipment.eqp_id > 0)
            {
                List<Equipment> listEqp = Equipment.load("select * from equipment where sts=1");
                if (listEqp.Count == 1)
                {
                    this.Close();
                    return;
                }
                DialogResult dr = MessageBox.Show("Delete ??", "Confirm", MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    objEquipment.delete();
                    this.Close();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
