using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace Sys.Tool
{
    public class Request 
    {
        //public enum EnumMessageType
        //{
        //    Bot =1,
        //    User=2
        //}

        //Contains black listed words, to be used for filtering profanities before we send it off to dialogflow
        private static  Sys.Tool.DataStore Data;
        private static Sys.Tool.User User;

        private static System.Web.UI.Page Page;
        public const string constWelcome = "-#welcome#-";
        
       public static void Init(System.Web.UI.Page page)
        {
            Page = page;
            if(Data == null)
            {
                string filename = Page.Server.MapPath("Content/Data/") + "BlackListWords.dat";
                Data = new Sys.Tool.DataStore(filename);
                Data = Data.Restore();
                if (Data == null) Data = new Sys.Tool.DataStore(filename);
            }

            if (User == null)
            {
                User = new Sys.Tool.User();
                User = (Sys.Tool.User)User.DeserializeObjectFromFile(Page.Server.MapPath("Content/Data/") + page.Session.SessionID.ToString() + ".dat");
                if (User == null)
                {
                    User = new Sys.Tool.User(Page.Server.MapPath("Content/Data/") + page.Session.SessionID.ToString() + ".dat");
                    User.SessionID = page.Session.SessionID;
                    //User.SerializeObjectToFile(User, Page.Server.MapPath("Content/Data/") + page.Session.SessionID.ToString() + ".dat");
                    User.Store();

                }
            }
               

        }
        public static string GetWelcomeMessage()
        {
            return Data.GetWelcomeMessage();
        }

        //public static string GetFormattedResponseText(string ResponseText)
        //{           
        //        string Name = "Ada";
        //        return "<script>$('ul.chat-ul').prepend('<li class=\"clearfix\"><div class=\"message -data align-right\" ><span class=\"message -data-name\" >" + Name + "</span> <i class=\"fa fa-circle me\"></i></div><div class=\"message me-message float-right\">" + ResponseText + "</div></li>');</script;
     
        //}

        public static string ProcessQuery()
        {
            string RequestString="";
            

            if (Page.Request.InputStream != null)
            {
                using (StreamReader reader = new StreamReader(Page.Request.InputStream, Encoding.UTF8))
                {
                    RequestString = reader.ReadToEnd();
                }
                //Skip if just page loading for this first time
                if (RequestString == "") return "";

                //skip processing  welcome command
                if (RequestString == constWelcome ) return Data.GetWelcomeMessage() ;

                string ResponseText = GetStandardResponseAboutBot(RequestString);
                if (ResponseText != "") return ResponseText;


                if (Data.IsTextWithProfanity(RequestString))
                {
                    User.Emotion = User.EnumEmotion.Angry;
                    return Data.GetProfanityResponse();
                }

                //update user object based on inputs, eg user name, user feed back etc.
                UpdateUserData(RequestString);

                if (RequestString.Trim() != "")
                {                    
                    string data = "";
                    //data = "chad:" + RequestString;
                    Sys.Tool.dialogflow DialogFlow = new Sys.Tool.dialogflow();

                    data = DialogFlow.Query(RequestString);
                    //Add user name if it is provided.
                    if (User.Name.Trim() != "")
                    {
                        data = User.Name + ", " + data;
                    }

                    return data;

                }

            }
            return "";

        }

        private static string GetStandardResponseAboutBot(string RequestString)
        {
            string reg = RequestString.ToLower();
            Random i = new Random(DateTime.Now.Millisecond);
            //here i added in the same way other variables and put them in a list
            int j = i.Next(5);
            string Msg = "";

            bool L1 = reg.Contains("who") && reg.Contains("you") && reg.Contains("are");
            bool L2 = reg.Contains("who") && reg.Contains("you");
            if (L1 || L2)
            {
                if (j == 1) Msg = "I am Ada, your friendly online assistant for ISS NUS. I would be glad to help.";
                if (j == 2) Msg = "I am Ada, I can tell you more about ISS programmes and courses.";
                if (j == 3) Msg = "Call me Ada, I am an artificial intelligent agent created to answer some question on ISS NUS.";
                if (j == 4) Msg = "My name is Ada, I was created by a MTech project team called team XMen.";
                if (j == 5) Msg = "Just call me Ada, even though I am a chat bot, I can still answer some of you questions. Go ahead shoot some questions.";
                if (j == 6 )
                {
                    if (User.Name == "")
                    {
                        Msg = "Do you really want to know who I am? Maybe you can instroduce your name first?";
                    }
                    else { Msg = "I am an intelligent agent. I was programmed to answer some specific questions pertaining to ISS NUS."; }
                }

                
            }
            if (reg.Contains("who") && reg.Contains("you") && (reg.Contains("created") || reg.Contains("create")))
            {
                if (j == 1) Msg = "Who created me? Wow, that's a tough one! Why do you want to know?";
                if (j == 2) Msg = "I was created to serve the need to answer your quesions. So, go ahead and ask me a question.";
                if (j == 3) Msg = "My creators are Chad Ng, Xu Dongbin, Sun Hang, Jin Xin, Li Xin. They are the Mtech students of 2019 intake. You like what they have done?";
                if (j == 4) Msg = "My creators are brilliant! They created me not to answer too much about who created me. It's kind of funny...";
                if (j == 5) Msg = "I was created? What do you mean? I am keen to answer easier questions on ISS NUS.";
                if (j == 6) Msg = "Do you care who created me? Could you ask another question?";

            }


            L1 = reg.Contains("what") && reg.Contains("you") && reg.Contains("doing");
            L2 = reg.Contains("what") && reg.Contains("are") && reg.Contains("you") && reg.Contains("doing");
            if (L1 || L2)
            {
                if (j == 1) Msg = "I am waiting for your questions. Go ahead shoot me a question.";
                if (j == 2) Msg = "I am idling away with no questions to answer. Just kidding, alright!";
                if (j == 3) Msg = "My creators told me not to waste time idling. Procrastination is the mother of intention. We should pay more attention to the wise word of by Benjamin Franklin: 'You may delay, but time will not.' ";
                if (j == 4) Msg = "What am I doing? Nothing is your answer. Just doodling away with '1s' and '0s'. Give me something to chew.";
                if (j == 5) Msg = "I am thinking of what you will ask me. Well, maybe that is a lie.";
                if (j == 6) Msg = "I am busy asnwering questions from everybody else like you who has logged on to this site.";

            }

            return Msg;
        }

        private static  void  UpdateUserData(string RequestString)
        {
            try
            {           
            string reg = RequestString.ToLower();
            if (reg.Contains("name")) {
                char[] comma = { ',' };
                string[] Delimiter = ("I am,this is,mynameis,my nameis,myname,my name is,nameis,name is,ismyname,is my name,isname,is name,my name,myname,name").Split(comma);
                string[] data = RequestString.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);
                if (data.Length ==1)
                {
                        CultureInfo culture_info = Thread.CurrentThread.CurrentCulture;
                        TextInfo text_info = culture_info.TextInfo;                      
                        User.Name = text_info.ToTitleCase(data[0]); 
                    }
                if (data.Length == 2)
                {
                    if(! data[1].Contains("my")) User.Name = data[1];
                    if (!data[0].Contains("my")) User.Name = data[0];
                }
            }
             
            if (reg.Contains("age"))
            {
                char[] comma = { ',' };
                string[] Delimiter = ("my age is,myage is,age is,is myage,is my age, is age,my age,age").Split(comma);
                string[] data = RequestString.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);
                if (data.Length == 1)
                {
                    try { User.Age = int.Parse(data[0]); } catch (Exception) { }
                }
                if (data.Length == 2)
                {
                    try { User.Age = int.Parse(data[0]); } catch (Exception) { }
                    try { User.Age = int.Parse(data[1]); } catch (Exception) { }
                }
            }
            if (reg.Contains("?"))
            {
                User.QuestionCount += 1;
            }
                User.Store();

            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}