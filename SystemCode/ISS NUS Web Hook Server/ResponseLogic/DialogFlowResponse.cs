using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Http.DialogFlow
{
   public class Response
    {

        public const string MasterProgrammeAvailable = "what masters programmes are available?";
        public const string ProgrammeAvailableAtISSNUS = "what programmes are available at iss nus?";
        public const string ConductPostGraduateProgramme = "does iss nus offer post graduate programmes?";

        //public Sys.Tool.DataStore DataStore = new Sys.Tool.DataStore(ApplicationDataPath() + "/DataStore.dat");

        Sys.Tool.KnowledgeObject KB1 = new Sys.Tool.KnowledgeObject(ApplicationDataPath() + "/KnowledgeBase1.dat");
        Sys.Tool.KnowledgeObject KB2 = new Sys.Tool.KnowledgeObject(ApplicationDataPath() + "/KnowledgeBase2.dat");
        Sys.Tool.KnowledgeObject KB3 = new Sys.Tool.KnowledgeObject(ApplicationDataPath() + "/KnowledgeBase3.dat");
        Sys.Tool.KnowledgeObject KB4 = new Sys.Tool.KnowledgeObject(ApplicationDataPath() + "/KnowledgeBase4.dat");

        public Response()
        {
            KB1 = KB1.Restore();
            KB2 = KB2.Restore();
            KB3 = KB3.Restore();
            KB4 = KB4.Restore();
        }

        static string ApplicationDataPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "Data";
        }

        public string GetResponse(Sys.Tool.RequestData RequestData)
        {
            string ResponseString = "";

            //RequestData.QueryResult.Parameters.InquiryType
            
            //example checks, need to implement better logic checks using truth table
            string question = RequestData.QueryResult.QueryText.ToLower();
            Tool.SearchParameters par = new Tool.SearchParameters();

            par.Action = RequestData.QueryResult.Parameters.Action +"";
            par.QuestionKey = RequestData.QueryResult.Parameters.QuestionKey + "";
            par.Location = RequestData.QueryResult.Parameters.Location + "";
            par.InquiryType = RequestData.QueryResult.Parameters.InquiryType + "";
            par.Subject = RequestData.QueryResult.Parameters.Subject + "";

            if (RequestData.QueryResult.Intent.DisplayName.ToLower().Trim() == "executiveeducationintent")
            {
                ResponseString = KB1.Search(question, par);
            }
            if (RequestData.QueryResult.Intent.DisplayName.ToLower().Trim() == "graduateprogrammeintent")
            {
                ResponseString = KB2.Search(question, par);
            }
            if (RequestData.QueryResult.Intent.DisplayName.ToLower().Trim() == "stackableprogrammeintent")
            {
                ResponseString = KB3.Search(question, par);
            }
            if (RequestData.QueryResult.Intent.DisplayName.ToLower().Trim() == "otherinformationintent")
            {
                ResponseString = KB4.Search(question, par);
            }
            if (ResponseString.Trim() =="")
            {
                switch (question)
                {
                    case MasterProgrammeAvailable:
                        ResponseString = "The are a number of masters programme available at ISS NUS. Namely: (1) Master of Technology in Enterprise Business Analytics, (2) Master of Technology in Digital Leadership, (3) Master of Technology in Intelligent Systems, (4) Master of Technology in Software Engineering.";
                        break;
                    case ProgrammeAvailableAtISSNUS:
                        ResponseString = "There are two areas of programmes in ISS NUS: (1) Executive Education, (2) Graduate programmes, Stackable programmes are a class of flexible programmes designed for working professionals. You can learn more by visiting <a href='https://www.iss.nus.edu.sg/' target='_bank'>ISS web site</a>.";
                        break;
                    case ConductPostGraduateProgramme:
                        ResponseString = "Yes, ISS NUS conducts post graduate programmes each year. They are essentially masters programmes.";
                        break;
                }
            }
          
            if (ResponseString =="") ResponseString= "I am sorry, I do not have a comment at this moment. Can you try again?";
            return ResponseString;
        }

    }
}
