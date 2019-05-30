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
        public string InquiryType;

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
    public class Intent
    {

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("displayName")]
        public string DisplayName;
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

        [JsonProperty("intent")]
        public Intent Intent;

        [JsonProperty("intentDetectionConfidence")]
        public double IntentDetectionConfidence;

        [JsonProperty("languageCode")]
        public string LanguageCode;
    }

    [Serializable()]
    public class Payload
    {
    }

    [Serializable()]
    public class OriginalDetectIntentRequest
    {

        [JsonProperty("payload")]
        public Payload Payload;
    }

    [Serializable()]
    public class RequestData
    {

        [JsonProperty("responseId")]
        public string ResponseId;

        [JsonProperty("queryResult")]
        public QueryResult QueryResult;

        [JsonProperty("originalDetectIntentRequest")]
        public OriginalDetectIntentRequest OriginalDetectIntentRequest;

        [JsonProperty("session")]
        public string Session;
    }



}
