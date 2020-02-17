﻿/*
 * C# class for entity table eqpdatavariable 
 * Created on 18 Jan 2018 ( Date ISO 2018-01-18 - Time 19:47:35 )
 * Generated by Telosys Tools Generator ( version 2.1.1 )
 * template update by NEO
 */
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Reflection;
using log4net;

/**
 * Entity bean for table "eqpdatavariable"
 * 
 * @author Telosys Tools Generator
 *
 */
namespace SecsGemDriver
{
    public class Eqpdatavariable
    {
        //Declare an instance for log4net
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int dv_id { get; set; }

        public int eqp_id { get; set; }
        public int dv_type { get; set; }
        public string dv_uuid { get; set; }
        public string dv_name { get; set; }
        public string dv_unit { get; set; }
        public string dv_limitmin { get; set; }
        public string dv_limitmax { get; set; }
        public string dv_default { get; set; }
        public int dv_datatype { get; set; }
        public string dv_secs { get; set; }
        public string dv_value { get; set; }
        public string dv_valuetext { get; set; }
        public string dv_updateby { get; set; }
        public int dv_lastupdate { get; set; }
        public int sts { get; set; }

        public Eqpdatavariable()
        {
        }

        /*
         * CRUD functions
         */
        public static List<Eqpdatavariable> load()
        {
            return load("");
        }
        public static List<Eqpdatavariable> load(string query)
        {
            List<Eqpdatavariable> list = new List<Eqpdatavariable>();
            MySqlDataReader rd = null;
            try
            {
                MySqlConnection conn = Main.getConnection();
                if (conn == null)
                {
                    return list;
                }
                if (query == null || query.Length == 0)
                {
                    query = "select * from eqpdatavariable";
                }
                Log.Info("Query: " + query);
                MySqlCommand cmd = new MySqlCommand(query, conn);

                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Eqpdatavariable obj = new Eqpdatavariable();
                    obj.dv_id = Convert.ToInt32(rd["dv_id"].ToString());   // Primary
                    obj.eqp_id = Convert.ToInt32(rd["eqp_id"].ToString());
                    obj.dv_type = Convert.ToInt32(rd["dv_type"].ToString());
                    obj.dv_uuid = rd["dv_uuid"].ToString();
                    obj.dv_name = rd["dv_name"].ToString();
                    obj.dv_unit = rd["dv_unit"].ToString();
                    obj.dv_limitmin = rd["dv_limitmin"].ToString();
                    obj.dv_limitmax = rd["dv_limitmax"].ToString();
                    obj.dv_default = rd["dv_default"].ToString();
                    obj.dv_datatype = Convert.ToInt32(rd["dv_datatype"].ToString());
                    obj.dv_secs = rd["dv_secs"].ToString();
                    obj.dv_value = rd["dv_value"].ToString();
                    obj.dv_valuetext = rd["dv_valuetext"].ToString();
                    obj.dv_updateby = rd["dv_updateby"].ToString();
                    obj.dv_lastupdate = Convert.ToInt32(rd["dv_lastupdate"].ToString());
                    obj.sts = Convert.ToInt32(rd["sts"].ToString());
                    list.Add(obj);
                }
                rd.Close();
            }
            catch (MySqlException e)
            {
                Log.Error("Error: " + e.Message);
            }
            finally
            {
                if (rd != null)
                {
                    rd.Close();
                }
            }
            return list;
        }
        public void query(string query)
        {
            try
            {
                MySqlConnection conn = Main.getConnection();
                if (conn == null)
                {
                    return;
                }
                Log.Info("Query: " + query);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                // Log file
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                string queryLog = "insert into action_log(log_text, log_time, user_id) values('" + query + "','" + unixTimestamp + "','" + Main.getUserId() + "')";
                MySqlCommand cmdLog = new MySqlCommand(queryLog, conn);
                cmdLog.ExecuteNonQuery();
                // End logfile
            }
            catch (MySqlException e)
            {
                Log.Error("Error: " + e.Message);
            }

            return;
        }

