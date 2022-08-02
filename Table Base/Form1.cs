using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Table_Base
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
            

        }
    }
}