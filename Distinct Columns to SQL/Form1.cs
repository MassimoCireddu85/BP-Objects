using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//reference to c# DataTables https://www.c-sharpcorner.com/UploadFile/mahesh/datatable-in-C-Sharp/

namespace Distinct_Columns_to_SQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //create a datatable instance and add the first columns
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("CODE1");
            dt.Columns.Add("CODE2");

            //add new rows with data
            dt.Rows.Add("01", "7240", "1020");
            dt.Rows.Add("02", "8660", "0642");
            dt.Rows.Add("03", "1428", "0131");

            //change column order with SetOrdinal Method
            dt.Columns["ID"].SetOrdinal(0);

            //import data to the data grid
            dataGridView1.DataSource = dt;

            //DISTINCT COLUMNS TO SQL
            //user parameters
            string column1 = "CODE1";
            string column2 = "CODE2";
            string query = "SELECT COMPANY_CD FROM TABLE WHERE COMPANY CODE IN (#COMPANY_LIST)"; //the target query which needs to be updated
            string fieldToReplace = "#COMPANY_LIST"; //the field which has to be replaced within the SQL query
            string output ; //the output query

            //incode parameters
            string result = "";
            string col1 = String.Empty;
            string col2 = String.Empty;

            //case if the input collection consists of only one column, then the col1 should be named as per that column
            if (dt.Columns.Count == 1) //replace with input.Columns.Count
            {
                col1 = dt.Columns[0].ColumnName;
            }
            
            //case if the input colleciton has more than one column and the input columns are two
            else if (dt.Columns.Count > 1 && column1 != "" && column2 != "")
            {
                col1 = column1;
                col2 = column2;
            }

            //hypothesis if the input colleciton has more than one column and the input columns is 1
            else if (dt.Columns.Count > 1 && column1 != "" && column2 == "")
            {
                col1 = column1;

            }


            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows) //replace dt with input
                {
                    result += "'" + r[col1].ToString().Trim() + "',";
                }
                int i = result.LastIndexOf(',');
                result = result.Remove(i, 1);
                output = result;
            }
            else output = "''";
        }
    }
}