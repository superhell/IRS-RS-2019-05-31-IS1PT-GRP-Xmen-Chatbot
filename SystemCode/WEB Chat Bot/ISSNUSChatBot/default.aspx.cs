using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sys.Application
{
    public partial class ChatBot : System.Web.UI.Page
    {
        private bool IsPageStarted = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                Sys.Tool.Request.Init(Page);

                

                string ResponseText = Sys.Tool.Request.ProcessQuery();

                if (ResponseText.Trim() != "")
                {
                    Page.Response.Clear();
                    Page.Response.ClearHeaders();
                    Page.Response.AddHeader("Content-Type", "text/plain");
                    Page.Response.Write(  ResponseText);
                    Page.Response.End();
                }

               


            }

        }

        

    }
 
}