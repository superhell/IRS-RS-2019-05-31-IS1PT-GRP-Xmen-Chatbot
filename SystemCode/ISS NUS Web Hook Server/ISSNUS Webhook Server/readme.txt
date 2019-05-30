Sample Request Json data from DialogFlow Server:
******************************************************
{\n
  \"responseId\": \"ab51d8e8-3139-409d-9d87-13e0752fb43d-81f88fd2\",\n

\"queryResult\": {\n
  \"queryText\": \"Does ISS NUS conduct post graduate programmes?\",\n       
   \"action\": \"location\",\n    

   \"parameters\": {\n
   	\"inquirytype\": \"post graduate programmes\",\n 
   	\"location\": \"ISS NUS\",\n
   	\"action\": \"conduct\"\n
    },\n

  \"allRequiredParamsPresent\": true,\n
   
   \"intent\": {\n
  	\"name\": \"projects/issnus-cbd88/agent/intents/80722f4e-c353-4e28-a87e-100f2cf07c17\",\n
  	\"displayName\": \"ProgrammeIntent\"\n
   },\n

 \"intentDetectionConfidence\": 1.0,\n
 \"languageCode\": \"en\"\n
  },\n

	\"originalDetectIntentRequest\": {\n
 		\"payload\": {\n
 	 	}\n
	},\n

\"session\": \"projects/issnus-cbd88/agent/sessions/57d0e8fc-f0b4-a642-f043-ea3721aa40d5\"\n
}


Raw Sample Request Json data from DialogFlow Server:
******************************************************
{\n  \"responseId\": \"f4cdb4a7-94b0-47b8-aab7-e7eab2387810-81f88fd2\",\n  \"queryResult\": {\n    \"queryText\": \"What programmes are available at ISS NUS?\",\n    \"action\": \"location\",\n    \"parameters\": {\n      \"inquirytype\": \"programmes\",\n      \"location\": \"ISS NUS\",\n      \"action\": \"available\"\n    },\n    \"allRequiredParamsPresent\": true,\n    \"intent\": {\n      \"name\": \"projects/issnus-b185f/agent/intents/80722f4e-c353-4e28-a87e-100f2cf07c17\",\n      \"displayName\": \"ProgrammeIntent\"\n    },\n    \"intentDetectionConfidence\": 1.0,\n    \"languageCode\": \"en\"\n  },\n  \"originalDetectIntentRequest\": {\n    \"payload\": {\n    }\n  },\n  \"session\": \"projects/issnus-b185f/agent/sessions/77463fbf-c359-43ee-8c61-07b7e8020c6e\"\n}