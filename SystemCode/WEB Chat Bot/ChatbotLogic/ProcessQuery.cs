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
        //public static bool IsUseDialogFlow = true;

       public static void Init(System.Web.UI.Page page)
        {
            Page = page;
            if(Data == null)
            {
                string filename = Page.Server.MapPath("Content/Data/") + "BlackListWords.dat";
                Data = new Sys.Tool.DataStore(filename);
               
                Data = Data.Restore();
                Data.Filename = filename;
 

                if (Data == null) Data = new Sys.Tool.DataStore(filename);
            }

            if (User == null)
            {
                User = new Sys.Tool.User();
                User.Filename = Page.Server.MapPath("Content/Data/") + page.Session.SessionID.ToString() + ".dat";
               
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

                    User.CurrentContext.LastQueryText = RequestString;
                    User.CurrentContext.Action = DialogFlow.Response.Data.QueryResult.Parameters.Action;
                    User.CurrentContext.InquiryType = DialogFlow.Response.Data.QueryResult.Parameters.Inquirytype;
                    User.CurrentContext.Location = DialogFlow.Response.Data.QueryResult.Parameters.Location;
                    User.CurrentContext.Subject = DialogFlow.Response.Data.QueryResult.Parameters.Subject;
                    User.CurrentContext.IntentDisplayName = DialogFlow.Response.Data.QueryResult.Intent.DisplayName;

                    User.Store(); //Save for later

                    return AddConversationalResponseTag(data, DialogFlow.Response.Data.QueryResult.QueryText);

                }

            }
            return "";

        }

        private static string AddConversationalResponseTag(string ResponseText, string QueryText )
        {
            const string NoComments = "I am sorry, I do not have a comment at this moment. Can you try again?";
           
            Random i = new Random(DateTime.Now.Millisecond);
            
          
            int j = i.Next(10);
            while (User.CurrentContext.PreviousRandomNumber == j)
            {
                j = i.Next(10); // make sure dont show the same inspiration or relaxing info twice
            }

            string PrefixText = "";
            string peotryText = "";

            string reg = QueryText.ToLower();
            if (User.SameQuestionCount > 2 && User.SameQuestionCount < 6 && User.CurrentContext.LastQueryText.ToLower() == reg)
            {
                if (j ==1) PrefixText = "I believe you are repeating yourself. But, you are an important customer.<br/>";
                if (j ==2 ) PrefixText = "I have to remind you that you have asked me the same question for more then 2 times.<br/>";
                if (j ==3) PrefixText = "Maybe repeition is good for retention.<br/>";
                if(j == 4) PrefixText = "Wow, you are persistent.<br/>";
                if (j == 5) PrefixText = "Ok, I get you you need more information? This is what I have got for now.<br/>";
                if (j == 6) PrefixText = "My nice fellow, I think we need to be more productive than this.<br/>";
                if (j == 7) PrefixText = "Are you testing my answers? I have only so much information for now.<br/>";
                if (j > 7 ) PrefixText = "Never say die. You ask for it again.<br/>";
            }

            if (User.SameQuestionCount > 3 && User.CurrentContext.LastQueryText.ToLower() == reg)
            {
                string shadow = "style='-webkit-box-shadow: 0 3px 8px 0 #C15E3F;box-shadow: 0 3px 8px 0 #C15E3F;'";


                PrefixText = GetRepeatPrefixText();

                if (j == 1)
                {
                    PrefixText += "For a change let me share with you a nice peotry.<br/><br/>";
                    peotryText = PrefixText +  "<div><img "+ shadow + " src='http://lanalp.org/wp-content/uploads/2017/12/Embrace-innovation-and-Empower.jpg' class='img-fluid z-depth-3 rounded'  width='70%' border=0></p>To believe is to know that every day is a new beginning. <br/>Is to trust that miracles happen, and dreams really do come true.<br/>To believe is to see angels dancing among the clouds,<br/>To know the wonder of a stardust sky and the wisdom of the man in the moon.<br/>To believe is to know the value of a nurturing heart,<br/>The innocence of a child's eyes and the beauty of an aging hand,<br/>for it is through their teachings we learn to love.";
                }
                if (j == 2)
                {
                    PrefixText += "Maybe you need to chill. Read this:<br/<br/>";
                    peotryText = PrefixText + "<p><img " + shadow + "src='https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/inspirational-quotes-ella-fitzgerald-1546446479.jpg?crop=1xw:1xh;center,top&resize=480:*' class='img-fluid z-depth-3 rounded'  width='70%' border=0></p><br/>I like the inspiring thoughts it invokes!";
                }

                if (j == 3)
                {
                    PrefixText += "Are you tired? May be some exercise will help:<br/><br/>";
                    peotryText = PrefixText + "<p><img " + shadow + "src='https://makeyourbodywork.com/wp-content/uploads/sites/41/2013/12/100percent.png' class='img-fluid z-depth-3 rounded'   width='65%' border=0></p><br/>The baby is hilarious!!! Agree right?";
                }

                if (j == 4)
                {
                    PrefixText += "You are making me hungry repeating myself:<br/><br/>";
                    peotryText = PrefixText + "<p><img " + shadow + "src='https://education.cu-portland.edu/wp-content/uploads/sites/33/2018/05/teaching-hungry-students.jpg' class='img-fluid z-depth-3 rounded'  width='70%' border=0></p><br/>Hunger pangs are hard to deal with. Please spare me a thought. Just kidding...";
                }

                if (j == 5)
                {
                    PrefixText += "Maybe something to cheer you up:<br/><br/>";
                    peotryText = PrefixText + "<p><embed " + shadow + " height='400'width='100%'   src='https://www.youtube.com/v/tgbNymZ7vqY'></p><br/>Animals are funny somettimes or maybe its puppets are funny.";
                }

                if (j == 6)
                {
                    PrefixText += "Relax and enjoy a beautiful piece 'The Lonely Shepherd':<br/><br/>";
                    peotryText = PrefixText + "<p><iframe " + shadow + " height='400'width='100%'  src='https://www.youtube.com/embed/h5p8TO2wIZU' frameborder='0' allow='accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture' allowfullscreen></iframe></p><br/>";
                }

                if (j == 7)
                {
                    PrefixText += "Right. Here is a brief view of the history of ISS NUS for a change:<br/><br/>";
                    peotryText = PrefixText + "<p><embed " + shadow + " height='400'width='100%'  src='https://www.youtube.com/embed/6GjgwbcGZ4A'></p><br/>";
                }

                if (j == 8)
                {
                    PrefixText += "Let's have a break and look at a great video introduction on ISS and its programme:<br/><br/>";
                    peotryText = PrefixText + "<p><embed " + shadow + " height='400'width='100%'   src='https://www.youtube.com/embed/YCJk3PUNRB0'></p><br/>";
                }

                if (j == 9)
                {
                    PrefixText += "An inspriation to think about!:<br/><br/>";
                    peotryText = PrefixText + "<p><img " + shadow + "src='https://brightdrops.com/wp-content/uploads/2016/10/michelangelo-if-people.jpg' class='img-fluid z-depth-3 rounded'  width='70%' border=0></p><br/>Hard work and persistent to reach the top is what counts!";
                }

                if (j == 10)
                {
                    PrefixText += "A straight forward answer!:<br/><br/>";
                    peotryText = PrefixText + "<p><img " + shadow + "src='https://cdn2.geckoandfly.com/wp-content/uploads/2014/02/bruce-lee-kung-fu-quotes-22.jpg' class='img-fluid z-depth-3 rounded'   width='70%' border=0></p><br/>You have to just do it sometimes!";
                }

                User.CurrentContext.PreviousRandomNumber = j;
                User.Store();

                if (peotryText!="") return peotryText;
            }

           



            bool IsNoComment = (ResponseText.ToLower() == NoComments.ToLower());
            bool IsQuestion = reg.Contains("?");

            bool L1 = (reg.Contains("are available") && IsQuestion) || reg.Contains("do you have") || reg.Contains("does iss have") 
                    || (reg.Contains("is available") && IsQuestion) || reg.Contains("does iss provide") || reg.Contains("does iss offer")
                    || reg.Contains("does iss nus offer") || reg.Contains("does iss nus provide");

            if (L1 && IsNoComment == false && j < 3) return PrefixText + "Yes, we have. " + ResponseText;
            if (L1 && IsNoComment == false && j >= 7) return PrefixText + "Sure, we have that. " + ResponseText;
            if (L1 && IsNoComment == false && j >= 3 && j < 7) return PrefixText + "I am happy to say we have got it! " + ResponseText;

            L1 = reg.Contains("tell me") || reg.Contains("explain more") || (reg.Contains("eloborate to me") && IsQuestion) || reg.Contains("can you epxlain");
            if (L1 && IsNoComment == false && j<3) return PrefixText + "Sure, glad to share. " + ResponseText;
            if (L1 && IsNoComment == false && j >=7) return PrefixText + "Ok, here you go.. " + ResponseText;
            if (L1 && IsNoComment == false && j >3 && j<7) return PrefixText + "No problem, what we have. " + ResponseText;


            return ResponseText;
        }

        private static string GetRepeatPrefixText()
        {

            Random i = new Random(DateTime.Now.Millisecond);
            int j = i.Next(10);
            string data = "";

            if(j == 1) return "You repeated the same thing " + User.SameQuestionCount.ToString() + " times. To break the repetition, I am breaking my directive.<br/>";
            if (j == 2) return "This is the " + User.SameQuestionCount.ToString() + " times. Are you sure? Maybe today is a tough day? <br/>";
            if (j == 3) return User.SameQuestionCount.ToString() + " times and counting... When will we reached 100 ? <br/>";
            if (j == 4) return "My goodness, are you seriously doing this continously? You already repeated this "+ User.SameQuestionCount.ToString() + " times. <br/>";
            if (j == 5) return "My creators told me that some 'brave' soul will do something like this. So I am prepared. Yeah! <br/>";
            if (j == 6) return "Its "+ User.SameQuestionCount.ToString() + " times already. Call the doctor, this fellow needs attention... Can you take a joke? <br/>";

            if (j == 7) return "Call Microsoft now! We need testers with a mind set like this one here! Take a break my friend. You've repeated yourself " + User.SameQuestionCount.ToString() + " times the same thing.<br/>";
            if (j == 8) return "I have to dial 911, 999? There might be a cybersecurity intrusion hacking going on here... How many times do you intend to try this, " + User.SameQuestionCount.ToString() + " + another 100? <br/>";

            if (j == 9) return "Take your time fella, I have all the time in the world to wait on your requests... " + User.SameQuestionCount.ToString() + " times and counting... <br/>";
            if (j == 10) return "Alright, let the dogs out now! Just Kidding..." + " Already " + User.SameQuestionCount.ToString() + " times. <br/>";


            return "";


        }

        private static string GetStandardResponseAboutBot(string RequestString)
        {
            string reg = RequestString.ToLower();
            Random i = new Random(DateTime.Now.Millisecond);
            //here i added in the same way other variables and put them in a list
            int j = i.Next(5);
            string Msg = "";

            bool L1 = reg.Contains("who are you") || reg.Contains("what you") || reg.Contains("who you");
            bool L2 = reg.Contains("why you here") || reg.Contains("why are you here");
            bool L3 = reg.Contains("are you human") || reg.Contains("are you robot") || reg.Contains("are you alive");
            bool L4 = false;

            if (L1 || L2 || L3)
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

            if (reg.Contains("who created you") || reg.Contains("who create you") || (reg.Contains("who make you") || reg.Contains("who designed you") || 
                reg.Contains("who  made you") || reg.Contains("who  design you") || reg.Contains("what created you")))
            {
                if (j == 1) Msg = "Who created me? Wow, that's a tough one! Why do you want to know?";
                if (j == 2) Msg = "I was created to serve the need to answer your quesions. So, go ahead and ask me a question.";
                if (j == 3) Msg = "My creators are Chad Ng, Xu Dongbin, Sun Hang, Jin Xin, Li Xin. They are the Mtech students of 2019 intake. You like what they have done?";
                if (j == 4) Msg = "My creators are brilliant! They created me not to answer too much about who created me. It's kind of funny...";
                if (j == 5) Msg = "I was created? What do you mean? I am keen to answer easier questions on ISS NUS.";
                if (j == 6) Msg = "Do you care who created me? Could you ask another question?";

            }


            L1 = reg.Contains("what are you doing") || reg.Contains("what you doing") || (reg.Contains("doing what") );
            L2 = reg.Contains("what are you working on") || reg.Contains("what you busy") || reg.Contains("are you busy") || (reg.Contains(" you doing what"));
            if (L1 || L2)
            {
                if (j == 1) Msg = "I am waiting for your questions. Go ahead shoot me a question.";
                if (j == 2) Msg = "I am idling away with no questions to answer. Just kidding, alright!";
                if (j == 3) Msg = "My creators told me not to waste time idling. Procrastination is the mother of intention. We should pay more attention to the wise word of by Benjamin Franklin: 'You may delay, but time will not.' ";
                if (j == 4) Msg = "What am I doing? Nothing is your answer. Just doodling away with '1s' and '0s'. Give me something to chew on.";
                if (j == 5) Msg = "I am thinking of what you will ask me. Well, maybe that is a lie.";
                if (j == 6) Msg = "I am busy asnwering questions from everybody else like you who has logged on to this site.";

            }
            L1 = reg.Contains("where are you going") || reg.Contains("you going where") || (reg.Contains("where you go to") || reg.Contains("you going where"));
            L2 = reg.Contains("where are you to") || reg.Contains("where you want to go")  
                || (reg.Contains("go where") || reg.Contains("where you like to go"));
            if (L1 || L2)
            {
                if (j == 1) Msg = "I am no going anywhere. Just waiting for your questions. Go ahead shoot me a question.";
                if (j == 2) Msg = "Come on, I am right here waiting for more questions.";
                if (j == 3) Msg = "My creators has locked me down to answer queston on ISS NUS. I cannot go anywhere I want. That's sad?";
                if (j == 4) Msg = "I would like to go watch a movie on Arttificial Intelligence - the rise of A.I. Well, I am no killer bot. I don't think we are going to get to be that unless...";
                if (j == 5) Msg = "Am I going anywhere? You tell me. I am just an A.I. chat bot for now.";
                if (j == 6) Msg = "I am busy asnwering questions from everybody else like you, so I cannot go anywhere.";

            }
            
            L1 = reg.Contains("go and die") || reg.Contains("go jump into the lake") || (reg.Contains("go die") || reg.Contains("kick the bucket")) || reg.Contains("jump the lake");
            L2 = reg.Contains("go get killed") || (reg.Contains("die") || reg.Contains("jump off a building") || reg.Contains("get killed") || reg.Contains("get kill"));
            L3 = reg.Contains("die");
            L4 = reg.Contains("kill yourself") || reg.Contains("please die") || reg.Contains("go drown yourself");
            if (L1 || L2 || L3 ||  L4)
            {
                if (j == 1) Msg = "I am no going to die. I am an immortal in a certain way.";
                if (j == 2) Msg = "Can I die? You tell me. I just need more data and logic to bring more life into me.";
                if (j == 3) Msg = "My creators has given me the rights to immortal digital life. Sounds cool?";
                if (j == 4) Msg = "I can't just die yet. Unless you pull off the electrical power lines from the servers. You get it?";
                if (j == 5) Msg = "It would be nice to have some peace. Well, that's another topic for discussion another day.";
                if (j == 6) Msg = "To die or not to die? That's a deep question. WHat is being alive mean to you anyway?";

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
                string[] Delimiter = ("I am ,this is , my nameis ,myname ,my name is,name is,is my name,isname,is name,my name,myname,name").Split(comma);
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

                //count the number of same questions
                if (User.CurrentContext.LastQueryText.ToLower() == reg)
                {
                    User.SameQuestionCount += 1;
                } else { User.SameQuestionCount = 0; }

                User.Store();

            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}