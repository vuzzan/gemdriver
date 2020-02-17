using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Web;

namespace SecsGemDriver
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        [DllImport("libsecsdriver.dll", EntryPoint = "_Z11startDriverPKc", CallingConvention = CallingConvention.Cdecl)]
        public static extern int startSecsDriver(string iniFilePath);

        public static string connString = "";
        public static MySqlConnection conn;

        public EqpAlarm objSelectedAlarm = null;
        public Eqpevent objSelectedEvent = null;
        public Eqpreport objSelectedReport = null;
        public Eqpdatavariable objSelectedVariable = null;


        // TCP 
        Socket sck;
        EndPoint remoteEp;
        byte[] buffer;

        //Equipment objEquipment = null;
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPageEquipment_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            loadEquipmentList();
            loadEquipmentConst();
            loadEquipmentDataVariavle();
            loadEquipmentStatusDataVariavle();
            loadEvent();
            loadAlarm();
            //
            loadCbListreport();
            loadCbListVariable();

        }

        private void loadEquipmentList()
        {
            metroGridEquipmentList.DataSource = Equipment.loadDt("select * from equipment where sts<>3");
            metroGridEquipmentList.Columns["eqp_id"].Visible = false;
        }

        private void loadEquipmentConst()
        {
            metroGridEquipmentConstant.DataSource = Eqpdatavariable.loadDt("select * from eqpdatavariable where dv_type=1 and sts<>3");
            metroGridEquipmentConstant.Columns["dv_id"].Visible = false;
            metroGridEquipmentConstant.Columns["eqp_id"].Visible = false;
            metroGridEquipmentConstant.Columns["dv_type"].Visible = false;
            metroGridEquipmentConstant.Columns["dv_default"].Visible = false;
            metroGridEquipmentConstant.Columns["dv_limitmin"].Visible = false;
            metroGridEquipmentConstant.Columns["dv_limitmax"].Visible = false;
            metroGridEquipmentConstant.Columns["dv_datatype"].Visible = false;
            metroGridEquipmentConstant.Columns["dv_valuetext"].Visible = false;
            metroGridEquipmentConstant.AutoResizeColumns();
        }
        private void loadEquipmentDataVariavle()
        {
            metroGridEquipmentDataVariable.DataSource = Eqpdatavariable.loadDt("select * from eqpdatavariable where dv_type=3 and sts<>3");
            metroGridEquipmentDataVariable.Columns["dv_id"].Visible = false;
            metroGridEquipmentDataVariable.Columns["eqp_id"].Visible = false;
            metroGridEquipmentDataVariable.Columns["dv_type"].Visible = false;
            metroGridEquipmentDataVariable.Columns["dv_default"].Visible = false;
            metroGridEquipmentDataVariable.Columns["dv_limitmin"].Visible = false;
            metroGridEquipmentDataVariable.Columns["dv_limitmax"].Visible = false;
            metroGridEquipmentDataVariable.Columns["dv_datatype"].Visible = false;
            metroGridEquipmentDataVariable.Columns["dv_valuetext"].Visible = false;
            metroGridEquipmentDataVariable.AutoResizeColumns();
        }

        private void loadEquipmentStatusDataVariavle()
        {
            metroGridEquipmentStatusVariable.DataSource = Eqpdatavariable.loadDt("select * from eqpdatavariable where dv_type=2 and sts<>3");
            metroGridEquipmentStatusVariable.Columns["dv_id"].Visible = false;
            metroGridEquipmentStatusVariable.Columns["eqp_id"].Visible = false;
            metroGridEquipmentStatusVariable.Columns["dv_type"].Visible = false;
            metroGridEquipmentStatusVariable.Columns["dv_default"].Visible = false;
            metroGridEquipmentStatusVariable.Columns["dv_limitmin"].Visible = false;
            metroGridEquipmentStatusVariable.Columns["dv_limitmax"].Visible = false;
            metroGridEquipmentStatusVariable.Columns["dv_datatype"].Visible = false;
            metroGridEquipmentStatusVariable.Columns["dv_valuetext"].Visible = false;
            metroGridEquipmentStatusVariable.AutoResizeColumns();
        }

        private void loadEvent()
        {
            metroGridLinkedEvent.DataSource = Eqpevent.loadDt("select * from eqpevent where sts<>3");
            metroGridLinkedEvent.Columns["ee_id"].Visible = false;
            metroGridLinkedEvent.Columns["eqp_id"].Visible = false;
            metroGridLinkedEvent.AutoResizeColumns();
            metroGridLinkedReport.DataSource = null;
            metroGridlinkedVariable.DataSource = null;
        }

        private void loadAlarm()
        {
            metroGridEquipmentAlarm.DataSource = EqpAlarm.loadDt("select * from eqp_alarm where sts<>3");
            metroGridEquipmentAlarm.Columns["ea_id"].Visible = false;
            metroGridEquipmentAlarm.Columns["eqp_id"].Visible = false;
            metroGridEquipmentAlarm.AutoResizeColumns();
        }

        private void loadLinkedReport(int ee_id)
        {
            metroGridlinkedVariable.DataSource = null;
            metroGridLinkedReport.DataSource = null;
            //
            metroGridLinkedReport.DataSource = Eqpreport.loadDt("select r.* from eqpreport r, linkevent2report l where l.ee_id='"+ee_id+"' and l.er_id=r.er_id and r.sts<>3");
            metroGridLinkedReport.Columns["er_id"].Visible = false;
            metroGridLinkedReport.Columns["eqp_id"].Visible = false;
            metroGridLinkedReport.Columns["typ"].Visible = false;
            metroGridLinkedReport.AutoResizeColumns();
        }

        private void loadLinkedVariable(int er_id)
        {
            metroGridlinkedVariable.DataSource = null;
            //
            metroGridlinkedVariable.DataSource = Eqpdatavariable.loadDt("select r.* from eqpdatavariable r, linkreport2variable l where l.er_id='" + er_id + "' and l.dv_id=r.dv_id and r.sts<>3");
            metroGridlinkedVariable.Columns["dv_id"].Visible = false;
            metroGridlinkedVariable.Columns["eqp_id"].Visible = false;
            metroGridlinkedVariable.Columns["dv_type"].Visible = false;
            metroGridlinkedVariable.Columns["dv_default"].Visible = false;
            metroGridlinkedVariable.Columns["dv_limitmin"].Visible = false;
            metroGridlinkedVariable.Columns["dv_limitmax"].Visible = false;
            metroGridlinkedVariable.Columns["dv_datatype"].Visible = false;
            metroGridlinkedVariable.Columns["dv_valuetext"].Visible = false;
            metroGridlinkedVariable.AutoResizeColumns();
        }

        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (!(index < 0))
            {
                DataGridViewRow row = metroGridEquipmentList.Rows[index];
                Equipment obj = new Equipment(row);
                Model.EquipmentForm dlg = new Model.EquipmentForm();
                dlg.objEquipment = obj;
                dlg.ShowDialog();
                loadEquipmentList();

            }
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {

        }

        private void btnSAVE_Click(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
        }

        private void metroGrid1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            Equipment obj = new Equipment();
            Model.EquipmentForm dlg = new Model.EquipmentForm();
            dlg.objEquipment = obj;
            dlg.ShowDialog();
            loadEquipmentList();
        }

        private void metroGrid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (metroGridEquipmentList.Columns[e.ColumnIndex].Name.Equals("sts"))
            {
                Int32 intValue;
                if (Int32.TryParse((String)e.Value, out intValue) &&
                    (intValue == 0))
                {
                    //e.CellStyle.BackColor = Color.Red;
                    e.Value = "Deactive";
                }
                else
                {
                    //e.CellStyle.BackColor = Color.Blue;
                    e.Value = "Active";
                }

            }

        }

        private void metroGridEquipmentConstant_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void metroGridEquipmentConstant_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (metroGridEquipmentConstant.Columns[e.ColumnIndex].Name.Equals("sts"))
            {
                Int32 intValue;
                if (Int32.TryParse((String)e.Value, out intValue) &&
                    (intValue == 0))
                {
                    //e.CellStyle.BackColor = Color.Red;
                    e.Value = "Deactive";
                }
                else
                {
                    //e.CellStyle.BackColor = Color.Blue;
                    e.Value = "Active";
                }

            }
            //
            if (metroGridEquipmentConstant.Columns[e.ColumnIndex].Name.Equals("dv_value"))
            {
                if (metroGridEquipmentConstant.Rows[e.RowIndex].Cells["dv_datatype"].Value == null)
                {
                    //
                    //
                }
                else
                {
                    int dv_datatype = Convert.ToInt16(metroGridEquipmentConstant.Rows[e.RowIndex].Cells["dv_datatype"].Value.ToString());
                    if (dv_datatype == 4 || dv_datatype == 5 || dv_datatype == 6)
                    {
                        e.Value = Convert.ToString(metroGridEquipmentConstant.Rows[e.RowIndex].Cells["dv_valuetext"].Value.ToString());
                    }
                    else
                    {
                        e.Value = Convert.ToString(metroGridEquipmentConstant.Rows[e.RowIndex].Cells["dv_value"].Value.ToString());
                    }
                }
            }
        }

        public static MySqlConnection getConnection()
        {
            try
            {
                if (conn != null)
                {
                    return conn;
                }
                conn = new MySqlConnection(Main.connString);
                conn.Open();
                Console.WriteLine("Connected");
                return conn;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return null;
        }

        private void Main_Deactivate(object sender, EventArgs e)
        {
            if (conn != null)
            {
                //conn.Close();
            }
        }

        private void metroGridEquipmentStatusVariable_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void metroGridEquipmentConstant_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void metroGridEquipmentDataVariable_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            gemStatusRefresh();
            
        }

        private void gemStatusRefresh()
        {
            Eqpdatavariable _GEM_ControlState = null; // Eqpdatavariable.loadUUID("70");
            Eqpdatavariable _GEM_CommunicationsState = null; // Eqpdatavariable.loadUUID("73");
            if (_GEM_ControlState == null || _GEM_CommunicationsState == null)
            {
                return;
            }
            //
            int _GEM_ControlState_dv_value = Convert.ToInt16(_GEM_ControlState.dv_value);
            if (_GEM_ControlState_dv_value == 1)
            {
                // GEM OFFLINE
                controlStateOFFLINE.BackColor = Color.Green;
                controlStateONLINE_LOCAL.BackColor = Color.White;
                controlStateONLINE_REMOTE.BackColor = Color.White;
                //
            }
            else if (_GEM_ControlState_dv_value == 2)
            {
                // GEM OFFLINE
                controlStateOFFLINE.BackColor = Color.White;
                controlStateONLINE_LOCAL.BackColor = Color.Green;
                controlStateONLINE_REMOTE.BackColor = Color.White;
                //
            }
            else if (_GEM_ControlState_dv_value == 3)
            {
                // GEM OFFLINE
                controlStateOFFLINE.BackColor = Color.White;
                controlStateONLINE_LOCAL.BackColor = Color.White;
                controlStateONLINE_REMOTE.BackColor = Color.Green;
                //
            }
            //
            //
            int _GEM_CommunicationsState_value = Convert.ToInt16(_GEM_CommunicationsState.dv_value);
            if (_GEM_CommunicationsState_value == 1)
            {
                // GEM OFFLINE
                controlStateSTART.BackColor = Color.Green;
                controlStateSTOP.BackColor = Color.White;
                controlStateRESUME.BackColor = Color.White;
                controlStateABORT.BackColor = Color.White;
                controlStatePAUSE.BackColor = Color.White;
                //
            }
            else if (_GEM_CommunicationsState_value == 2)
            {
                // GEM OFFLINE
                controlStateSTART.BackColor = Color.White;
                controlStateSTOP.BackColor = Color.Green;
                controlStateRESUME.BackColor = Color.White;
                controlStateABORT.BackColor = Color.White;
                controlStatePAUSE.BackColor = Color.White;
                //
            }
            else if (_GEM_CommunicationsState_value == 3)
            {
                // GEM OFFLINE
                controlStateSTART.BackColor = Color.White;
                controlStateSTOP.BackColor = Color.White;
                controlStateRESUME.BackColor = Color.Green;
                controlStateABORT.BackColor = Color.White;
                controlStatePAUSE.BackColor = Color.White;
                //
            }
            else if (_GEM_CommunicationsState_value == 4)
            {
                // GEM OFFLINE
                controlStateSTART.BackColor = Color.White;
                controlStateSTOP.BackColor = Color.White;
                controlStateRESUME.BackColor = Color.White;
                controlStateABORT.BackColor = Color.Green;
                controlStatePAUSE.BackColor = Color.White;
                //
            }
            else if (_GEM_CommunicationsState_value == 5)
            {
                // GEM OFFLINE
                controlStateSTART.BackColor = Color.White;
                controlStateSTOP.BackColor = Color.White;
                controlStateRESUME.BackColor = Color.White;
                controlStateABORT.BackColor = Color.White;
                controlStatePAUSE.BackColor = Color.Green;
                //
            }
            //
        }

        private void metroGridListEvent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // LIST REPORT
            int index = e.RowIndex;
            if (!(index < 0))
            {
                DataGridViewRow row = metroGridLinkedEvent.Rows[index];
                objSelectedEvent = new Eqpevent(row);
                txt_ee_uuid.Text = objSelectedEvent.ee_uuid;
                txt_ee_name.Text = objSelectedEvent.ee_eventname;
                txt_ee_sts.Checked = objSelectedEvent.sts==1;
                //
                loadLinkedReport(objSelectedEvent.ee_id);
                btnDELETEEE.Text = "DELETE";
                btnUPDATEEE.Text = "UPDATE";
                //
            }
            else
            {
                metroGridLinkedReport.DataSource = null;
                btnDELETEEE.Text = "NEW";
                btnUPDATEEE.Text = "INSERT";
            }
        }

        private void metroGridLinkedEvent_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // EDIT
        }

        private void metroGridLinkedReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnUPDATEEE_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_ee_uuid.Text))
            {
                MessageBox.Show("Event ID empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ee_uuid.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_ee_name.Text))
            {
                MessageBox.Show("Event Name empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ee_name.Focus();
                return;
            }

            if (objSelectedEvent != null && objSelectedEvent.ee_id>0)
            {
                objSelectedEvent.ee_eventname = txt_ee_name.Text;
                objSelectedEvent.ee_uuid = txt_ee_uuid.Text;
                objSelectedEvent.sts = txt_ee_sts.Checked == true ? 1 : 0;
                // Update DB
                objSelectedEvent.update();
                // Reload Event list
                loadEvent();
            }
            else
            {
                objSelectedEvent = new Eqpevent();
                objSelectedEvent.ee_eventname = txt_ee_name.Text;
                objSelectedEvent.ee_uuid = txt_ee_uuid.Text;
                objSelectedEvent.sts = txt_ee_sts.Checked == true ? 1 : 0;
                // Update DB
                objSelectedEvent.insert();
                // Reload Event list
                loadEvent();
            }
        }
        private void btnDELETEEE_Click(object sender, EventArgs e)
        {
            if (objSelectedEvent != null && objSelectedEvent.ee_id > 0)
            {
                DialogResult dr = MessageBox.Show("Delete event??", "Confirm delete event", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    // Update DB
                    objSelectedEvent.delete();
                    // Reload Event list
                    loadEvent();
                }
            }
        }
        private void btnNEWEE_Click(object sender, EventArgs e)
        {
            btnDELETEEE.Text = "DELETE";
            btnUPDATEEE.Text = "INSERT";
            txt_ee_uuid.Text = "";
            txt_ee_name.Text = "";
            txt_ee_sts.Checked = true;
            // Reload Event list
        }

        private void btnNEWER_Click(object sender, EventArgs e)
        {
            if (objSelectedReport != null && objSelectedReport.er_id > 0)
            {
                DialogResult dr = MessageBox.Show("Delete event??", "Confirm delete report", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    // Update DB
                    objSelectedReport.delete();
                    // Reload Event list
                    loadLinkedReport(objSelectedEvent.ee_id);
                    btnNEWER.Text = "NEW";
                    btnUPDATEER.Text = "INSERT";
                }
            }
            else
            {
                if (btnNEWER.Text == "NEW")
                {
                    btnNEWER.Text = "CREATE LINK";
                    btnUPDATEER.Text = "INSERT";
                    txt_er_uuid.Text = "";
                    txt_er_name.Text = "";
                    txt_er_sts.Checked = true;
                    // Reload Event list
                }
                else
                {
                    // create new report, and link to event
                    objSelectedReport = new Eqpreport();
                    objSelectedReport.er_reportname = txt_er_name.Text;
                    objSelectedReport.er_uuid = txt_er_uuid.Text;
                    objSelectedReport.sts = txt_er_sts.Checked == true ? 1 : 0;
                    // Update DB
                    objSelectedReport.insert();
                    // link to event
                    string query = "insert into linkevent2report values('" + objSelectedEvent.ee_id + "','" + objSelectedReport.er_id + "','1','1')";
                    objSelectedReport.query(query);
                    // create new report, and link to event
                    btnNEWER.Text = "NEW";
                    btnUPDATEER.Text = "INSERT";
                    // Reload report list
                    loadLinkedReport(objSelectedEvent.ee_id);
                    // reload combo list
                    loadCbListreport();
                    //
                }
            }
        }

        private void btnUPDATEER_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_er_uuid.Text))
            {
                MessageBox.Show("Report ID empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_er_uuid.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_er_name.Text))
            {
                MessageBox.Show("Report name empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_er_name.Focus();
                return;
            }

            if (objSelectedReport != null && objSelectedReport.er_id > 0)
            {
                objSelectedReport.er_reportname = txt_er_name.Text;
                objSelectedReport.er_uuid = txt_er_uuid.Text;
                objSelectedReport.sts = txt_er_sts.Checked == true ? 1 : 0;
                // Update DB
                objSelectedReport.update();
                // Reload Event list
                loadLinkedReport(objSelectedEvent.ee_id);
            }
            else
            {
                objSelectedReport = new Eqpreport();
                objSelectedReport.er_reportname = txt_er_name.Text;
                objSelectedReport.er_uuid = txt_er_uuid.Text;
                objSelectedReport.sts = txt_ee_sts.Checked == true ? 1 : 0;
                // Update DB
                objSelectedReport.insert();
                // Reload report list
                loadLinkedReport(objSelectedEvent.ee_id);
                // reload combo list
                loadCbListreport();
                //
            }
            //
            //
        }

        public void loadCbListreport()
        {
            Dictionary<int, string> mapSecsdata = new Dictionary<int, string>();
            List<Eqpreport> listEqpreport = Eqpreport.load();
            foreach (Eqpreport obj in listEqpreport)
            {
                mapSecsdata.Add(obj.er_id, obj.er_uuid+"_"+obj.er_reportname);
            }
            cbListReport.DataSource = new BindingSource(mapSecsdata, null);
            cbListReport.DisplayMember = "Value";
            cbListReport.ValueMember = "Key";
            //
        }
        public void loadCbListVariable()
        {
            Dictionary<int, string> mapVariable = new Dictionary<int, string>();
            List<Eqpdatavariable> listEqpVariable = Eqpdatavariable.load();
            foreach (Eqpdatavariable obj in listEqpVariable)
            {
                if (obj.dv_type == 1)
                {
                    // EQP const
                    continue;
                }
                else if (obj.dv_type == 2)
                {
                    mapVariable.Add(obj.dv_id, "SVID_"+obj.dv_uuid + "_" + obj.dv_name);
                }
                else if (obj.dv_type == 3)
                {
                    mapVariable.Add(obj.dv_id, "DVID_" + obj.dv_uuid + "_" + obj.dv_name);
                }
            }
            cbListDVID.DataSource = new BindingSource(mapVariable, null);
            cbListDVID.DisplayMember = "Value";
            cbListDVID.ValueMember = "Key";
            //
        }
        private void metroButton2_Click(object sender, EventArgs e)
        {
            //
            if (objSelectedEvent == null || objSelectedEvent.ee_id == 0)
            {
                return;
            } 
            
            int er_id = (((KeyValuePair<int, string>)cbListReport.SelectedItem).Key);
            if (objSelectedEvent != null)
            {
                // link to event
                string query = "insert into linkevent2report values('" + objSelectedEvent.ee_id + "','" + er_id + "','1','1')";
                objSelectedEvent.query(query);
                // Reload report list
                loadLinkedReport(objSelectedEvent.ee_id);
            }
        }

        private void btnNewVariable_Click(object sender, EventArgs e)
        {
            View.EqpdatavariableForm dlg = new View.EqpdatavariableForm();
            dlg.objEqpdatavariable = new Eqpdatavariable();
            dlg.objEqpdatavariable.dv_type = 2;
            dlg.ShowDialog();
            loadCbListVariable();
        }

        private void btnAddVariableToReport_Click(object sender, EventArgs e)
        {
            //
            if (objSelectedReport == null || objSelectedReport.er_id == 0)
            {
                return;
            }

            int dv_id = (((KeyValuePair<int, string>)cbListDVID.SelectedItem).Key);
            if (objSelectedReport != null)
            {
                // link to event
                string query = "insert into linkreport2variable values('" + objSelectedReport.er_id + "','" + dv_id + "','1','1')";
                objSelectedReport.query(query);
                // Reload report list
                loadLinkedVariable(objSelectedReport.er_id);
            }
        }

        private void metroGridlinkedVariable_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            

        }

        private void metroGridEquipmentConstant_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void metroGridEquipmentConstant_KeyPress(object sender, KeyPressEventArgs eventArgs)
        {
            
        }

        private void metroGridEquipmentConstant_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                int Count = metroGridEquipmentConstant.SelectedRows.Count;
                if (Count > 0)
                {
                    DataGridViewRow row = metroGridEquipmentConstant.SelectedRows[0];
                    Eqpdatavariable obj = new Eqpdatavariable(row);
                    DialogResult dr = MessageBox.Show("Delete equipment constant data ["+obj.dv_uuid+"]??", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        // Update DB
                        obj.delete();
                        //
                        loadEquipmentConst();
                        //
                    }
                }
            }

        }

        private void metroGridLinkedReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                int Count = metroGridLinkedReport.SelectedRows.Count;
                if (Count > 0)
                {
                    DataGridViewRow row = metroGridLinkedReport.SelectedRows[0];
                    Eqpreport obj = new Eqpreport(row);
                    //
                    DialogResult dr = MessageBox.Show("Remove link this report to event ["+obj.er_uuid+"]??", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        // Update DB
                        obj.query("delete from linkevent2report where ee_id='" + objSelectedEvent.ee_id + "' and er_id='" + obj.er_id+"'");
                        //
                        loadLinkedReport(objSelectedEvent.ee_id);
                        //
                    }
                }
            }
        }

        private void metroGridlinkedVariable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                int Count = metroGridlinkedVariable.SelectedRows.Count;
                if (Count > 0)
                {
                    DataGridViewRow row = metroGridlinkedVariable.SelectedRows[0];
                    Eqpdatavariable obj = new Eqpdatavariable(row);
                    //
                    DialogResult dr = MessageBox.Show("Remove link this variable to report ["+obj.dv_uuid+"]?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        // Update DB
                        obj.query("delete from linkreport2variable where er_id='" + objSelectedReport.er_id + "' and dv_id='" + obj.dv_id + "'");
                        //
                        loadLinkedVariable(objSelectedReport.er_id);
                        //
                    }
                }
            }
        }

        private void btnUPDATEALARM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_ea_uuid.Text))
            {
                MessageBox.Show("Alarm ID empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ea_uuid.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txt_ea_name.Text))
            {
                MessageBox.Show("Alarm name empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ea_name.Focus();
                return;
            }
            
            if (btnUPDATEALARM.Text == "CREATE NEW")
            {
                EqpAlarm obj = new EqpAlarm();
                obj.ea_uuid = txt_ea_uuid.Text;
                obj.ea_name = txt_ea_name.Text;
                obj.sts = 1;
                obj.eqp_id = 1;
                obj.insert();
                //
                objSelectedAlarm = obj;
                //
                btnUPDATEALARM.Text = "UPDATE";
                btnDeleteAlarm.Text = "DELETE";
                loadAlarm();
            }
            else
            {
                if (objSelectedAlarm.ea_id > 0)
                {
                    objSelectedAlarm.ea_uuid = txt_ea_uuid.Text;
                    objSelectedAlarm.ea_name = txt_ea_name.Text;
                    objSelectedAlarm.update();
                    //
                    btnUPDATEALARM.Text = "UPDATE";
                    btnDeleteAlarm.Text = "DELETE";
                    //
                    loadAlarm();
                }
            }
        }

        private void metroGridEquipmentAlarm_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        public static int getUserId()
        {
            return currentUser.user_id;
        }

        private void metroGridEquipmentAlarm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        internal static void setLoginUser(Users users)
        {
            Main.currentUser = users;
        }

        public static Users currentUser { get; set; }

        private void metroPanel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSTART_Click(object sender, EventArgs e)
        {
            try
            {
                int rc = startSecsDriver("gem/secsdriver.ini");
                if (rc == 1)
                {
                    MessageBox.Show("Check licence fail.");
                }
                else
                {
                    btnSTART.Enabled = false;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void metroGridEquipmentConstant_Click(object sender, EventArgs e)
        {
            if (metroGridEquipmentConstant.SelectedRows.Count>0)
            {
                DataGridViewRow row = metroGridEquipmentConstant.SelectedRows[0];
                Eqpdatavariable obj = new Eqpdatavariable(row);
                View.EqpdatavariableForm dlg = new View.EqpdatavariableForm();
                dlg.objEqpdatavariable = obj;
                dlg.ShowDialog();
                //
                if (dlg.DialogResult == DialogResult.OK)
                {
                    loadEquipmentConst();
                }
                //
            }           
        }

        private void metroGridEquipmentStatusVariable_Click(object sender, EventArgs e)
        {
            if (metroGridEquipmentStatusVariable.SelectedRows.Count > 0)
            {
                DataGridViewRow row = metroGridEquipmentStatusVariable.SelectedRows[0];
                Eqpdatavariable obj = new Eqpdatavariable(row);
                View.EqpdatavariableForm dlg = new View.EqpdatavariableForm();
                dlg.objEqpdatavariable = obj;
                dlg.ShowDialog();
                //
                if (dlg.DialogResult == DialogResult.OK)
                {
                    loadEquipmentStatusDataVariavle();
                }
                //
            }
        }

        private void metroGridEquipmentDataVariable_Click(object sender, EventArgs e)
        {
            if (metroGridEquipmentDataVariable.SelectedRows.Count > 0)
            {
                DataGridViewRow row = metroGridEquipmentDataVariable.SelectedRows[0];
                Eqpdatavariable obj = new Eqpdatavariable(row);
                View.EqpdatavariableForm dlg = new View.EqpdatavariableForm();
                dlg.objEqpdatavariable = obj;
                dlg.ShowDialog();
                //
                if (dlg.DialogResult == DialogResult.OK)
                {
                    loadEquipmentDataVariavle();
                }
                //
            }
        }

        private void metroGridEquipmentAlarm_Click(object sender, EventArgs e)
        {
            int Count = metroGridEquipmentAlarm.SelectedRows.Count;
            if (Count > 0)
            {
                DataGridViewRow row = metroGridEquipmentAlarm.SelectedRows[0];
                objSelectedAlarm = new EqpAlarm(row);
                //
                txt_ea_name.Text = objSelectedAlarm.ea_name;
                txt_ea_uuid.Text = objSelectedAlarm.ea_uuid;
                //
                btnUPDATEALARM.Text = "UPDATE";
                btnDeleteAlarm.Text = "DELETE";
                //
            }
        }

        private void metroGridLinkedEvent_Click(object sender, EventArgs e)
        {
            // LIST REPORT
            if (metroGridLinkedEvent.SelectedRows.Count > 0)
            {
                DataGridViewRow row = metroGridLinkedEvent.SelectedRows[0];
                objSelectedEvent = new Eqpevent(row);
                txt_ee_uuid.Text = objSelectedEvent.ee_uuid;
                txt_ee_name.Text = objSelectedEvent.ee_eventname;
                txt_ee_sts.Checked = objSelectedEvent.sts == 1;
                //
                loadLinkedReport(objSelectedEvent.ee_id);
                btnDELETEEE.Text = "DELETE";
                btnUPDATEEE.Text = "UPDATE";
                //
            }
            else
            {
                metroGridLinkedReport.DataSource = null;
                btnDELETEEE.Text = "NEW";
                btnUPDATEEE.Text = "INSERT";
            }
            /*
            // LIST LINKED VARIABLE
            if (metroGridLinkedEvent.SelectedRows.Count > 0)
            {
                DataGridViewRow row = metroGridLinkedEvent.SelectedRows[0];
                objSelectedReport = new Eqpreport(row);
                //
                //
                txt_er_uuid.Text = objSelectedReport.er_uuid;
                txt_er_name.Text = objSelectedReport.er_reportname;
                txt_er_sts.Checked = objSelectedReport.sts == 1;
                //
                loadLinkedVariable(objSelectedReport.er_id);
                //
                btnNEWER.Text = "DELETE";
                btnUPDATEER.Text = "UPDATE";
            }
            else
            {
                metroGridlinkedVariable.DataSource = null;
            }
             */ 
        }

        private void metroGridLinkedReport_Click(object sender, EventArgs e)
        {
            if (metroGridLinkedReport.SelectedRows.Count > 0)
            {
                DataGridViewRow row = metroGridLinkedReport.SelectedRows[0];
                objSelectedReport = new Eqpreport(row);
                //
                //
                txt_er_uuid.Text = objSelectedReport.er_uuid;
                txt_er_name.Text = objSelectedReport.er_reportname;
                txt_er_sts.Checked = objSelectedReport.sts == 1;
                //
                loadLinkedVariable(objSelectedReport.er_id);
                //
                btnNEWER.Text = "DELETE";
                btnUPDATEER.Text = "UPDATE";
            }
            else
            {
                metroGridlinkedVariable.DataSource = null;
            }
        }

        private void metroGridlinkedVariable_Click(object sender, EventArgs e)
        {
            int Count = metroGridlinkedVariable.SelectedRows.Count;
            if (Count > 0)
            {
                DataGridViewRow row = metroGridlinkedVariable.SelectedRows[0];
                Eqpdatavariable obj = new Eqpdatavariable(row);
                View.EqpdatavariableForm dlg = new View.EqpdatavariableForm();
                dlg.objEqpdatavariable = obj;
                dlg.ShowDialog();
                //
                loadLinkedVariable(objSelectedReport.er_id);
                //
            }
        }

        private void metroGridLinkedEvent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                metroGridLinkedReport.DataSource = null;
                btnDELETEEE.Text = "NEW";
                btnUPDATEEE.Text = "INSERT";
            }
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gemStatusRefresh();
        }

        private void btnNEWEA_Click(object sender, EventArgs e)
        {
            txt_ea_uuid.Text = "";
            txt_ea_name.Text = "";
            //
            btnUPDATEALARM.Text = "CREATE NEW";
            //
        }

        private void btnDeleteAlarm_Click(object sender, EventArgs e)
        {
            if (objSelectedAlarm.ea_id > 0)
            {
                txt_ea_uuid.Text = "";
                txt_ea_name.Text = "";
                //
                objSelectedAlarm.delete();
                loadAlarm();
                //
                btnUPDATEALARM.Text = "UPDATE";
            }
        }

        private void metroGridEquipmentConstant_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCONNECT_Click(object sender, EventArgs e)
        {
            if (btnCONNECT.Text == "DISCONNECT")
            {
                disconnectSecsdriver();
            }
            else
            {
                connectSecsdriver();
            }
        }
        private void disconnectSecsdriver()
        {
            if (sck != null)
            {
                try
                {
                    logScreen("Disconnecting...");
                    sck.Close();
                    logScreen("Disconnected");
                    btnCONNECT.Text = "CONNECT";
                }
                catch (Exception ex)
                {
                    logScreen("ERROR: " + ex.Message);
                    //e.Message;
                }
            }
        }
        private void connectSecsdriver()
        {
            String hostName = txtHost.Text;
            int port = Convert.ToInt32(txtPort.Text);
            if (port > 1000)
            {
                try
                {
                    listBox1.Items.Clear();
                    logScreen("Begin connect to " + hostName + ":" + port);
                    //setup socket
                    IPAddress ip = IPAddress.Parse(hostName);
                    sck = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

                    //binding socket
                    remoteEp = new IPEndPoint(IPAddress.Parse(hostName), port);
                    sck.Connect(remoteEp);
                    //client = new TcpClient(hostName, port);
                    logScreen("Connected");
                    btnCONNECT.Text = "DISCONNECT";
                    buffer = new byte[4000];
                    sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEp,
                        new AsyncCallback(MessageCallBack), buffer);

                }
                catch (Exception ex)
                {
                    logScreen("ERROR: " + ex.Message);
                }

            }
        }
        private void MessageCallBack(IAsyncResult ar)
        {
            try
            {
                byte[] receiveData = (byte[])ar.AsyncState;
                ASCIIEncoding ascencoding = new ASCIIEncoding();
                string response = ascencoding.GetString(receiveData);
                int i = response.IndexOf('\0');
                if (i >= 0) response = response.Substring(0, i);
                //Adding message to listbox
                if (response == "ping")
                {
                    sendData("pong");
                }
                else
                {
                    logScreen(response);

                }
                buffer = new byte[4000];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None,
                    ref remoteEp, new AsyncCallback(MessageCallBack), buffer);
            }
            catch (Exception e)
            {
                logScreen(e.ToString());
            }
        }

        private void sendData(string text)
        {
            if (sck.Connected == true)
            {
                int byteCount = Encoding.ASCII.GetByteCount(text);
                //byte[] sendData = new byte[byteCount];
                byte[] bytes = Encoding.ASCII.GetBytes(text);
                int lengthByte = text.Length;
                byte[] intBytes = BitConverter.GetBytes(lengthByte);
                byte[] intBytesSend = intBytes;
                sck.Send(intBytesSend, 0, 4, 0);
                sck.Send(bytes, lengthByte, 0);
                if (text != "pong")
                {

                    logScreen("SEND: " + text);
                }
            }
        }
        public void logScreen(string str)
        {
            if (InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    listBox1.Items.Add(DateTime.Now.ToString() + " " + str);
                    //Your code here, like set text box content or get text box contents etc..
                }));
            }
            else
            {
                // Your code here, like set text box content or get text box contents etc..
                // SAME CODE AS ABOVE
                listBox1.Items.Add(DateTime.Now.ToString() + " " + str);

            }
            //throw new NotImplementedException();
        }

        private void btnSEND_ALARM_Click(object sender, EventArgs e)
        {
            // SEND ALARM S5F1.
            // Send request to secsdriver in equipment to send S5F1

            try
            {
                string msgS5F1 = "s5f1 = S5F1_S w output < l[3] "+
                                "< b[1] 0 >"+
                                "< u4[1] 1 > "+
                                "< a[40] 'neo' > "+
                               ">.";
                msgS5F1 = HttpUtility.JavaScriptStringEncode(msgS5F1);
                String jsonString = "";
                jsonString += "{";
                jsonString += "\"reqname\":\"" + "SEND_SML" + "\"";
                jsonString += ", \"params\":[";
                jsonString += "{\"" + "SML" + "\":\"" + msgS5F1 + "\"}";
                jsonString += ",";
                jsonString += "]";
                jsonString += "}";

                //var x = JsonConvert.DeserializeObject(jsonString);
                //String jsonStringSend = JsonConvert.SerializeObject(x, Formatting.Indented);
                sendData(jsonString);
            }
            catch (Exception ex)
            {
                logScreen(ex.Message);
            }
            //
        }
    }
}