        public static Eqpdatavariable load(int dv_id)
        {
            MySqlDataReader rd = null;
            try
            {
                MySqlConnection conn = Main.getConnection();
                if (conn == null)
                {
                    return null;
                }
                string query = "select * from eqpdatavariable where dv_id='" + dv_id + "'";
                Log.Info("Query: " + query);
                MySqlCommand cmd = new MySqlCommand(query, conn);

                rd = cmd.ExecuteReader();
                Eqpdatavariable obj = new Eqpdatavariable();

                while (rd.Read())
                {
                    obj.dv_id = Convert.ToInt32(rd["dv_id"].ToString());   // Primary
                    obj.eqp_id = Convert.ToInt32(rd["eqp_id"].ToString());
                    obj.dv_type = Convert.ToInt32(rd["dv_type"].ToString());
                    obj.dv_uuid = rd["dv_uuid"].ToString();
                    obj.dv_name = rd["dv_name"].ToString();
                    obj.dv_unit = rd["dv_unit"].ToString();
                    obj.dv_limitmin = rd["dv_limitmin"].ToString();
                    obj.dv_limitmax = rd["dv_limitmax"].ToString();
                    obj.dv_default = rd["dv_default"].ToString();
                    obj.dv_datatype = Convert.ToInt32(rd["dv_datatype"].ToString());
                    obj.dv_secs = rd["dv_secs"].ToString();
                    obj.dv_value = rd["dv_value"].ToString();
                    obj.dv_valuetext = rd["dv_valuetext"].ToString();
                    obj.dv_updateby = rd["dv_updateby"].ToString();
                    obj.dv_lastupdate = Convert.ToInt32(rd["dv_lastupdate"].ToString());
                    obj.sts = Convert.ToInt32(rd["sts"].ToString());
                    break;
                }
                rd.Close();

                return obj;
            }
            catch (MySqlException e)
            {
                Log.Error("Query: " + e.Message);
            }
            finally
            {
                if (rd != null)
                {
                    rd.Close();
                }
            }
            return null;
        }


        public static Eqpdatavariable loadUUID(string uuid)
        {
            MySqlDataReader rd = null;
            try
            {
                MySqlConnection conn = Main.getConnection();
                if (conn == null)
                {
                    return null;
                }
                string query = "select * from eqpdatavariable where dv_uuid='" + uuid + "'";
                Log.Info("Query: " + query);
                MySqlCommand cmd = new MySqlCommand(query, conn);

                rd = cmd.ExecuteReader();
                Eqpdatavariable obj = new Eqpdatavariable();

                while (rd.Read())
                {
                    obj.dv_id = Convert.ToInt32(rd["dv_id"].ToString());   // Primary
                    obj.eqp_id = Convert.ToInt32(rd["eqp_id"].ToString());
                    obj.dv_type = Convert.ToInt32(rd["dv_type"].ToString());
                    obj.dv_uuid = rd["dv_uuid"].ToString();
                    obj.dv_name = rd["dv_name"].ToString();
                    obj.dv_unit = rd["dv_unit"].ToString();
                    obj.dv_limitmin = rd["dv_limitmin"].ToString();
                    obj.dv_limitmax = rd["dv_limitmax"].ToString();
                    obj.dv_default = rd["dv_default"].ToString();
                    obj.dv_datatype = Convert.ToInt32(rd["dv_datatype"].ToString());
                    obj.dv_secs = rd["dv_secs"].ToString();
                    obj.dv_value = rd["dv_value"].ToString();
                    obj.dv_valuetext = rd["dv_valuetext"].ToString();
                    obj.dv_updateby = rd["dv_updateby"].ToString();
                    obj.dv_lastupdate = Convert.ToInt32(rd["dv_lastupdate"].ToString());
                    obj.sts = Convert.ToInt32(rd["sts"].ToString());
                    break;
                }
                rd.Close();

                return obj;
            }
            catch (MySqlException e)
            {
                Log.Error("Query: " + e.Message);
            }
            finally
            {
                if (rd != null)
                {
                    rd.Close();
                }
            }

            return null;
        }

