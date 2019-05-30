
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Sys.Http.DialogFlow
{
    class Request
    {

        public Sys.Tool.RequestData Data = new Sys.Tool.RequestData();

  

        public Request() { }
 

        public void CreateRequestObject(string RequestData)
        {
 
            try
            {
    
                Data = JsonConvert.DeserializeObject<Sys.Tool.RequestData>(RequestData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
          

        }
    }
  
}
 
