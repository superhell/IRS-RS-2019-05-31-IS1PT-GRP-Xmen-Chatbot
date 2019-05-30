using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
 

namespace Sys.Tool
{
    [Serializable()]
    public class DataStore: Sys.Tool.Serialization
    {
        public DataTable Table;

        public string Filename = "";

        public DataStore() { }



        public DataStore(string filename)
        {
            Filename = filename;
        }

        public void Clear()
        {
            Table.Clear();
        }
        public void CreateTable(string TableName, params string[] ColumnNames)
        {
            Table = new DataTable();
            Table.Clear();

            foreach(string col in ColumnNames)
            {
                Table.Columns.Add(col);
            }          
        }

        public void WriteToCSVFile(string filename)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter("Data/"+ filename);
            StringBuilder sb = new StringBuilder();
            string ColumnNames = "";

            for (int i = 0; i < Table.Columns.Count; i++)
            {
                sb.Append("\"" + Table.Columns[i].ColumnName + "\"" + ",");
            }
            sw.WriteLine(sb.ToString().Substring(0, sb.Length - 1));
            sb.Remove(0, sb.Length);

            foreach (DataRow row in Table.Rows)
            {
                for (int i = 0; i < Table.Columns.Count; i++)
                {                  
                    sb.Append("\"" + row[i] + "\"" + ",");
                }
              
                sw.WriteLine(sb.ToString().Substring(0, sb.Length - 1));
                sb.Remove(0, sb.Length);
            }           
            sw.Flush(); sw.Close();
        }

        public DataStore Restore()
        {
            return (DataStore)DeserializeObjectFromFile(Filename);
        }
        public void Store()
        {
            SerializeObjectToFile(this, Filename);
        }

