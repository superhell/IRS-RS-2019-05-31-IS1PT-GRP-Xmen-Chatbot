
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
 

namespace Sys.Tool
{
    [Serializable()]
    public  class Response: Serialization
    {


        public Sys.Tool.ResponseData Data = new Sys.Tool.ResponseData();


        

        public void CreateResponseObject(string RequestData)
        {
           // string reqdata = RequestData;

            //reqdata = reqdata.Replace("\"", "");
           // reqdata = reqdata.Replace(@"\\", string.Empty);

            //JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            //var obj = json_serializer.DeserializeObject(RequestData);

            try
            {
                Data = JsonConvert.DeserializeObject<Sys.Tool.ResponseData>(RequestData);
            }
            catch (Exception ex)
            {

                throw ex;
            }
          


        }



    }

}
 

 

 
 