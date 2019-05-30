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
    public class dialogflow: IDisposable
    {
        public string AgentID = "issnus-b185f";   //"weatherworkshop-a97f5";
        public string GoogleCredentialJsonFile = "issnus-b185f-be9c3b99695d.json";  //"weatherworkshop-a97f5-66f4472b43d4.json";
        public string dialogFlowString = "";
        public string FulfillmentText = "";
        public string QueryResult = "";

        private string sessionId = "";
        private SessionsClient client;
        private Grpc.Core.Channel channel;
        private bool IsChannelKeepAlive = true;
        public NetworkError NetworkError = new NetworkError();

        public dialogflow(bool ChannelKeepAlive=true)
        {
            IsChannelKeepAlive = ChannelKeepAlive;
        }
        public dialogflow()
        {
        }

        private void Init()
        {
            if(channel == null)
            {
                sessionId = Guid.NewGuid().ToString();

                var creds = GoogleCredential.FromFile(HttpContext.Current.Server.MapPath("Content/Account/" + GoogleCredentialJsonFile));

                channel = new Grpc.Core.Channel(SessionsClient.DefaultEndpoint.Host,
                              creds.ToChannelCredentials());

                client = SessionsClient.Create(channel);
            }          
        }

        public string Query(string Question)
        {
            if (channel != null)
            {
                if(channel.State == Grpc.Core.ChannelState.Shutdown ||
                    channel.State == Grpc.Core.ChannelState.TransientFailure)
                {
                    channel = null;                   
                }
            }
            if (channel == null) Init();

            var query = new QueryInput
            {
                Text = new TextInput
                {
                    Text = Question,
                    LanguageCode = "en-us"
                }
            };

            DetectIntentResponse dialogFlow =null;
            try
            {
                dialogFlow = client.DetectIntent(
                               new SessionName(AgentID, sessionId),
                               query
                           );
            }
            catch (Exception ex)
            {
                NetworkError.Message = ex.Message;
                NetworkError.StackTrace = ex.ToString();
                return "I am unable to access the internet network. Can you please make sure your network is online?";
            }
           

            dialogFlowString = dialogFlow.ToString();

            QueryResult = dialogFlow.QueryResult.ToString();
            FulfillmentText = dialogFlow.QueryResult.FulfillmentText;

            if (IsChannelKeepAlive == false)
            {
                channel.ShutdownAsync();                
            }

            return FulfillmentText;

        }
        public void Shutdown()
        {
            if (channel == null) channel.ShutdownAsync();
        }
        public void Dispose()
        {
            try
            {
                channel.ShutdownAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
    public class NetworkError
    {
        public  string Message;
        public  string StackTrace;
    }

}



