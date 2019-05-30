using Sys.Http.DialogFlow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Sys.Http.Server
{
    class Program
    {
        private static int RequestCount = 0;
        private static int Port = 5001;
        private static Sys.Http.DialogFlow.Request Request  = new Sys.Http.DialogFlow.Request();

        static string ApplicationPath()
        {
          return  AppDomain.CurrentDomain.BaseDirectory;
        }
        static string ApplicationDataPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "Data";
        }

        static void Main(string[] args)
        {
            ////*******************************************************
            ////Test Search Algorithm 
            ////*******************************************************
            //Sys.Tool.KnowledgeObject kb = new Sys.Tool.KnowledgeObject(ApplicationDataPath() + "/KnowledgeBase.dat");
            ////kb.TestBinaryTree();
            ////kb.SetUpKnowledgeBase();
            ////kb.Store();

            //Sys.Tool.DataStore DataStore = new Sys.Tool.DataStore(ApplicationDataPath() + "/DataStore.dat");
            //DataStore = DataStore.Restore();
            //DataStore.WriteToCSVFile("DataStore.csv");


            Sys.Tool.KnowledgeObject kb1 = new Sys.Tool.KnowledgeObject(ApplicationDataPath() + "/KnowledgeBase1.dat");
            Sys.Tool.DataStore DataStore = new Sys.Tool.DataStore(ApplicationDataPath() + "/DataStore.dat");

            //DataStore.LoadDataFileExcel(ApplicationDataPath() + "/ISS_ExecutiveEducation_v1.xlsx", "data");
            //kb1.SetUpKnowledgeBase(DataStore.Table);
            //kb1.Store();

            //Sys.Tool.KnowledgeObject kb2 = new Sys.Tool.KnowledgeObject(ApplicationDataPath() + "/KnowledgeBase2.dat");
            //DataStore.LoadDataFileExcel(ApplicationDataPath() + "/ISS_GraduateProgramme_v1.xlsx", "data");
            //kb2.SetUpKnowledgeBase(DataStore.Table);
            //kb2.Store();

            Sys.Tool.KnowledgeObject kb3 = new Sys.Tool.KnowledgeObject(ApplicationDataPath() + "/KnowledgeBase3.dat");
            DataStore.LoadDataFileExcel(ApplicationDataPath() + "/ISS_StackableProgramme_v1.xlsx", "data");
            kb3.SetUpKnowledgeBase(DataStore.Table);
            kb3.Store();

            Sys.Tool.KnowledgeObject kb4 = new Sys.Tool.KnowledgeObject(ApplicationDataPath() + "/KnowledgeBase4.dat");
            DataStore.LoadDataFileExcel(ApplicationDataPath() + "/ISS_Other_v1.1.xlsx", "data");
            kb4.SetUpKnowledgeBase(DataStore.Table);
            kb4.Store();




            Tool.SearchParameters pp = new Tool.SearchParameters();
            //pp.Action = "choose"; pp.QuestionKey = "why"; pp.Location = ""; pp.InquiryType = ""; pp.Subject = "NUS ISS";
            //pp.Action = "get"; pp.QuestionKey = "how"; pp.Location = ""; pp.InquiryType = ""; pp.Subject = "NUS ISS";
            pp.Action = "select"; pp.QuestionKey = "why"; pp.Location = ""; pp.InquiryType = ""; pp.Subject = "NUS ISS";
            string ff = kb4.Search("", pp);
            //*******************************************************

            //Sys.Tool.DataStore data = new Sys.Tool.DataStore(ApplicationDataPath() + "/DataStore.dat");
            //data.LoadDataFileExcel(ApplicationDataPath() + "/ISS_Courses_V1b.xlsx", "data");
            //data.CleanData();
            //data.Store();
            //data = data.Restore();


            //data.Filename = ApplicationDataPath() + "/DataStore.dat";


            using (var server = new NHttp.HttpServer())
            {
                // New requests are signaled through the RequestReceived
                // event.

                server.RequestReceived += Server_RequestReceived;
               

                // Start the server on a random port. Use server.EndPoint
                // to specify a specific port, e.g.:
                //
                server.EndPoint = new IPEndPoint(IPAddress.Loopback, Port);
                //

                server.Start();

                // Start the default web browser.
                // Process.Start(String.Format("http://{0}/", server.EndPoint));

                //Console.WriteLine("Press any key to continue...");
                //Console.ReadKey();

                while (true)
                {
                    System.Threading.Thread.Sleep(1);
                }
                // When the HttpServer is disposed, all opened connections
                // are automatically closed.
            }
        }
 

        private static void Server_RequestReceived(object sender, NHttp.HttpRequestEventArgs e)
        {
            string RequestString = "";

            if (e.Request.HttpMethod == "GET")
            {
                //Ignore all GET requests
                //Show server message
                using (var writer = new StreamWriter(e.Response.OutputStream))
                {
                    writer.Write("<h2>Welcome to ISS NUS Http Server</h2>\n" + "Total post requests served since last startup: " + RequestCount.ToString());
                }
            }

            if (e.Request.HttpMethod == "POST" && e.Request.InputStream != null)
            {
                RequestCount += 1;
                Sys.Http.DialogFlow.Request RequestData;
                using (StreamReader reader = new StreamReader(e.Request.InputStream, Encoding.UTF8))
                {
                    RequestString = reader.ReadToEnd();                    
                }

                Request.CreateRequestObject(RequestString);
                Sys.Http.DialogFlow.Response Response = new Sys.Http.DialogFlow.Response();

                string ResponseText = Response.GetResponse(Request.Data);
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"fulfillmentText\": \"" + ResponseText + "\" }");


                using (var writer = new StreamWriter(e.Response.OutputStream))
                {
                    writer.Write(sb.ToString());
                }
            }
           
        }
    }
}
