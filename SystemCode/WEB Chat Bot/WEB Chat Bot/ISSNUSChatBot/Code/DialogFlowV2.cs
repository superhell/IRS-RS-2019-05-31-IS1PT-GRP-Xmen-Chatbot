using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Dialogflow.V2;

using Grpc.Auth;


/// <summary>
/// Summary description for dialogflow
/// </summary>
namespace Sys.Tool
{
    public class dialogflow
    {
        public string AgentID = "issnus-b185f";   //"weatherworkshop-a97f5";
        public string GoogleCredentialJsonFile = "issnus-b185f-be9c3b99695d.json";  //"weatherworkshop-a97f5-66f4472b43d4.json";
        public dialogflow()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string Query(string Question)
        {

            var query = new QueryInput
            {
                Text = new TextInput
                {
                    Text = Question,
                    LanguageCode = "en-us"
                }
            };

            var sessionId = Guid.NewGuid().ToString();
            var agent = AgentID;
            var creds = GoogleCredential.FromFile(HttpContext.Current.Server.MapPath("~/Content/Account/" + GoogleCredentialJsonFile));

            var channel = new Grpc.Core.Channel(SessionsClient.DefaultEndpoint.Host,
                          creds.ToChannelCredentials());

            var client = SessionsClient.Create(channel);

            var dialogFlow = client.DetectIntent(
                new SessionName(agent, sessionId),
                query
            );



            string data = dialogFlow + "\n\n" + dialogFlow.QueryResult + "     FulfillmentText:   " + dialogFlow.QueryResult.FulfillmentText;

            channel.ShutdownAsync();
            return data;


          

        }


    }

}



