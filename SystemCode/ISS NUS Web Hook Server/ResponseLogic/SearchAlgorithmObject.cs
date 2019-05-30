using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Tool
{
    [Serializable()]
    public  class KnowledgeObject: Sys.Tool.Serialization
    {
        //tree structure objects
        public Node Node;
        public string Filename = "";
        private int _Count = 0;
        public  int Count { get { return _Count; } } 

        public KnowledgeObject(string filename)
        {
            this.Filename = filename;
        }
        public void Store()
        {
            SerializeObjectToFile(this, Filename);
        }
        public KnowledgeObject Restore()
        {
            return (KnowledgeObject)DeserializeObjectFromFile(Filename);
        }

        public void SetUpKnowledgeBase(DataTable Table)
        {
            if(Table == null) return;
            if (Table.Rows.Count == 0) return;
            Sys.Tool.Node n= null;
            bool IsRootNode = true;
            string[] data;
            char[] delimiter = { '|' };
            //string[] parameters = { "location", "inquirytype", "subject", "questionkey", "action", "value" };
            int i = 0;

            n = new Sys.Tool.Node(i.ToString());

            foreach (DataRow row in Table.Rows)
            {
                //if((string)row[0]=="19"  ) { 

                //sno, location, inquirytype, subject, questionkey, action, value
                //location
                    data = (row["location"].ToString()).Split(delimiter);
                n.Location.Add(""); //add a blank location, necesscary for L3 logic
                foreach (var item in data) { n.Location.Add(item.ToLower().Trim()); }

                //inquirytype
                data = (row["inquirytype"].ToString()).Split(delimiter);
                foreach (var item in data) { n.InquiryType.Add(item.ToLower().Trim()); }

                //subject
                data = (row["subject"].ToString()).Split(delimiter);
                //n.Subject.Add(""); //add a blank location, necesscary for L3 logic
                foreach (var item in data) { n.Subject.Add(item.ToLower().Trim()); }

                //questionkey
                data = (row["questionkey"].ToString()).Split(delimiter);
                n.QuestionKey.Add(""); //add a blank location, necesscary for L3 logic
                foreach (var item in data) { n.QuestionKey.Add(item.ToLower().Trim()); }

                //action
                data = (row["action"].ToString()).Split(delimiter);
                foreach (var item in data) { n.Action.Add(item.ToLower().Trim()); }

                n.Value = (string)row["value"];

                if (IsRootNode)
                {                   
                    this.Node = n;
                    this._Count += 1;
                } else
                {
                    if(this.Node.Add(n)) this._Count += 1; 
                }

                n = new Sys.Tool.Node(i.ToString());
                i += 1;
                IsRootNode = false;
             // }
            }
        }
            public void SetUpKnowledgeBase()
        {

            Sys.Tool.Node n = new Sys.Tool.Node("ISSNUS");
            n.Action.Add("available"); n.Action.Add("have"); n.Action.Add("conduct"); n.Action.Add("offer");
            n.InquiryType.Add("programme"); n.InquiryType.Add("program"); n.InquiryType.Add("programmes"); n.InquiryType.Add("programs");
            n.QuestionKey.Add(""); n.QuestionKey.Add("what"); n.QuestionKey.Add("does");
            n.Subject.Add(""); n.Subject.Add("programme"); n.Subject.Add("program"); n.Subject.Add("programmes"); n.Subject.Add("programs");
            n.Location.Add(""); n.Location.Add("iss"); n.Location.Add("nus"); n.Location.Add("iss nus"); n.Location.Add("issnus");
            n.Value = "There are Graduate Programmes and Stackable Certificate Programmes available at ISS NUS.";
            this.Node = n;
          

            n = new Sys.Tool.Node("Graduate Diploma in Systems Analysis");
            n.Action.Add("available"); n.Action.Add("have"); n.Action.Add("conduct"); n.Action.Add("offer"); ; n.Action.Add("is");
            n.InquiryType.Add("graduate diploma"); n.InquiryType.Add("systems analysis"); n.InquiryType.Add("diploma");
            n.QuestionKey.Add(""); n.QuestionKey.Add("what"); n.QuestionKey.Add("does");
            n.Subject.Add(""); n.Subject.Add("graduate diploma"); n.Subject.Add("systems analysis"); n.Subject.Add("diploma");
            n.Location.Add(""); n.Location.Add("iss"); n.Location.Add("nus"); n.Location.Add("iss nus"); n.Location.Add("issnus");
            n.Value = "The Graduate Diploma in Systems Analysis programme (GDipSA) is designed for non-IT graduates intending to craft a new career path in the IT industry. IT graduates and professionals who wish to advance their careers in their current field and recognise the need to equip themselves with the latest IT knowledge and skills to stay relevant may apply as well.";
            this.Node.Add(n);

            n = new Sys.Tool.Node("Master of Technology in Intelligent Systems");
            n.Action.Add("available"); n.Action.Add("have"); n.Action.Add("conduct"); n.Action.Add("offer"); n.Action.Add("is");
            n.InquiryType.Add("master of technology"); n.InquiryType.Add("master programme");
            n.QuestionKey.Add(""); n.QuestionKey.Add("what"); n.QuestionKey.Add("does");
            n.Subject.Add(""); n.Subject.Add("master of technology"); n.Subject.Add("master of technology in intelligent systems"); n.Subject.Add("intelligent systems"); n.Subject.Add("intelligent system");
            n.Location.Add(""); n.Location.Add("iss"); n.Location.Add("nus"); n.Location.Add("iss nus"); n.Location.Add("issnus");
            n.Value = "The NUS Master of Technology in Intelligent Systems programme is targeted at working professionals who wish to be able to design and build systems that utilise Artificial Intelligence and other Smart Systems techniques. Application areas are wide and diverse, and include robotics, autonomous vehicles, intelligent sensing systems, Internet of Things, Smart City applications and Industry 4.0 applications, as well as applications within business and commerce.";
            this.Node.Add(n);


            n = new Sys.Tool.Node("Master of Technology in Enterprise Business Analytics");
            n.Action.Add("available"); n.Action.Add("have"); n.Action.Add("conduct"); n.Action.Add("offer"); n.Action.Add("is");
            n.InquiryType.Add("master of technology"); n.InquiryType.Add("master programme");
            n.QuestionKey.Add(""); n.QuestionKey.Add("what"); n.QuestionKey.Add("does");
            n.Subject.Add(""); n.Subject.Add("master of technology"); n.Subject.Add("master of technology in enterprise business analytics"); n.Subject.Add("enterprise business analytics"); n.Subject.Add("business analytics");
            n.Location.Add(""); n.Location.Add("iss"); n.Location.Add("nus"); n.Location.Add("iss nus"); n.Location.Add("issnus");
            n.Value = "The NUS Master of Technology in Enterprise Business Analytics programme (MTech EBAC) is specifically designed to meet the industry demand for data scientists who can help organisations achieve improved business outcomes through data insights. It is best suited for professionals seeking to focus on the following - methodical data exploration and visualisation, diagnostic analytics, predictive modelling using statistical and machine learning techniques, text analytics, recommender systems, and big data engineering, etc.";
            this.Node.Add(n);

            n = new Sys.Tool.Node("Master of Technology in Software Engineering");
            n.Action.Add("available"); n.Action.Add("have"); n.Action.Add("conduct"); n.Action.Add("offer"); n.Action.Add("is");
            n.InquiryType.Add("master of technology"); n.InquiryType.Add("master programme");
            n.QuestionKey.Add(""); n.QuestionKey.Add("what"); n.QuestionKey.Add("does");
            n.Subject.Add(""); n.Subject.Add("master of technology"); n.Subject.Add("master of technology in software engineering"); n.Subject.Add("software engineering");
            n.Location.Add(""); n.Location.Add("iss"); n.Location.Add("nus"); n.Location.Add("iss nus"); n.Location.Add("issnus");
            n.Value = "The NUS Master of Technology in Software Engineering is designed to meet the industry demand for software engineers who can help Singapore organisations to realise the smart nation initiatives through building robust, reliable and scalable software systems. This programme is best suited for individuals who have a few years of experience in software engineering roles and are looking to further enhance their knowledge and skills in architecting scalable, secure and smart software systems.";
            this.Node.Add(n);

            n = new Sys.Tool.Node("Master of Technology in Digital Leadership");
            n.Action.Add("available"); n.Action.Add("have"); n.Action.Add("conduct"); n.Action.Add("offer"); n.Action.Add("is");
            n.InquiryType.Add("master of technology"); n.InquiryType.Add("master programme");
            n.QuestionKey.Add(""); n.QuestionKey.Add("what"); n.QuestionKey.Add("does");
            n.Subject.Add(""); n.Subject.Add("master of technology"); n.Subject.Add("master of technology in digital leadership"); n.Subject.Add("digital leadership");
            n.Location.Add(""); n.Location.Add("iss"); n.Location.Add("nus"); n.Location.Add("iss nus"); n.Location.Add("issnus");
            n.Value = "The Master of Technology in Digital Leadership programme focus on digital strategy and leadership will equip students with the critical thinking, hard and soft skills to become an effective leader. It will accelerate their career and enhance one's ability to take on greater roles and responsibilities in digital leadership. Students will be equipped with the right processes and people capabilities to ride the digital wave and to thrive in the digital economy. Our goal is also to help organisations to develop its next generation of IT and digital leaders.";
            this.Node.Add(n);

            n = new Sys.Tool.Node("Stackable Certificate Programme in Data Science");
            n.Action.Add("available"); n.Action.Add("have"); n.Action.Add("conduct"); n.Action.Add("offer"); n.Action.Add("is");
            n.InquiryType.Add("stackable certificate programme"); n.InquiryType.Add("stackable certificate programme in data science"); n.InquiryType.Add("programme in data science"); n.InquiryType.Add("data science");
            n.QuestionKey.Add(""); n.QuestionKey.Add("what"); n.QuestionKey.Add("does");
            n.Subject.Add(""); n.Subject.Add("stackable certificate programme"); n.Subject.Add("stackable certificate programme in data science"); n.Subject.Add("programme in data science"); n.Subject.Add("data science");
            n.Location.Add(""); n.Location.Add("iss"); n.Location.Add("nus"); n.Location.Add("iss nus"); n.Location.Add("issnus");
            n.Value = "The NUS-ISS Stackable Certificate Programme in Data Science, leading to the NUS Master of Technology in Enterprise Business Analytics is designed to meet the industry demand for data scientists who can help organisations achieve improved business outcomes through data insights. It is best suited for professionals who wish to enhance their existing skill sets to progress from the entry level to specialist or expert level positions in the data science and business analytics domain. ";
            this.Node.Add(n);

            n = new Sys.Tool.Node("ISS Stackable Certificate Programmes in Digital Solutions Development");
            n.Action.Add("available"); n.Action.Add("have"); n.Action.Add("conduct"); n.Action.Add("offer");
            n.InquiryType.Add("stackable certificate programmes"); n.InquiryType.Add("programmes in digital solutions development"); n.InquiryType.Add("digital solutions development");
            n.QuestionKey.Add(""); n.QuestionKey.Add("what"); n.QuestionKey.Add("does");
            n.Subject.Add(""); n.Subject.Add("stackable certificate programmes in digital solutions development"); n.Subject.Add("stackable certificate programmes");
            n.Location.Add(""); n.Location.Add("iss"); n.Location.Add("nus"); n.Location.Add("iss nus"); n.Location.Add("issnus");
            n.Value = "The Stackable Certificate Programmes in Digital Solutions Development course equips participants with necessary skills to design and develop IT solutions to resolve business problems, enable process automation and support smart living in our day to day life.";
            this.Node.Add(n);

            n = new Sys.Tool.Node("Stackable Certificate Programme in Artificial Intelligence");
            n.Action.Add("available"); n.Action.Add("have"); n.Action.Add("conduct"); n.Action.Add("offer");
            n.InquiryType.Add("stackable certificate programmes"); n.InquiryType.Add("programmes in artificial intelligence"); n.InquiryType.Add("artificial intelligence");
            n.QuestionKey.Add(""); n.QuestionKey.Add("what"); n.QuestionKey.Add("does");
            n.Subject.Add(""); n.Subject.Add("stackable certificate programme in artificial intelligence"); n.Subject.Add("stackable certificate programmes"); n.Subject.Add("programme in artificial intelligence");
            n.Location.Add(""); n.Location.Add("iss"); n.Location.Add("nus"); n.Location.Add("iss nus"); n.Location.Add("issnus");
            n.Value = "The Stackable Certificate Programme in Artificial Intelligence course equips participants with necessary skills to design and develop IT solutions to resolve business problems, enable process automation and support smart living in our day to day life.";
            this.Node.Add(n);

            n = new Sys.Tool.Node("Graduate Certificate in Intelligent Reasoning Systems");
            n.Action.Add("available"); n.Action.Add("have"); n.Action.Add("conduct"); n.Action.Add("offer");
            n.InquiryType.Add("graduate certificate "); n.InquiryType.Add("certificate in intelligent reasoning systems");
            n.QuestionKey.Add(""); n.QuestionKey.Add("what"); n.QuestionKey.Add("does");
            n.Subject.Add(""); n.Subject.Add("graduate certificate in intelligent reasoning systems"); n.Subject.Add("certificate in intelligent reasoning systems"); n.Subject.Add("intelligent reasoning systems"); ; n.Subject.Add("reasoning systems");
            n.Location.Add(""); n.Location.Add("iss"); n.Location.Add("nus"); n.Location.Add("iss nus"); n.Location.Add("issnus");
            n.Value = "The Graduate Certificate in Intelligent Reasoning Systems teaches how to build Intelligent Systems that solve problems by computational reasoning using captured domain knowledge and data. Example applications include, question answering systems such as IBM's Watson, personal assistants such as Amazon’s Alexa Skills and game-playing systems such as Google's AlphaGo.";
            this.Node.Add(n);


        }

        public void TestBinaryTree()
        {
            //******************************************************************
            //Test binary tree
            //******************************************************************
            Sys.Tool.Node n = new Sys.Tool.Node("0");
            n.Action.Add("0"); n.InquiryType.Add("0"); n.QuestionKey.Add("0"); n.Subject.Add("0"); n.Location.Add("0");
            n.Value = "value 0"; this.Node = n;

            n = new Sys.Tool.Node("1");
            n.Action.Add("1"); n.InquiryType.Add("1"); n.QuestionKey.Add("1"); n.Subject.Add("1"); n.Location.Add("1");
            n.Value = "value 1"; this.Node.Add(n);

            n = new Sys.Tool.Node("2");
            n.Action.Add("2"); n.InquiryType.Add("2"); n.QuestionKey.Add("2"); n.Subject.Add("2"); n.Location.Add("2");
            n.Value = "value 2"; this.Node.Add(n);

            n = new Sys.Tool.Node("3");
            n.Action.Add("3"); n.InquiryType.Add("3"); n.QuestionKey.Add("3"); n.Subject.Add("3"); n.Location.Add("3");
            n.Value = "value 3"; this.Node.Add(n);

            n = new Sys.Tool.Node("4");
            n.Action.Add("4"); n.InquiryType.Add("4"); n.QuestionKey.Add("4"); n.Subject.Add("4"); n.Location.Add("4");
            n.Value = "value 4"; this.Node.Add(n);

            n = new Sys.Tool.Node("5");
            n.Action.Add("5"); n.InquiryType.Add("5"); n.QuestionKey.Add("5"); n.Subject.Add("5"); n.Location.Add("5");
            n.Value = "value 5"; this.Node.Add(n);

            n = new Sys.Tool.Node("6");
            n.Action.Add("6"); n.InquiryType.Add("6"); n.QuestionKey.Add("6"); n.Subject.Add("6"); n.Location.Add("6");
            n.Value = "value 6"; this.Node.Add(n);

            n = new Sys.Tool.Node("7");
            n.Action.Add("7"); n.InquiryType.Add("7"); n.QuestionKey.Add("7"); n.Subject.Add("7"); n.Location.Add("7");
            n.Value = "value 7"; this.Node.Add(n);

            n = new Sys.Tool.Node("8");
            n.Action.Add("8"); n.InquiryType.Add("8"); n.QuestionKey.Add("8"); n.Subject.Add("8"); n.Location.Add("8");
            n.Value = "value 8"; this.Node.Add(n);

            n = new Sys.Tool.Node("9");
            n.Action.Add("9"); n.InquiryType.Add("9"); n.QuestionKey.Add("9"); n.Subject.Add("9"); n.Location.Add("9");
            n.Value = "value 9"; this.Node.Add(n);

            n = new Sys.Tool.Node("10");
            n.Action.Add("10"); n.InquiryType.Add("10"); n.QuestionKey.Add("10"); n.Subject.Add("10"); n.Location.Add("10");
            n.Value = "value 10"; this.Node.Add(n);

            n = new Sys.Tool.Node("11");
            n.Action.Add("11"); n.InquiryType.Add("11"); n.QuestionKey.Add("11"); n.Subject.Add("11"); n.Location.Add("11");
            n.Value = "value 11"; this.Node.Add(n);

            n = new Sys.Tool.Node("12");
            n.Action.Add("12"); n.InquiryType.Add("12"); n.QuestionKey.Add("12"); n.Subject.Add("12"); n.Location.Add("12");
            n.Value = "value 12"; this.Node.Add(n);

            n = new Sys.Tool.Node("13");
            n.Action.Add("13"); n.InquiryType.Add("13"); n.QuestionKey.Add("13"); n.Subject.Add("13"); n.Location.Add("13");
            n.Value = "value 13"; this.Node.Add(n);

            n = new Sys.Tool.Node("14");
            n.Action.Add("14"); n.InquiryType.Add("14"); n.QuestionKey.Add("14"); n.Subject.Add("14"); n.Location.Add("14");
            n.Value = "value 14"; this.Node.Add(n);

            //Search - Level 3 of Node B (8) - See Slide on "Custom knowledge base struture"
            Tool.SearchParameters pp = new Tool.SearchParameters();
            pp.Action = "8"; pp.QuestionKey = "8"; pp.Location = ""; pp.InquiryType = "8"; pp.Subject = "8";
            string ff = Search("", pp); //Passed

            //Search - Level 3 of Node B (10) - See Slide on "Custom knowledge base struture"
            pp = new Tool.SearchParameters();
            pp.Action = "10"; pp.QuestionKey = "10"; pp.Location = ""; pp.InquiryType = "10"; pp.Subject = "10";
            ff = Search("", pp); //Passed

            //Search - Level 3 of Node A (13) - See Slide on "Custom knowledge base struture"
            pp = new Tool.SearchParameters();
            pp.Action = "13"; pp.QuestionKey = "13"; pp.Location = ""; pp.InquiryType = "13"; pp.Subject = "13";
            ff = Search("", pp); //Passed

            //Search - Level 2 of Node A (5) - See Slide on "Custom knowledge base struture"
            pp = new Tool.SearchParameters();
            pp.Action = "5"; pp.QuestionKey = "5"; pp.Location = ""; pp.InquiryType = "5"; pp.Subject = "5";
            ff = Search("", pp); //Passed
            //******************************************************************
        }

        public string Search (string QueryText, SearchParameters Parameters)
        {
           Node node=  Node.Find(Parameters);
            if (node != null)
            {
                if (node.IsSearchSuccess)
                {
                    node.IsSearchSuccess = false;
                    return node.Value;
                } else { return ""; }
            }
            else return "";
        }
    }

    [Serializable()]
    public class Node
    {
     
        public enum EnumQuestionKeyType
        { 
            What=1,
            Where=2,
            How=3,
            Who=4,
            When=5
        }
        public enum EnumInquiryType
        {
            Programme=1,
            Course=2,
            Fee = 3,
            Requirement=4,
            Preparation=5,
            Duration=6,
            Funding=7,
            Overview=8,
            Benefit=9,
            
            
        }

        public ArrayList InquiryType;
        public ArrayList QuestionKey;
        public ArrayList Action;
        public ArrayList Subject;
        public ArrayList Location;

        public Node NodeA;
        public Node NodeB;
        public string Name = "";
        public string Value = "";
        public bool IsSearchSuccess = false;

       

        public Node(string name)
        {
            this.Name = name;
            InquiryType = new ArrayList();
            QuestionKey = new ArrayList();
            Action = new ArrayList();
            Subject = new ArrayList();
            Location = new ArrayList();
        }

        public bool Add(Sys.Tool.Node node)
        {
            if (NodeA == null)
            {
                NodeA = node;
                return true;
            }
            else if (NodeB == null)
            {
                NodeB = node;
                return true;
            }
            else
            {
                //if( NodeA.Add(node) == false)
                //{
                //    return NodeB.Add(node); 
                //}
                //throw new Exception("The node is full.");
                bool result = NodeA.Add(node);
                if (result) return true;
                result = NodeB.Add(node);
                if (result) return true;
                return false;
            }
           
        }
        
        public Node Find(SearchParameters parameters)
        {
            //Clear Search Status first

            SearchParameters Parameters = new SearchParameters();
            Parameters.Action = parameters.Action.ToLower().Trim();
            Parameters.InquiryType = parameters.InquiryType.ToLower().Trim();
            Parameters.Location = parameters.Location.ToLower().Trim();
            Parameters.Subject = parameters.Subject.ToLower().Trim();
            Parameters.QuestionKey = parameters.QuestionKey.ToLower().Trim();

            bool L1 = false; bool L2 = false; bool L3 = false; bool L4 = false; bool L5 = false; bool L6 = false; bool L7= false;
            L1 = InquiryType.Contains(Parameters.InquiryType) && 
                   Action.Contains(Parameters.Action) 
                   && QuestionKey.Contains(Parameters.QuestionKey) && Subject.Contains(Parameters.Subject);

            L2 = (InquiryType.Contains(Parameters.InquiryType) &&
                 Action.Contains(Parameters.Action)
                 && Subject.Contains(Parameters.Subject)) && (QuestionKey.Contains(Parameters.QuestionKey) || Location.Contains(Parameters.Location));

            L3 = (QuestionKey.Contains(Parameters.QuestionKey) &&
                 Action.Contains(Parameters.Action)
                 && Subject.Contains(Parameters.Subject)) && Location.Contains(Parameters.Location);

            L4 = (QuestionKey.Contains(Parameters.QuestionKey) &&
               Action.Contains(Parameters.Action)
               && Subject.Contains(Parameters.Subject)) && InquiryType.Contains(Parameters.InquiryType);

            L5 = (QuestionKey.Contains(Parameters.QuestionKey) &&
               Action.Contains(Parameters.Action)
               && Subject.Contains(Parameters.Subject));

            L6 = (QuestionKey.Contains(Parameters.QuestionKey) &&
               Action.Contains(Parameters.Action)
               && Subject.Contains(Parameters.Subject)) && InquiryType.Contains(Parameters.InquiryType) 
               && Location.Contains(Parameters.Location);

            L7 = (InquiryType.Contains(Parameters.InquiryType) &&
                    Action.Contains(Parameters.Action)
                    && Subject.Contains(Parameters.Subject));

       
            if (L1) {
                IsSearchSuccess = true; return this; }
                else if (L2) {
                IsSearchSuccess = true; return this; }
                else if (L3) {
                IsSearchSuccess = true; return this; }
                else if (L4) {
                IsSearchSuccess = true; return this; }
                else if (L5) {
                IsSearchSuccess = true; return this; }
                else if (L6) {
                IsSearchSuccess = true; return this; }

            //check NodeA
            if (NodeA != null)
            {
                L1 = (NodeA.InquiryType.Contains(Parameters.InquiryType) &&
                       NodeA.Action.Contains(Parameters.Action)
                       && NodeA.QuestionKey.Contains(Parameters.QuestionKey)) && NodeA.Subject.Contains(Parameters.Subject);

                L2 = (NodeA.InquiryType.Contains(Parameters.InquiryType) &&
                     NodeA.Action.Contains(Parameters.Action)
                     && NodeA.Subject.Contains(Parameters.Subject)) && (NodeA.QuestionKey.Contains(Parameters.QuestionKey) || NodeA.Location.Contains(Parameters.Location));

                L3 = (NodeA.QuestionKey.Contains(Parameters.QuestionKey) &&
                  NodeA.Action.Contains(Parameters.Action)
                  && NodeA.Subject.Contains(Parameters.Subject)) && NodeA.Location.Contains(Parameters.Location);

                L4 = (NodeA.QuestionKey.Contains(Parameters.QuestionKey) &&
                   NodeA.Action.Contains(Parameters.Action)
                   && NodeA.Subject.Contains(Parameters.Subject)) && NodeA.InquiryType.Contains(Parameters.InquiryType);

                L5 = (NodeA.QuestionKey.Contains(Parameters.QuestionKey) &&
                   NodeA.Action.Contains(Parameters.Action)
                   && NodeA.Subject.Contains(Parameters.Subject));


                L6 = (NodeA.QuestionKey.Contains(Parameters.QuestionKey) &&
                   NodeA.Action.Contains(Parameters.Action)
                   && NodeA.Subject.Contains(Parameters.Subject)) && NodeA.InquiryType.Contains(Parameters.InquiryType)
                   && NodeA.Location.Contains(Parameters.Location);

                L7 = (NodeA.InquiryType.Contains(Parameters.InquiryType) &&
                    NodeA.Action.Contains(Parameters.Action)
                    && NodeA.Subject.Contains(Parameters.Subject));

        
                if (L1) {
                    NodeA.IsSearchSuccess = true; return NodeA; }
                else if (L2) {
                    NodeA.IsSearchSuccess = true; return NodeA; }
                else if (L3) {
                    NodeA.IsSearchSuccess = true; return NodeA; }
                else if (L4) {
                    NodeA.IsSearchSuccess = true; return NodeA; }
                else if (L5) {
                    NodeA.IsSearchSuccess = true; return NodeA; }
                else if (L6) {
                    NodeA.IsSearchSuccess = true; return NodeA; }
                else if (L7) {
                    NodeA.IsSearchSuccess = true; return NodeA; }
                
                //Check child of NodeA
                Node n = NodeA.Find(Parameters); if (n != null) return n;

            }




            if (NodeB != null)
            {
                L1 = (NodeB.InquiryType.Contains(Parameters.InquiryType) &&
                       NodeB.Action.Contains(Parameters.Action)
                       && NodeB.QuestionKey.Contains(Parameters.QuestionKey)) && NodeB.Subject.Contains(Parameters.Subject);

                L2 = (NodeB.InquiryType.Contains(Parameters.InquiryType) &&
                     NodeB.Action.Contains(Parameters.Action)
                     && NodeB.Subject.Contains(Parameters.Subject)) && (NodeB.QuestionKey.Contains(Parameters.QuestionKey) || NodeB.Location.Contains(Parameters.Location));

                L3 = (NodeB.QuestionKey.Contains(Parameters.QuestionKey) &&
                  NodeB.Action.Contains(Parameters.Action)
                  && NodeB.Subject.Contains(Parameters.Subject)) && NodeB.Location.Contains(Parameters.Location);

                L4 = (NodeB.QuestionKey.Contains(Parameters.QuestionKey) &&
                   NodeB.Action.Contains(Parameters.Action)
                   && NodeB.Subject.Contains(Parameters.Subject)) && NodeB.InquiryType.Contains(Parameters.InquiryType);

                L5 = (NodeB.QuestionKey.Contains(Parameters.QuestionKey) &&
                   NodeB.Action.Contains(Parameters.Action)
                   && NodeB.Subject.Contains(Parameters.Subject));

                L6 = (NodeB.QuestionKey.Contains(Parameters.QuestionKey) &&
                   NodeB.Action.Contains(Parameters.Action)
                   && NodeB.Subject.Contains(Parameters.Subject)) && NodeB.InquiryType.Contains(Parameters.InquiryType)
                   && NodeB.Location.Contains(Parameters.Location);

                L7 = (NodeB.InquiryType.Contains(Parameters.InquiryType) &&
                   NodeB.Action.Contains(Parameters.Action)
                   && NodeB.Subject.Contains(Parameters.Subject));

             

                if (L1) {
                    NodeB.IsSearchSuccess = true; return NodeB; }
                else if (L2) {
                    NodeB.IsSearchSuccess = true; return NodeB; }
                else if (L3) {
                    NodeB.IsSearchSuccess = true; return NodeB; }
                else if (L4) {
                    NodeB.IsSearchSuccess = true; return NodeB; }
                else if (L5) {
                    NodeB.IsSearchSuccess = true; return NodeB; }
                else if (L6) {
                    NodeB.IsSearchSuccess = true; return NodeB; }
                else if (L7) {
                    NodeB.IsSearchSuccess = true; return NodeB;
                }

                Node n = NodeB.Find(Parameters); if (n != null) return n;



                }
            return null;
        }
           

     

    }
    [Serializable()]
    public class SearchParameters
    {
        public string InquiryType="";
        public string Location = "";    
        public string Action = "";
        public string Subject = "";
        public string QuestionKey = "";
    }
}
