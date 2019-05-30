using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sys.Tool
{
    [Serializable()]
    public class Parameters
    {

        [JsonProperty("inquirytype")]
        public string Inquirytype;

        [JsonProperty("location")]
        public string Location;

        [JsonProperty("action")]
        public string Action;

        [JsonProperty("subject")]
        public string Subject;

        [JsonProperty("questionkey")]
        public string QuestionKey;
    }

    [Serializable()]
    public class text
    {

        [JsonProperty("text")]
        public IList<string> Text;
    }

    [Serializable()]
    public class FulfillmentMessage
    {

        [JsonProperty("text")]
        public text Text;
    }

    [Serializable()]
    public class Intent
    {

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("displayName")]
        public string DisplayName;
    }

    [Serializable()]
    public class DiagnosticInfo
    {

        [JsonProperty("webhook_latency_ms")]
        public int WebhookLatencyMs;
    }

    [Serializable()]
    public class QueryResult
    {

        [JsonProperty("queryText")]
        public string QueryText;

        [JsonProperty("action")]
        public string Action;

        [JsonProperty("parameters")]
        public Parameters Parameters;

        [JsonProperty("allRequiredParamsPresent")]
        public bool AllRequiredParamsPresent;

        [JsonProperty("fulfillmentText")]
        public string FulfillmentText;

        [JsonProperty("fulfillmentMessages")]
        public IList<FulfillmentMessage> FulfillmentMessages;

        [JsonProperty("intent")]
        public Intent Intent;

        [JsonProperty("intentDetectionConfidence")]
        public string IntentDetectionConfidence;

        [JsonProperty("diagnosticInfo")]
        public DiagnosticInfo DiagnosticInfo;

        [JsonProperty("languageCode")]
        public string LanguageCode;
    }

    [Serializable()]
    public class WebhookStatus
    {

        [JsonProperty("message")]
        public string Message;
    }

    [Serializable()]
    public class ResponseData
    {

        [JsonProperty("responseId")]
        public string ResponseId;

        [JsonProperty("queryResult")]
        public QueryResult QueryResult;

        [JsonProperty("webhookStatus")]
        public WebhookStatus WebhookStatus;
    }

}