        public void Add(object[] obj)
        {
            try
            {
                Table.Rows.Add(obj);
                // Table.Rows.Add(new object[] { "Ravi", 500 });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public void LoadDataFileExcel(string FileName, string SheetName)
        {
            Table = new DataTable("datastore");

            string strConnString = null;
            strConnString = "DRIVER=Microsoft Excel Driver (*.xls, *.xlsx, *.xlsm, *.xlsb);ReadOnly=1;FIL=excel 12.0;DBQ=" + FileName + ";";

            string strSQL = null;
            strSQL = "SELECT * FROM [" + SheetName + "$]";

            System.Data.Odbc.OdbcConnection cnn = new System.Data.Odbc.OdbcConnection(strConnString);
            cnn.Open();

            try
            {
                OdbcCommand cmd = new OdbcCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = strSQL;

                OdbcDataReader reader = cmd.ExecuteReader();

                object[] fields = new object[reader.FieldCount];

                Table.Load(reader);
                cnn.Close();


            }
            catch (Exception ex)
            {
                if(cnn.State == ConnectionState.Open) cnn.Close();
                throw ex;
            }
 
        }

        //public void LoadCSVFile(string FileName)
        //{
        //    System.IO.StreamReader sr=null;
        //    string[] cols;
        //    string s = "";
        //    try
        //    {
        //        sr = new System.IO.StreamReader(FileName);
        //        string data = sr.ReadLine();
        //        //header
        //        char[] comma = { '|' };
        //        string[] Delimiter = ("\":\"").Split(comma);
        //        cols = data.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);
        //        if (cols.Length>0)
        //        {
        //            cols[0] = cols[0].Replace("\"", "");
        //            cols[cols.Length-1] = cols[cols.Length-1].Replace("\"", "");
        //        }
        //        CreateTable("data", cols);

        //        data = sr.ReadLine();
        //        while (data != null)
        //        {
        //            cols = data.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);
        //            if (cols.Length > 0)
        //            {
        //                cols[0] = cols[0].Replace("\"", "");
        //                cols[cols.Length - 1] = cols[cols.Length - 1].Replace("\"", "");
        //            }
        //            Table.Rows.Add((object[])cols);
        //            s= cols[0];
        //            data = sr.ReadLine();
        //        }
        //        sr.Close();

        //    }
        //    catch (Exception ex)
        //    {
        //        if (sr != null) sr.Close();
        //        throw ex;
        //    }           
        //}

        public void CleanData()
        {
            string data = "";
            string text = "";
            int Cols = Table.Columns.Count;

            foreach (DataRow row in Table.Rows)
            {
                //
                for (int i = 0; i < Cols; i++)
                {
                    if(row[i].GetType().ToString() == "System.String")
                    {
                        data = row[i].ToString();
                        data = data.Replace("_x000D_", "");
                        row[i] = data.Replace(" { }", "");
                       
                    }
                   
                }
                    //clean overview column's data
                data = row["overview"].ToString(); //indx 6
                row["overview"]= data.Replace("Overview\n", "");

                //clean up coming column's data
                data = row["up_coming"].ToString(); //indx 9
                row["up_coming"] = data.Replace("Upcoming Classes\n", "");

                //remove key takeaways text
                data = row["course_benefit"].ToString(); //indx 10
                text = "Key Takeaways";
                data = data.Replace(text + "\n", "");
                row["course_benefit"] = data.Replace(text + " \n", "");


                //remove who should attend text
                data = row["who_attend"].ToString(); //indx 11
                text = "Who Should Attend";
                data = data.Replace(text + "\n", "");
                row["who_attend"] = data.Replace(text + " \n", "");

                //remove what will be covered text
                data = row["course_detail"].ToString();  //indx 12
                text = "What Will Be Covered";
                data = data.Replace(text + "\n", "");
                row["course_detail"] = data.Replace(text + " \n", "");

                //remove fees & funding text
                data = row["fee_funding"].ToString();  //indx 13
                text = "Fees & Funding";
                data = data.Replace(text+ "\n", "").Replace(text + " \n", "");              
                row["fee_funding"] = data.Replace(text + "_x000D_" + "\n", "").Replace(text + " \n", "");
                 

                //remove certificate , certificate of completion texts
                data = row["certificate"].ToString(); //indx 15
                data = data.Replace("Certificate\n", "").Replace("Certificate \n","");
                text = "Certificate of Completion";
                row["certificate"] = data.Replace(text + " \n", "").Replace(text + "\n", "");

                //remove preparing for your course text
                text = "Preparing for Your Course";
                data = row["preparation"].ToString();  //indx 16
                data = data.Replace(text + "\n", "").Replace(text + " \n", "");
                text = "Preparing for Your Course_x000D_";
                row["preparation"] = data.Replace(text + "\n", "").Replace(text + " \n", "");
               
                //remove Exams & Certificate , Certificate of Completion texts
                data = row["exam_certification"].ToString(); //indx 17
                text = "Exams & Certificate";
                data = data.Replace(text + " \n", "").Replace(text + "\n", "");
                text = "Certificate of Completion";
                row["exam_certification"] = data.Replace(text + " \n", "").Replace(text + "\n", "");

                //remove Requirements text
                data = row["requirements"].ToString(); //indx 18
                text = "Requirements";               
                row["requirements"] = data.Replace(text + " \n", "").Replace(text + "\n", "");

                //remove How to Apply  text
                data = row["how_to_apply"].ToString();  //indx 19
                text = "How to Apply";
                row["how_to_apply"] = data.Replace(text + " \n", "").Replace(text + "\n", "");
            }
        }

        //public void LoadDataFileCSV(string csvFilename)
        // {
        //     try
        //     {
        //         if (System.IO.File.Exists(csvFilename))
        //         {
        //             if (Table != null) Table.Clear(); // clear existing data

        //             string[] Data = System.IO.File.ReadAllLines(csvFilename);
        //             // items in csv file must be separated by tab character, no single or double quotes required
        //             // first row must have header column names

        //             // 1) Create column names
        //             char[] tab = { '\t' };

        //             string[] columnnames = Data[1].Split(tab);
        //             this.CreateTable("datastore", columnnames);

        //             bool IsFirstRow = true; //skip first row                
        //             foreach (var item in Data)
        //             {
        //                 if (!IsFirstRow)
        //                 {
        //                     string[] obj = item.Split(tab);
        //                     this.Add(obj);
        //                 }
        //                 IsFirstRow = false;
        //             }

        //         }
        //     }
        //     catch (Exception ex)
        //     {

        //         throw;
        //     }

        // }


    }

  
}