        public String toString()
        {
            String strData = "eqpdatavariable "
                    + " dv_id = " + dv_id + "; eqp_id = " + eqp_id
                    + "; dv_type = " + dv_type
                    + "; dv_uuid = " + dv_uuid
                    + "; dv_name = " + dv_name
                    + "; dv_unit = " + dv_unit
                    + "; dv_limitmin = " + dv_limitmin
                    + "; dv_limitmax = " + dv_limitmax
                    + "; dv_default = " + dv_default
                    + "; dv_datatype = " + dv_datatype
                    + "; dv_secs = " + dv_secs
                    + "; dv_value = " + dv_value
                    + "; dv_valuetext = " + dv_valuetext
                    + "; dv_updateby = " + dv_updateby
                    + "; dv_lastupdate = " + dv_lastupdate
                    + "; sts = " + sts
                    ;
            return strData;
        }

        public void insert()
        {
            //
            try
            {
                MySqlConnection conn = Main.getConnection();
                if (conn == null)
                {
                    return;
                }
                string query = "insert into eqpdatavariable(" +
                                    "eqp_id," +
                                    "dv_type," +
                                    "dv_uuid," +
                                    "dv_name," +
                                    "dv_unit," +
                                    "dv_limitmin," +
                                    "dv_limitmax," +
                                    "dv_default," +
                                    "dv_datatype," +
                                    "dv_secs," +
                                    "dv_value," +
                                    "dv_valuetext," +
                                    "dv_updateby," +
                                    "dv_lastupdate," +
                                    "sts" +
" )values (" +
                                    "'" + eqp_id + "'," +
                                    "'" + dv_type + "'," +
                                    "'" + dv_uuid + "'," +
                                    "'" + dv_name + "'," +
                                    "'" + dv_unit + "'," +
                                    "'" + dv_limitmin + "'," +
                                    "'" + dv_limitmax + "'," +
                                    "'" + dv_default + "'," +
                                    "'" + dv_datatype + "'," +
                                    "'" + dv_secs + "'," +
                                    "'" + dv_value + "'," +
                                    "'" + dv_valuetext + "'," +
                                    "'" + dv_updateby + "'," +
                                    "'" + dv_lastupdate + "'," +
                                    "'" + sts + "'" +
" )";
                Log.Info("INSERT: " + query);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                this.dv_id = (int)cmd.LastInsertedId;
                // Log file
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                string log_text = "Create new eqpdatavariable. ID=" + this.dv_id;
                string queryLog = "insert into action_log(log_text, log_time, user_id) values('" + log_text + "','" + unixTimestamp + "','" + Main.getUserId() + "')";
                MySqlCommand cmdLog = new MySqlCommand(queryLog, conn);
                cmdLog.ExecuteNonQuery();
                // End logfile
            }
            catch (MySqlException e)
            {
                Log.Error("Error: " + e.Message);
            }
        }

        public void update()
        {
            try
            {
                MySqlConnection conn = Main.getConnection();
                if (conn == null)
                {
                    return;
                }
                string query = "update eqpdatavariable set " +
                                "eqp_id='" + eqp_id + "' ," +
                                "dv_type='" + dv_type + "' ," +
                                "dv_uuid='" + dv_uuid + "' ," +
                                "dv_name='" + dv_name + "' ," +
                                "dv_unit='" + dv_unit + "' ," +
                                "dv_limitmin='" + dv_limitmin + "' ," +
                                "dv_limitmax='" + dv_limitmax + "' ," +
                                "dv_default='" + dv_default + "' ," +
                                "dv_datatype='" + dv_datatype + "' ," +
                                "dv_secs='" + dv_secs + "' ," +
                                "dv_value='" + dv_value + "' ," +
                                "dv_valuetext='" + dv_valuetext + "' ," +
                                "dv_updateby='" + dv_updateby + "' ," +
                                "dv_lastupdate='" + dv_lastupdate + "' ," +
                                "sts='" + sts + "' " +

" where " +
"dv_id='" + dv_id + "'";
                Log.Info("UPDATE: " + query);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                // Log file
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                string log_text = "Update value eqpdatavariable. ID=" + this.dv_id;
                string queryLog = "insert into action_log(log_text, log_time, user_id) values('" + log_text + "','" + unixTimestamp + "','" + Main.getUserId() + "')";
                MySqlCommand cmdLog = new MySqlCommand(queryLog, conn);
                cmdLog.ExecuteNonQuery();
                // End logfile
            }
            catch (MySqlException e)
            {
                Log.Error("Error: " + e.Message);
            }

        }


