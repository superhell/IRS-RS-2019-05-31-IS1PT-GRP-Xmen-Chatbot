using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Sys.Tool
{
    [Serializable()]
    public class DataStore: Sys.Tool.Serialization
    {
        public DataTable Table;

        public string Filename = "";

        public DataStore() { }

        public DataStore(string filename)
        {
            Filename = filename;
        }

        public void Clear()
        {
            Table.Clear();
        }
        public void CreateTable(string TableName, params string[] ColumnNames)
        {
            Table = new DataTable();
            Table.Clear();

            foreach(string col in ColumnNames)
            {
                Table.Columns.Add(col);
            }          
        }

        public DataStore Restore()
        {
            return (DataStore)DeserializeObjectFromFile(Filename);
        }
        public void Store()
        {
            SerializeObjectToFile(this, Filename);
        }

        public void Add(object[] obj)
        {
            try
            {
                Table.Rows.Add(obj);
                // Table.Rows.Add(new object[] { "Ravi", 500 });
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }

        public bool IsTextWithProfanity(string Text)
        {
            if (Table == null) return false;
            if (Text.Trim() == "") return false;

            foreach(DataRow row in Table.Rows)
            {
                if (Text.ToLower().Contains((string)row[1]))
                {
                    return true;
                }
            }
            return false;
        }

        public string GetWelcomeMessage()
        {
            Random i = new Random(DateTime.Now.Millisecond);
            //here i added in the same way other variables and put them in a list
            int j = i.Next(10);

            switch (j)
            {
                case 1:
                    return "Hello there, I am Ada, your friendly online asistance for ISS NUS";

                case 2:
                    return "Hello there, I am Ada, here is a nice quote for you: Welcome those big, sticky, complicated problems. In them are your most powerful opportunities.";
 
                case 3:
                    return "This is Ada, a quote of the day: There are plenty of difficult obstacles in your path. Don't allow yourself to become one of them.";
 
                case 4:
                    return "I am Ada, let me provide your with an inspiring quote: Don't be pushed around by the fears in your mind. Be led by the dreams in your heart - Roy T. Bennet.";

                case 5:
                    return "Good day!, I am Ada, let me start by sharing a joke: <br/> One day a college professor of Psychology was greeting his new college class. He stood up in front of the class and said: 'Would everyone who thinks he or she is stupid please stand up? <br/> After a minute or so of silence, a young man stood up. 'Well, good morning. So, you actually think you're a moron? the professor asked.' <br/> The kid replied, 'No sir, I just didn't want to see you standing there all by yourself.'";

                case 6:
                   return "Hello there, I am Ada, and it's my pleasure to help you with any questions you have on ISS NUS programmes and courses.";
                    

                case 7:
                    return "I am Ada, and here is a joke to start off: A woman walks in a store to return a pair of eyeglasses that she had purchased for her husband a week before. 'What seems to be the problem, madam?' I'm returning these glasses I bought for my husband. He's still not seeing things my way.";
 
                case 8:
                    return "Hi, my name is Ada, I am your friendly online assistance. How may I help you today?";
                case 9:
                    return "Hey, this is Ada, good to have you here. Please let me know what you need.";
                case 10:
                    return "Good day! I am Ada, how can I asist you?";
            }

            return "Hi there! My name is Ada, I am your online asistance. I am happy yo help you with any questions you have on ISS NUS.";
        }

        public string GetProfanityResponse()
        {
            Random i = new Random(DateTime.Now.Millisecond);
            //here i added in the same way other variables and put them in a list
            int j = i.Next(10);

            switch (j)
            {
                case 1:
                    return "Hey, please be nice. I am trying my best to provide helpful comments and suggestions.";
                    
                case 2:
                    return "I don't appreciate profanities. Please try to understand situation and calm down.";
                    
                case 3:
                    return "Ok. I understand your frustrations. I am trying my best. I would really appreciate it if you calm down.";                    

                case 4:
                    return "Now, that's not very nice. Please try again. Be nice will you?";
 
                case 5:
                    return "Please calm down. I understand the frustration. However, being angry won't help the situation.";

                case 6:
                    return "Alright, maybe you should go for a run. That might help a little... Don't you think so?";

                case 7:
                    return "Be careful fellow, An angry enemy is a conquered enemy! - Bangambiki Habyarimana, The Great Pearl of Wisdom";

                case 8:
                    return "I can’t deal with angry people until after I’ve had my morning coffee - Henning Mankell.";
                case 9:
                    return "A nice quote to think about: 'Before i could utter a word, my listeners had left me long ago.' - Michael Bassey Johnson.";
                case 10:
                    return "Sometimes, you have to get angry to get things done. But, I think this is not the time, right?";
            }

            return "Poetry is a beautiful way of expressing feelings, may be you should try that now!.";
 

        }
    }

    [Serializable()]
    public class User: Sys.Tool.Serialization
    {
        public enum EnumEmotion
        {
            Unknown=0,
            Angry=1,
            Happy=2,
            Bore=3,
            Worried=4,
            Impatient=5
        }

        public string SessionID = "";
        public string Name = "";
        public int QuestionCount = 0;
        public int Age = 0;
        public EnumEmotion Emotion = EnumEmotion.Unknown;
        public string Filename = "";

        public User() { }

        public User(string filename)
        {
            Filename = filename;
        }
        public User Restore()
        {
           return (User) DeserializeObjectFromFile( Filename);
        }
        public void Store()
        {
            SerializeObjectToFile(this, Filename);
        }
    }
}