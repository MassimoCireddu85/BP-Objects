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

namespace Replace_value_with_expression
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //how to create a datatable instance and add the first columns
            DataTable input = new DataTable();
            input.Columns.Add("ID");
            input.Columns.Add("Name");
            input.Columns.Add("Surname");

            //how to add new rows with data
            input.Rows.Add("01", "Massimo", "Cireddu");
            input.Rows.Add("02", "Malak", "Lunati");
            input.Rows.Add("03", "Nanzo", "Lunati");

            //how to change column order with SetOrdinal Method
            input.Columns["ID"].SetOrdinal(1);

            //how to import data to the data grid
            dataGridView1.DataSource = input;

            //REPLACE VALUE WITH EXPRESSION
            //user parameters
            string exp = "IIF([Surname] = 'Cireddu','Max',[Surname])";
            string name = "Name";
            string type = "Text";
            DataTable output = new DataTable();

            //incode parameters
            string name2 = name + "abcdeF654321";

            if (type.ToLower().Equals("number"))
            {
                input.Columns.Add(name2, typeof(decimal), exp);
            }
            else if (type.ToLower().Equals("text"))
            {
                input.Columns.Add(name2, typeof(string), exp);
            }
            else if (type.ToLower().Equals("flag"))
            {
                input.Columns.Add(name2, typeof(bool), exp);
            }
            else if (type.ToLower().Equals("datetime"))
            {
                input.Columns.Add(name2, typeof(DateTime), exp);
            }

            foreach (DataRow r in input.Rows)
            {
                r[name] = r[name2];
            }

            input.Columns.Remove(name2);


            input.AcceptChanges();

            output = input.Copy();
            dataGridView1.DataSource = output;
        }
    }
}