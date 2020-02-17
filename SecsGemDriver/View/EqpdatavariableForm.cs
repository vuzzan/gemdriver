using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecsGemDriver.View
{
    public partial class EqpdatavariableForm : MetroFramework.Forms.MetroForm
    {
        public Eqpdatavariable objEqpdatavariable = null;
        public static Dictionary<int, string> mapSecsdata = new Dictionary<int, string>();
        public EqpdatavariableForm()
        {
            InitializeComponent();
        }

        private void EqpdatavariableForm_Load(object sender, EventArgs e)
        {
            //Dictionary<int, string> mapSecsdata = new Dictionary<int, string>();
            if (mapSecsdata.Count ==0 )
            {
                List<Secsdatatype> listsecs = Secsdatatype.load();
                foreach (Secsdatatype obj in listsecs)
                {
                    mapSecsdata.Add(obj.sdt_id, obj.sdt_name);
                }
            }
            txt_dv_datatype.DataSource = new BindingSource(mapSecsdata, null);
            txt_dv_datatype.DisplayMember = "Value";
            txt_dv_datatype.ValueMember = "Key";
            //
            if (objEqpdatavariable != null)
            {
                if (objEqpdatavariable.dv_type == 1)
                {
                    // EQP constant
                    dv_type1.Checked = true;
                    dv_type2.Checked = false;
                    dv_type3.Checked = false;
                }
                else if (objEqpdatavariable.dv_type == 2)
                {
                    // EQP constant
                    dv_type1.Checked = false;
                    dv_type2.Checked = true;
                    dv_type3.Checked = false;
                }
                else if (objEqpdatavariable.dv_type == 3)
                {
                    // EQP constant
                    dv_type1.Checked = false;
                    dv_type2.Checked = false;
                    dv_type3.Checked = true;
                }
                //
                txt_dv_uuid.Text = objEqpdatavariable.dv_uuid;
                txt_dv_name.Text = objEqpdatavariable.dv_name;
                txt_dv_unit.Text = objEqpdatavariable.dv_unit;
                txt_dv_default.Text = objEqpdatavariable.dv_default;
                txt_dv_limitmax.Text = objEqpdatavariable.dv_limitmax;
                txt_dv_limitmin.Text = objEqpdatavariable.dv_limitmin;
                if (objEqpdatavariable.dv_datatype == 4 || objEqpdatavariable.dv_datatype == 5 || objEqpdatavariable.dv_datatype == 6)
                {
                    txt_dv_value.Text = objEqpdatavariable.dv_valuetext;
                }
                else
                {
                    txt_dv_value.Text = objEqpdatavariable.dv_value;
                }
                txt_dv_datatype.Text = objEqpdatavariable.dv_secs;
                btn_sts.Checked = objEqpdatavariable.sts == 1;
            }
            //
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dv_type1.Checked == true)
            {
                objEqpdatavariable.dv_type = 1;
            }
            else if (dv_type2.Checked == true)
            {
                objEqpdatavariable.dv_type = 2;
            }
            else if (dv_type3.Checked == true)
            {
                objEqpdatavariable.dv_type = 3;
            }
            //
            objEqpdatavariable.dv_limitmax = ""+Convert.ToDouble(txt_dv_limitmax.Text);
            objEqpdatavariable.dv_limitmin = ""+Convert.ToDouble(txt_dv_limitmin.Text);
            objEqpdatavariable.dv_default = "" + (txt_dv_default.Text);
            objEqpdatavariable.dv_datatype = (((KeyValuePair<int, string>)txt_dv_datatype.SelectedItem).Key);
            objEqpdatavariable.dv_secs = (((KeyValuePair<int, string>)txt_dv_datatype.SelectedItem).Value);
            //
            objEqpdatavariable.dv_name = "" + (txt_dv_name.Text);
            objEqpdatavariable.dv_uuid = "" + (txt_dv_uuid.Text);
            objEqpdatavariable.dv_unit = "" + (txt_dv_unit.Text);
            if (objEqpdatavariable.dv_datatype == 4 || objEqpdatavariable.dv_datatype == 5 || objEqpdatavariable.dv_datatype == 6)
            {
                objEqpdatavariable.dv_valuetext = txt_dv_value.Text;
                objEqpdatavariable.dv_value = "";
            }
            else
            {
                objEqpdatavariable.dv_valuetext = "";
                objEqpdatavariable.dv_value = txt_dv_value.Text;
            }
            objEqpdatavariable.sts = (btn_sts.Checked?1:0);

            if (objEqpdatavariable.dv_id > 0)
            {
                objEqpdatavariable.update();
            }
            else
            {
                objEqpdatavariable.insert();
            }


            this.DialogResult = DialogResult.OK;

            this.Close();
            //
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void EqpdatavariableForm_Activated(object sender, EventArgs e)
        {
            

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}