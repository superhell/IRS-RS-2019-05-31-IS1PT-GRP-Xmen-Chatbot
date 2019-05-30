 Install-Package Google.Cloud.Dialogflow.V2 -Version 1.0.0-beta01 

 Sample Response from DialogFlow (V2)
 
 Raw ouput:

 { \"responseId\": \"a08f4ade-743e-4036-8498-384363116741-81f88fd2\", \"queryResult\": { \"queryText\": \"what is day?\", \"action\": \"input.welcome\", \"parameters\": { }, \"allRequiredParamsPresent\": true, \"fulfillmentText\": \"Hello! How can I help you?\", \"fulfillmentMessages\": [ { \"text\": { \"text\": [ \"Hello! How can I help you?\" ] } } ], \"intent\": { \"name\": \"projects/issnus-b185f/agent/intents/05150e15-b322-490b-b1e6-deebffa73dd4\", \"displayName\": \"Default Welcome Intent\" }, \"intentDetectionConfidence\": 0.5885197, \"languageCode\": \"en\" } }

 ***************************************
 Layout:
 ***************************************
 { 
\"responseId\": \"a08f4ade-743e-4036-8498-384363116741-81f88fd2\", 

\"queryResult\": { 
\"queryText\": \"what is day?\", 
\"action\": \"input.welcome\", 

\"parameters\": { }, 
\"allRequiredParamsPresent\": true, 
\"fulfillmentText\": \"Hello! How can I help you?\", 

\"fulfillmentMessages\": [ { 
\"text\": { 
\"text\": [ \"Hello! How can I help you?\" ] } } ], 
\"intent\": { \"name\": \"projects/issnus-b185f/agent/intents/05150e15-b322-490b-b1e6-deebffa73dd4\", 
\"displayName\": \"Default Welcome Intent\" }, 
\"intentDetectionConfidence\": 0.5885197, 
\"languageCode\": \"en\" 

} }


Example 2:

{ 
\"responseId\": \"c43c434c-fe56-45af-98ab-f6ec372bc649-81f88fd2\", 
\"queryResult\": { 
\"queryText\": \"Does ISS NUS offer post graduate programmes?\", 
\"action\": \"location\", 

\"parameters\": { 
\"action\": \"offer\", 
\"inquirytype\": \"post graduate programmes\", 
\"location\": \"ISS NUS\" }, 
\"allRequiredParamsPresent\": true, 

\"intent\": { 
\"name\": \"projects/issnus-b185f/agent/intents/80722f4e-c353-4e28-a87e-100f2cf07c17\", 
\"displayName\": \"ProgrammeIntent\" }, 
\"intentDetectionConfidence\": 1, 

\"diagnosticInfo\": { 
\"webhook_latency_ms\": 110 }, 
\"languageCode\": \"en\" }, 

\"webhookStatus\": { 
\"message\": \"Webhook execution successful\" } }



{ "responseId": "9a3028fb-835b-4c2a-b1ac-4f9e95f43923-81f88fd2", "queryResult": { "queryText": "What programmes are available at ISS NUS?", "action": "location", "parameters": { "inquirytype": "programmes", "location": "ISS NUS", "action": "available" }, "allRequiredParamsPresent": true, "fulfillmentText": "There are 3 areas of programmes in ISS NUS: (1) Executive Education, (2) Graduate programmes, (3) Stackable programmes ", "fulfillmentMessages": [ { "text": { "text": [ "There are 3 areas of programmes in ISS NUS: (1) Executive Education, (2) Graduate programmes, (3) Stackable programmes " ] } } ], "intent": { "name": "projects/issnus-b185f/agent/intents/80722f4e-c353-4e28-a87e-100f2cf07c17", "displayName": "ProgrammeIntent" }, "intentDetectionConfidence": 1, "diagnosticInfo": { "webhook_latency_ms": 42 }, "languageCode": "en" }, "webhookStatus": { "message": "Webhook execution successful" } }

