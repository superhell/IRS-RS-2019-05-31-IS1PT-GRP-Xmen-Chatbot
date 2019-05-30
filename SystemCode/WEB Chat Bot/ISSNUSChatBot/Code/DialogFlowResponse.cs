
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sys.Tool.DialogFlow
{
    class Request
    {
        public string responseId = "";
        public Base.queryResult queryResult = new Base.queryResult();
        public string session = "";

        public Request() { }
        public Request(string RequestData)
        {
            CreateRequestObject(RequestData);
        }

        private string[] Split(string data)
        {
            char[] Delimiter = { ':' };
            string[] item = data.Split(Delimiter);

            for (int i = 0; i < item.Length; i++)
            {
                //removce spaces
                item[i] = item[i].Trim();
                item[i] = item[i].TrimEnd(',');
            }

            return item;

        }
        private void CreateRequestObject(string RequestData)
        {
            string reqdata = RequestData;

            reqdata = reqdata.Replace("\"", "");
            reqdata = reqdata.Replace(@"\", string.Empty);
            bool IsParameters = false;
            bool IsqueryResult = false;
            bool Isintent = false;

            //split data into lines using "\n"
            char[] RowDelimiter = { '\n' };
            char[] ItemDelimiter = { ':' };

            string[] data = reqdata.Split(RowDelimiter);
            string[] element;
            try
            {
                foreach (string item in data)
                {
                    element = Split(item);
                    switch (element[0])
                    {
                        case "responseId":
                            responseId = element[1];
                            break;

                        case "queryResult":
                            IsqueryResult = true;
                            break;
                        case "queryText":
                            if (IsqueryResult) queryResult.queryText = element[1];
                            break;
                        case "action":
                            if (IsqueryResult) queryResult.action = element[1];
                            if (IsParameters) queryResult.parameters.action = element[1];
                            break;

                        case "parameters":
                            IsParameters = true;
                            IsqueryResult = false;
                            break;
                        case "inquirytype":
                            if (IsParameters) queryResult.parameters.inquirytype = element[1];
                            break;
                        case "location":
                            if (IsParameters) queryResult.parameters.location = element[1];
                            break;


                        case "allRequiredParamsPresent":
                            queryResult.allRequiredParamsPresent = element[1];
                            break;

                        case "intent":
                            Isintent = true;
                            break;
                        case "name":
                            if (Isintent) queryResult.intent.name = element[1];
                            break;
                        case "displayName":
                            if (Isintent) queryResult.intent.displayName = element[1];
                            break;

                        case "intentDetectionConfidence":
                            element[1] = element[1].Replace(@",", "");
                            queryResult.intentDetectionConfidence = float.Parse(element[1]);
                            break;
                        case "languageCode":
                            queryResult.languageCode = element[1];
                            break;
                        case "session":
                            session = element[1];
                            break;


                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }

}

namespace Sys.Tool.DialogFlow.Base
{
    class queryResult
    {
        public string queryText = "";
        public string action = "";

        public parameters parameters = new parameters();
        public string allRequiredParamsPresent = "";
        public Intent intent = new Intent();
        public float intentDetectionConfidence = 0.0f;
        public string languageCode = "en";
    }

    class parameters
    {
        public string inquirytype = "";
        public string location = "";
        public string action = "";
    }
    class Intent
    {
        public string name = "";
        public string displayName = "";
    }
}
