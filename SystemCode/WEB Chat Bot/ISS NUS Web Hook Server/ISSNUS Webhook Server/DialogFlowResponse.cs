using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Http.DialogFlow
{
    class Response
    {

        public const string MasterProgrammeAvailable = "what masters programmes are available?";
        public const string ProgrammeAvailableAtISSNUS = "what programmes are available at iss nus?";
        public const string ConductPostGraduateProgramme = "does iss nus conduct post graduate programmes?";

        public string GetResponse(Sys.Http.DialogFlow.Request RequestData)
        {
            string ResponseString = "";

            //example checks, need to implement better logic checks using truth table
            string question = RequestData.queryResult.queryText.ToLower();
            switch (question)
            {
                case MasterProgrammeAvailable:
                    ResponseString = "The are a number of masters programme available at ISS NUS. Namely: (1) Master of Technology in Enterprise Business Analytics, (2) Master of Technology in Digital Leadership, (3) Master of Technology in Intelligent Systems, (4) Master of Technology in Software Engineering.";
                    break;
                case ProgrammeAvailableAtISSNUS:
                    ResponseString = "There are 3 areas of programmes in ISS NUS: (1) Executive Education, (2) Graduate programmes, (3) Stackable programmes ";
                    break;
                case ConductPostGraduateProgramme:
                    ResponseString = "Yes, ISS NUS conducts post graduate programmes each year. They are essentially masters programmes.";
                    break;
            }
            return ResponseString;
        }

    }
}
