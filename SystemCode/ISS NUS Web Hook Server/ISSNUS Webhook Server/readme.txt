Sample Request Json data from DialogFlow Server:
*************************************************
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