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
            DataTable input = new DataTable();
            input.Columns.Add("ID");
            input.Columns.Add("CODE1");
            input.Columns.Add("CODE2");

            //add new rows with data
            input.Rows.Add("01", "7240", "1020");
            input.Rows.Add("02", "8660", "0642");
            input.Rows.Add("03", "1428", "0131");
            input.Rows.Add("04", "1428", "0131");
            input.Rows.Add("05", "1020", "7240");

            //change column order with SetOrdinal Method
            input.Columns["ID"].SetOrdinal(0);

            //import data to the data grid
            dataGridView1.DataSource = input;
     
            

            //DISTINCT COLUMNS TO SQL WITH ARRAYS
            //user parameters
            string column1 = "CODE1";
            string column2 = "";
            string query = "SELECT COMPANY_CD FROM TABLE WHERE COMPANY CODE IN (#COMPANY_LIST)"; //the target query which needs to be updated
            string fieldToReplace = "#COMPANY_LIST"; //the field which has to be replaced within the SQL query
            string output; //the output query


            //incode parameters
            string result = "";
            string col1 = String.Empty;
            string col2 = String.Empty;

            //case if the input collection consists of only one column, then the col1 should be named as per that column
            if (input.Columns.Count == 1) //replace with input.Columns.Count
            {
                col1 = input.Columns[0].ColumnName;

                if (input.Rows.Count > 0)
                {
                    //arrayList instance where to store distinct string elements
                    string[] myList = new string[input.Rows.Count];

                    //first array element
                    int k = 0;

                    //datatable loop
                    foreach (DataRow r in input.Rows) //replace dt with input
                    {
                        myList[k] = r[col1].ToString();
                        k++;
                    }

                    //get the distinct array
                    string[] myListdist = myList.Distinct().ToArray();

                    //loop arrayList and build a string with separators
                    foreach (string obj in myListdist)
                    {
                        result += "'" + obj.ToString().Trim() + "',";
                    }

                    //result with seprator cleaning
                    int i = result.LastIndexOf(',');
                    result = result.Remove(i, 1);

                }

                else output = "''";
            }

            //case if the input colleciton has more than one column and the input columns are two
            else if (input.Columns.Count > 1 && column1 != "" && column2 != "")
            {
                //assign the input columns
                col1 = column1;
                col2 = column2;

                if (input.Rows.Count > 0)
                {
                    //arrayList instance where to store distinct string elements
                    string[] myList = new string[input.Rows.Count*2];

                    //first array element
                    int k = 0;

                    //datatable loop
                    foreach (DataRow r in input.Rows) //replace dt with input
                    {
                        myList[k] = r[col1].ToString();
                        myList[k+1] = r[col2].ToString();
                       
                        k = k + 2;
                        
                    }

                    //get the distinct array
                    string[] myListdist = myList.Distinct().ToArray();

                    //loop arrayList and build a string with separators
                    foreach (string obj in myListdist)
                    {
                        result += "'" + obj.ToString().Trim() + "',";
                    }

                    //result with seprator cleaning
                    int i = result.LastIndexOf(',');
                    result = result.Remove(i, 1);

                }

                else output = "''";
            }

            //case if the input colleciton has more than one column and the input columns is 1
            else if (input.Columns.Count > 1 && column1 != "" && column2 == "")
            {

                //assign the input column
                col1 = column1;

                if (input.Rows.Count > 0)
                {
                    //arrayList instance where to store distinct string elements
                    string[] myList = new string[input.Rows.Count];

                    //first array element
                    int k = 0;

                    //datatable loop
                    foreach (DataRow r in input.Rows) //replace dt with input
                    {
                        myList[k] = r[col1].ToString();
                        k++;
                    }

                    //get the distinct array
                    string[] myListdist = myList.Distinct().ToArray();

                    //loop arrayList and build a string with separators
                    foreach (string obj in myListdist)
                    {
                        result += "'" + obj.ToString().Trim() + "',";
                    }

                    //result with seprator cleaning
                    int i = result.LastIndexOf(',');
                    result = result.Remove(i, 1);

                }

                else output = "''";

            }
            else
            {
                output = "''";
            }

            //query update with distinct list
            output = query.Replace(fieldToReplace, result);

            /*
            //DISTINCT COLUMNS TO SQL WITH ARRAYLIST
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
            if (input.Columns.Count == 1) //replace with input.Columns.Count
            {
                col1 = input.Columns[0].ColumnName;
            }
            
            //case if the input colleciton has more than one column and the input columns are two
            else if (input.Columns.Count > 1 && column1 != "" && column2 != "")
            {
                //assign the input columns
                col1 = column1;
                col2 = column2;

                if (input.Rows.Count > 0)
                {
                    //arrayList instance where to store distinct string elements
                    ArrayList myList = new ArrayList();

                    //datatable loop
                    foreach (DataRow r in input.Rows) //replace dt with input
                    {
                        //if element on r doesn't belong to arrayList, add to arrayList
                        if (!myList.Contains(r[col1].ToString()))
                        {
                            myList.Add(r[col1].ToString());
                        }
                        if (!myList.Contains(r[col2].ToString()))
                        {
                            myList.Add(r[col2].ToString());
                        }

                    }

                    //loop arrayList and build a string with separators
                    foreach (object obj in myList)
                    {
                        result += "'" + obj.ToString().Trim() + "',";
                    }

                    //result with seprator cleaning
                    int i = result.LastIndexOf(',');
                    result = result.Remove(i, 1);

                    //query update with distinct list
                    output = query.Replace(fieldToReplace, result);

                }

                else output = "''";
            }

            //case if the input colleciton has more than one column and the input columns is 1
            else if (input.Columns.Count > 1 && column1 != "" && column2 == "")
            {
                
                //assign the input column
                col1 = column1;

                if (input.Rows.Count > 0)
                {
                    //arrayList instance where to store distinct string elements
                    ArrayList myList = new ArrayList();

                    //datatable loop
                    foreach (DataRow r in input.Rows) //replace dt with input
                    {
                        //if element on r doesn't belong to arrayList, add to arrayList
                        if (!myList.Contains(r[col1].ToString()))
                        {
                            myList.Add(r[col1].ToString());
                        }
        
                    }

                    //loop arrayList and build a string with separators
                    foreach(object obj in myList)
                    {
                        result += "'" + obj.ToString().Trim() + "',";
                    }

                    //result with seprator cleaning
                    int i = result.LastIndexOf(',');
                    result = result.Remove(i, 1);

                    //query update with distinct list
                    output = query.Replace(fieldToReplace, result);
                    
                }
                
                else output = "''";

            }
            
            */


        }
    }
}