        public void delete()
        {
            //
            try
            {
                MySqlConnection conn = Main.getConnection();
                if (conn == null)
                {
                    return;
                }
                string query = "update eqpdatavariable set STS=3 " +
" where " +
"dv_id='" + dv_id + "'";
                Console.WriteLine("DELETE: " + query);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                // Log file
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                string log_text = "Update value eqpdatavariable. ID=" + this.dv_id;
                string queryLog = "insert into action_log(log_text, log_time, user_id) values('" + log_text + "','" + unixTimestamp + "','" + Main.getUserId() + "')";
                MySqlCommand cmdLog = new MySqlCommand(queryLog, conn);
                cmdLog.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Log.Error("Error: " + e.Message);
            }
        }

        public void deleteRow()
        {
            //
            try
            {
                MySqlConnection conn = Main.getConnection();
                if (conn == null)
                {
                    return;
                }
                string query = "delete from eqpdatavariable " +
" where " +
"dv_id='" + dv_id + "'";
                Log.Info("DELETE: " + query);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                // Log file
                Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                string log_text = "Delete row eqpdatavariable. ID=" + this.dv_id;
                string queryLog = "insert into action_log(log_text, log_time, user_id) values('" + log_text + "','" + unixTimestamp + "','" + Main.getUserId() + "')";
                MySqlCommand cmdLog = new MySqlCommand(queryLog, conn);
                cmdLog.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Log.Error("Error: " + e.Message);
            }
        }

        public Eqpdatavariable(System.Windows.Forms.DataGridViewRow row)
        {
            getFromRow(row);
        }

        public void getFromRow(System.Windows.Forms.DataGridViewRow row)
        {
            dv_id = Convert.ToInt32(row.Cells["dv_id"].Value.ToString());

            eqp_id = Convert.ToInt32(row.Cells["eqp_id"].Value.ToString());
            dv_type = Convert.ToInt32(row.Cells["dv_type"].Value.ToString());
            dv_uuid = row.Cells["dv_uuid"].Value.ToString();
            dv_name = row.Cells["dv_name"].Value.ToString();
            dv_unit = row.Cells["dv_unit"].Value.ToString();
            dv_limitmin = row.Cells["dv_limitmin"].Value.ToString();
            dv_limitmax = row.Cells["dv_limitmax"].Value.ToString();
            dv_default = row.Cells["dv_default"].Value.ToString();
            dv_datatype = Convert.ToInt32(row.Cells["dv_datatype"].Value.ToString());
            dv_secs = row.Cells["dv_secs"].Value.ToString();
            dv_value = row.Cells["dv_value"].Value.ToString();
            dv_valuetext = row.Cells["dv_valuetext"].Value.ToString();
            dv_updateby = row.Cells["dv_updateby"].Value.ToString();
            dv_lastupdate = Convert.ToInt32(row.Cells["dv_lastupdate"].Value.ToString());
            sts = Convert.ToInt32(row.Cells["sts"].Value.ToString());
        }

        public static DataTable loadDt(string query)
        {
            List<Eqpdatavariable> list = load(query);
            return ListToDataTable(list);
        }

        public static DataTable loadDt()
        {
            List<Eqpdatavariable> list = load("");
            return ListToDataTable(list);
        }

        public static DataTable ListToDataTable<T>(List<T> items)
        {

            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {

                //Setting column names as Property names

                dataTable.Columns.Add(prop.Name);

            }

            foreach (T item in items)
            {

                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)
                {

                    //inserting property values to datatable rows

                    values[i] = Props[i].GetValue(item, null);

                }

                dataTable.Rows.Add(values);

            }

            //put a breakpoint here and check datatable

            return dataTable;

        }

    }
}