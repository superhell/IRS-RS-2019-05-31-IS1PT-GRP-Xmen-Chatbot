<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Sys.Application.ChatBot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta name="viewport" content="width=device-width, initial-scale=1">

  
    <link rel="stylesheet" type="text/css" href="Content/bootstrap/3.3.7/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="Content/bootstrap/3.3.7/css/bootstrap-theme.css" />

    <link  rel="stylesheet" type="text/css" href="Content/site.css" /> 
    <link  rel="stylesheet" type="text/css" href="Content/chatbot/css/chat.css" />
    <link  rel="stylesheet" type="text/css" href="Content/chatbot/css/footer.css" />
    <a href="Content/bootstrap/3.3.7/fonts/glyphicons-halflings-regular.ttf">Content/bootstrap/3.3.7/fonts/glyphicons-halflings-regular.ttf</a>
    
    <style>
    .responsive {
    width: 100%;
    height: auto;
    }
    </style>
</head>
<body>
    

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
 
<div id="main" ><!--class="container clearfix" -->
  
    <div class="chat">   
      <div class="chat-history">
        
        <div id="chatlist">
        <ul class="chat-ul">
     
 

         </ul>
         </div>
        
      </div> <!-- end chat-history -->
      
    </div> <!-- end chat -->


</div>
 <br/>
 <br/>
 <br/>
 <br/>
</main>

<div class="stickyFooter" >
 
     <div class="input-group input-group-lg" id="footer">
   <input type="text" class="form-control" placeholder="Type here..." name="Query" id="Query"/>
      <span class="input-group-btn">
        <button   class="btn btn-default" type="button"  onclick="javascript:PostMessage('');" id="Send"  >Send</button>
      </span>
    </div>
</div>

</body>

<script type="text/javascript" src="Content/jquery/1.11/js/jquery-1.11.min.js"></script>
<script type="text/javascript" src="Content/bootstrap/3.3.7/js/bootstrap.min.js"></script>
 
<script>
    var ChatID = 1;
    var IsPageStarted = false;
    $(document).ready(function () {
        if (!IsPageStarted) { PostMessage("startup"); IsPageStarted = true; }
        $("#Query").focus();
         if(!detectmobile) $("main").toggleClass("container clearfix"); //apply centering for non mobile browser
    });

$.fn.scrollView = function () {
    return this.each(function () {
        $('html, body').animate({
            scrollTop: $(this).offset().top
        }, 1000);
    });
}

$("#Query").keydown(function (e) {
    if (e.keyCode == 13) {
       if ($("#Query").val() != "") PostMessage('');
  }
});


function PostMessage(command)
{         
    
    var query = $("#Query").val();
    if (command == "startup") query = "-#welcome#-";
    if (command != "startup" && query != "") ShowUserMessage(query);
   
    if (query != "") {
    $.ajax({
        type: "POST",
        url: "default.aspx",
        data: query,
        dataType: "text",
        success: function (data) {
            //update chat message
            ShowBotMessage(data);
            $("#Query").val("");
            //$('ul.chat-ul').prepend('<li class="clearfix"><div class="message-data align-right"><span class="message-data-name">Ada</span> <i class="fa fa-circle me"></i></div><div class="message me-message float-right">' + data + '</div></li>');
        },
        error: function (xhr, status, errorThrown) {
            //Here the status code can be retrieved like;
            xhr.status;
             alert("1" + errorThrown);
            //The message added to Response object in Controller can be retrieved as following.
            xhr.responseText;
       }
    });
    }

}
    function ShowBotMessage(message) {
        //do not use prepend
        $('ul.chat-ul').append('<li class="clearfix"><div id="chat' + ChatID + '" class="message-data align-right"><span class="message-data-name">Ada</span> <i class="fa fa-circle me"></i></div><div class="message me-message float-right">' + message + '</div></li>');        
        $('#chat' + ChatID).scrollView();  //$(document).scrollTo('#chat' + ChatID);
        ChatID += 1;
    }
     function ShowUserMessage(message) {
        //do not use prepend
         $('ul.chat-ul').append('<li> <div id="chat' + ChatID + '" class="message-data"><span class="message-data-name"><i class="fa fa-circle you"></i>You</span></div><div class="message you-message">' + message + '</div></li>');
         $('#chat' + ChatID).scrollView();  
         ChatID += 1;
    }

 function detectmobile() { 
 if( navigator.userAgent.match(/Android/i)
 || navigator.userAgent.match(/webOS/i)
 || navigator.userAgent.match(/iPhone/i)
 || navigator.userAgent.match(/iPad/i)
 || navigator.userAgent.match(/iPod/i)
 || navigator.userAgent.match(/BlackBerry/i)
 || navigator.userAgent.match(/Windows Phone/i)
 ){
    return true;
  }
 else {
    return false;
  }
}
</script>


 
</html>
