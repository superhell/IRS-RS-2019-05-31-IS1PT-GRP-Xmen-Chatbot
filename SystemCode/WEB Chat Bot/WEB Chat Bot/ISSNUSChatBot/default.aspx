<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Sys.Application.ChatBot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta name="viewport" content="width=device-width, initial-scale=1">

  
    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap/3.3.7/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="~/Content/bootstrap/3.3.7/css/bootstrap-theme.css" />

    <link  rel="stylesheet" type="text/css" href="~/Content/site.css" /> 
    <link  rel="stylesheet" type="text/css" href="~/Content/chatbot/css/chat.css" />
    <link  rel="stylesheet" type="text/css" href="~/Content/chatbot/css/footer.css" />
    <a href="Content/bootstrap/3.3.7/fonts/glyphicons-halflings-regular.ttf">Content/bootstrap/3.3.7/fonts/glyphicons-halflings-regular.ttf</a>

</head>
<body>
    <form id="form1" runat="server">

 <nav class="navbar  navbar-inverse navbar-fixed-top">
   <div class="container-fluid">
   <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">ISS NUS Chat <span class="glyphicon glyphicon-user" aria-hidden="true"></span></a>
            </div>
    
 
 
 </div>
</nav>
       <main class="Site-content">


 

<div class="container clearfix">
  
    <div class="chat">   
      <div class="chat-history">
        
        <div id="c1">
        <ul class="chat-ul">
          <li>
            <div class="message-data">
              <span class="message-data-name"><i class="fa fa-circle you"></i> You</span>
            </div>
            <div class="message you-message">
                What programmes are available in ISS NUS?
            </div>
          </li>
          <li class="clearfix">
            <div class="message-data align-right">
              <span class="message-data-name">Ada, your ISS NUS customer service</span> <i class="fa fa-circle me"></i>
            </div>
            <div class="message me-message float-right">We have courses in Executive Education, Graduate Programmes, and Centres Of Excellence</div>
          </li>
         </ul>
         </div>

         <div id="c2">
         <ul class="chat-ul">
            <li class="clearfix">
            <div class="message-data">
              <span class="message-data-name"><i class="fa fa-circle you"></i> You</span>
            </div>
            <div class="message you-message">
            What?! No way, how did I miss that. I never forgot that part before.

            </div>
          </li>
          <li class="clearfix">
            <div class="message-data align-right">
              <span class="message-data-name">Ada, your OperationsAlly</span> <i class="fa fa-circle me"></i>
            </div>
            <div class="message me-message float-right">Remembering everything can quickly become impossible as your business grows, we need to take a look at your reminder management system and also see if there are steps in your business we can automate.</div>
          </li>
         </ul>
         </div>

          <div id="c3">
          <ul class="chat-ul">
            <li>
            <div class="message-data">
              <span class="message-data-name"><i class="fa fa-circle you"></i> You</span>
            </div>
            <div class="message you-message">
            6? Bob told me 8! How did this mix up happen?!
            </div>
          </li>
          <li class="clearfix">
            <div class="message-data align-right">
              <span class="message-data-name">Ada, your OperationsAlly</span> <i class="fa fa-circle me"></i>
            </div>
            <div class="message me-message float-right">
The more people in your business, the more opportunity for mistakes, having a solid system in place for tracking important client data will help avoid these miscommunications.            </div>
          </li>
         </ul>
         </div>
         
          <div id="c4">
           <ul class="chat-ul">
          <li>
            <div class="message-data">
              <span class="message-data-name"><i class="fa fa-circle you"></i> You</span>
            </div>
            <div class="message you-message">
            I know that I spoke with Mary about this, but where did I put that note...hopefully she also sent me an email...

            </div>
          </li>

          <li class="clearfix">
            <div class="message-data align-right">
              <span class="message-data-name">Ada, your OperationsAlly</span> <i class="fa fa-circle me"></i>
            </div>
            <div class="message me-message float-right">Finding the right information when you need it will save you time and energy. Your data management systems need to grow with your business. All businesses need a dynamic data strategy and a system to ensure that the strategy is implemented correctly.</div>
          </li>
         </ul>
         </div>
        
      </div> <!-- end chat-history -->
      
    </div> <!-- end chat -->
 
  </div>

</main>

<div class="stickyFooter" >
 
     <div class="input-group input-group-lg">
   <input type="text" class="form-control" placeholder="Type here..." />
      <span class="input-group-btn">
        <button class="btn btn-default" type="button" runat="server">Send</button>
      </span>
    </div>
</div>
    </form>
</body>


<script src="~/Content/jquery/1.11/js/jquery-1.11.min.js"></script>
<script src="~/Content/bootstrap/3.3.7/js/bootstrap.min.js"></script>
 
</html>
