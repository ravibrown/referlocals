<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true" ValidateRequest="false"  CodeBehind="ForProfessional.aspx.cs" Inherits="ReferLocals.ForProfessional" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
<title>ReferLocal | For Professionals</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta content="width=device-width, initial-scale=1" name="viewport" />
<meta content="" name="description" />
<meta content="" name="author" />
<link rel="shortcut icon" href="favicon.ico" />

</head>
<body class="">
<div class="container">
  <div class="row">
   <%-- <div class="col-md-12 coming-soon-header"> <a class="brand" href="/index"> <img src="/images/logo.png" alt="logo" /> </a>
      <div class="updownnav">
        <ul class="up1_downnav1">
        
          <li><a href="/login" class="ancher_udown"> Login </a></li>
          <li><a href="/login#register" class="ancher_udown"> Register </a></li>
        </ul>
      </div>
    </div>--%>
      <header id="navigation" class="navbar-fixed-top navbar SearchNavbar">
                <div class="container">
                    <div class="navbar-header">
                        <!-- responsive nav button -->
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse"><span class="sr-only">Toggle navigation</span> <i class="fa fa-bars fa-2x"></i></button>
                        <!-- /responsive nav button -->
                        <!-- logo -->
                        <a class="navbar-brand logobrand" href="/Index">
                            <img src="/images/blue_logo.png" alt="RefereLocals" />
                        </a>
                        <!-- /logo -->
                    </div>
                    <!-- main nav -->
                    <nav class="collapse navbar-collapse navbar-right navchange" role="navigation">
                        <ul id="nav" class="nav navbar-nav navchangeclr">
                            <li class="current"><a href="/index">HOME</a></li>
                            <li><a href="/referprofessional/search">LOCAL PROFESSIONAL </a></li>
                            <li><a href="/referprofessional/searchjob">LOCAL JOBS</a></li>
                            <li><a href="http://blog.referlocals.com/" target="_blank">BLOG</a></li>
                            <li><a id="aForProfessional" runat="server" clientidmode="Static">FOR PROFESSIONAL</a></li>

                        </ul>
                        <%if (!DataAccess.HelperClasses.SessionService.HasKey("UserId"))
                            { %>
                        <div class="log-b"><a href="/Login" class="loginbtn">Login </a><span>I </span><a href="/Login" class="loginbtn">Register </a></div>
                        <%}
                            else
                            { %>
                        <div class="profile_b">
                            <ul>
                                <li class="dropdown dropdown-user">
                                    <a data-close-others="true" data-hover="dropdown" data-toggle="dropdown" class="dropdown-toggle" href="javascript:;">
                                        <img src="<%=DataAccess.HelperClasses.Common.UserImage==""||DataAccess.HelperClasses.Common.UserImage==null?DataAccess.HelperClasses.Common.NoImageIcon+"?width=30&height=30":DataAccess.HelperClasses.Common.UserImagesPath+DataAccess.HelperClasses.Common.UserImage+"?width=30&height=30"%>" class="img-circle" alt="UserImage" />
                                        <span class="username username-hide-on-mobile"><%=DataAccess.HelperClasses.Common.UserName%> </span>
                                        <i class="fa fa-angle-down"></i>
                                    </a>
                                    <ul id="ulLoginMenu" class="dropdown-menu dropdown-menu-default">
                                        <li><a href="/user_dashboard" style="color: #333333 !important;">My Dashboard </a></li>
                                        <li><a href="/Profile" style="color: #333333 !important;">My Account </a></li>
                                        <li><a href="/Favorites" style="color: #333333 !important;">My Network </a></li>
                                        <li><a href="/inbox" style="color: #333333 !important;">Messages </a></li>
                                        <li class="divider"></li>
                                        <li><a onclick="Logout();" style="cursor: pointer; color: #333333 !important;">Log Out</a> </li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                        <%} %>
                    </nav>
                    <!-- /main nav -->
                </div>
            </header>
  </div>
  <div class="row" style="margin-top:50px;">
    <div class="col-md-6 coming-soon-content homeprofessionals">
      <h2 class="homeh2">Leverage your Referrals right</h2>
      <p class="hope_p">Let People see who referred you</p>
<p class="hope_p">Send Estimates, get hired and grow your business</p>
<p class="hope_p">Manage your clients and stay in touch with them</p>
<p class="hope_p">Network with other Professionals to grow your business</p>
      <br>
     <div class="ancherhome"> <a href="/login#register"> Register Now </a> </div>
     
     <div class="d_home_app">
     <a href="#" class="a_appHome"><img src="/images/apple-store-icon.png" alt="">   </a>
     <a href="#" class="a_appHome"><img src="/images/google-play-icon.png" alt="">   </a>
     
     
     </div>
     
     
      
     
    </div>
    <div class="col-md-6 coming-soon-countdown">
      <div id="defaultCountdown"> </div>
    </div>
  </div>
</div>


<div class="ceta_list_home">
<div class="container">
<div class="list_name_text">
<h2 class="all_service"> Serving following Professionals and more coming soon </h2>
<p class="p_detailceta"> We are currently serving Individual service Professionals from following categories. As a Individual you can Register and get started with our Free App. Corporate Accounts to manage multiple Employees coming soon.</p> 

<div class="iconDetail">
    <asp:Repeater ID="rptSubCategory" runat="server">
        <ItemTemplate><div class="col-md-2"> <a title="<%#Eval("Name") %>" href="/SearchResult?type=<%#Eval("Name")%>" class="a_ceta_detail"> <%#Eval("Name").ToString().Length>12?Eval("Name").ToString().Substring(0,12)+"..":Eval("Name").ToString() %></a> </div></ItemTemplate>
    </asp:Repeater>

<%--<div class="col-md-2"> <a href="#" class="a_ceta_detail"> Handyman </a> </div>
<div class="col-md-2"> <a href="#" class="a_ceta_detail"> Landscaper </a> </div>
<div class="col-md-2"> <a href="#" class="a_ceta_detail"> Maid Service </a> </div>
<div class="col-md-2"> <a href="#" class="a_ceta_detail"> Home Painter </a> </div>
<div class="col-md-2"> <a href="#" class="a_ceta_detail"> Real Estate Agent </a> </div>
<div class="col-md-2"> <a href="#" class="a_ceta_detail"> Loan Officer </a> </div>
<div class="col-md-2"> <a href="#" class="a_ceta_detail"> Movers </a> </div>
<div class="col-md-2"> <a href="#" class="a_ceta_detail"> Home Inspector </a> </div>
<div class="col-md-2"> <a href="#" class="a_ceta_detail"> Baby Sitter </a> </div>
<div class="col-md-2"> <a href="#" class="a_ceta_detail"> Nanny </a> </div>
<div class="col-md-2"> <a href="#" class="a_ceta_detail"> Personal Trainer </a> </div>
<div class="col-md-offset-5 col-md-2"> <a href="#" class="a_ceta_detail"> Dog Groomer </a> </div>
<div class="moreview"> <a href="#"> More </a>  </div>--%>
</div>

</div>
</div>
</div>



<div class="secound_partDiv secound_partDiv_clr " id="a2" >
  <div class="container3">
    <div class="main_c_soon" style="background: rgb(255, 255, 255) none repeat scroll 0% 0%; border-top: 1px solid rgb(222, 222, 221);">
      <h1 class="c_soon_h1"> For Professionals </h1>
      <h4 class="c_soon_h4"> Aiming to become your "The App" or "Must have App </h4>
    </div>
    
    <div class="fill_csoon">
      <div class="col-md-6">
        <div class="text_secound_text">
          <h2 class="post_c1"> Showcase Referrals to build local visibility </h2>
          <ul class="textUl">
            <li> Invite your current Clients to Refer you </li>
            <li> When someone search for a local professionals they can see that their neighbors have referred you </li>
            <li> Expect lot of improvements with future updates around "trust factor" when getting in front of new prospective clients </li>
            <li> Your Clients can Refer and share your profile via Text Message, Email, Whatsapp, Social media and more right thru the App </li>
            <li> Add Referral URL in your signature to increase your exposure </li>
          </ul>
        </div>
      </div>
      <div class="col-md-6">
        <div class="text_secound_img"> <img class="post_iphone responsive_post_img1" alt="#" src="/images/191.png"> </div>
      </div>
    </div>
    
    <div class="fill_csoon text_secound_img_phone">
      <div class="col-md-6">
        <div class="text_secound_img"> <img class="post_iphone responsive_post_img3" alt="#" src="/images/jobboard.png"> </div>
      </div>
      <div class="col-md-6">
        <div class="text_secound_text">
          <h2 class="post_c1"> Job Board </h2>
          <p class="c_p_soon">We have heard stories that you are paying hefty monthly fees, spending money to send estimates and in some cases purchasing zipcodes( not that you own that exclusively). We are here to change that </p>
          <ul class="textUl">
            <li>Search for Jobs by Profession ( Save the job to view later)</li>
            <li> Send Estimates for FREE</li>
            <li> Set up the Appointments within the App </li>
            <li> Build your Referral network and see the change in your business</li>
          </ul>
        </div>
      </div>
    </div>
    
    <div class="fill_csoon">
      <div class="col-md-6">
        <div class="text_secound_text">
          <h2 class="post_c1"> Send an Estimate </h2>
          <ul class="textUl">
            <li>We are making it supereasy to send an Estimate for the Jobs posted by people living in your service area</li>
            <li> Set up an Appointment within the App</li>
            <li> Keep Track of your Appointments</li>
            <li> Keep Track of your Past Jobs </li>
          </ul>
        </div>
      </div>
      <div class="col-md-6">
        <div class="text_secound_img"> <img class="post_iphone responsive_post_img1" alt="#" src="/images/iphone4.png"> </div>
      </div>
    </div>
    
    <div class="fill_csoon text_secound_img_phone">
      <div class="col-md-6">
        <div class="text_secound_img"> <img src="/images/followers.png" alt="#" class="post_iphone responsive_post_img1"> </div>
      </div>
      <div class="col-md-6">
        <div class="text_secound_text">
          <h2 class="post_c1"> Create Network of Other Professionals </h2>
          <ul class="textUl">
            <li>Grow your business by connecting with other professionals.</li>
            <li> Talk to other professionals on your network via chat within the App regarding new business, existing projects and more</li>
            <li> One of our goal: Make this section so much valuable in coming time by adding features to make your life easy</li>
            <li> Keep Track of everyone at one place </li>
          </ul>
        </div>
      </div>
    </div>
    
    <div class="fill_csoon">
      <div class="col-md-6">
        <div class="text_secound_text">
          <h2 class="post_c1"> Get in touch with your Clients/Messaging </h2>
          <ul class="textUl">
            <li>Talking to your client regarding upcoming appointment, about the job that they posted or even saying happy Bday is awesome </li>
            <li> You can create your secure client list within the App</li>
            <li>When you finish the job , your client will be part of your growing list </li>
          </ul>
        </div>
      </div>
      <div class="col-md-6">
        <div class="text_secound_img"> <img class="post_iphone responsive_post_img1" src="/images/message.png" alt="#"> </div>
      </div>
    </div>
    
  </div>
</div>


<!--<div class="table_home">

<h2 class="h2_border_home"> Pro App Benifits </h2>
<p class="p_border_home"> If you are still debating whether to download our App then let us make that easy the features above are FREE!</p>

<div class="container">
<div class="text_secound_text"> 
  <ul class="textUl">
    <li> All of the Above Features for FREE </li>
    <li> No Monthly or Yearly Fee  </li>
    <li> No Fee to contact your Clients or send estimates </li>
    <li> Awesome product which can let you generate and leverage Referrals </li>
    <li> Search, View the latest jobs and let you send Estimates with pulling your wallet </li>
    <li> Manage your Clients and stay in touch with them </li>
    <li> Network with other professionals to grow your business </li>
  </ul>
</div>
</div>

</div>-->

<div class="main_c_soon" style="background: rgb(255, 255, 255) none repeat scroll 0% 0%; border-top: 1px solid rgb(222, 222, 221);">
      <h1 class="c_soon_h1"> Pro App Benifits </h1>
      <h4 class="c_soon_h4"> If you are still debating whether to download our App then let us make that easy. The features above are FREE!  </h4>
    </div>
    
    <div class="fill_csoon">
      <div class="col-md-6">
        <div class="text_secound_text" style="margin-top: 50px;">
         <!-- <h2 class="post_c1" style="color:#fff;"> Showcase Referrals to build local visibility </h2>-->
          <ul class=" col-md-offset-4 textUl" style="color:#fff;">
            <li> All of the Above Features for FREE</li>
<li>No Monthly or Yearly Fee </li>
<li>No Fee to contact your Clients or send estimates</li>
<li>Awesome product which can let you generate and leverage Referrals</li>
<li>Search, View the latest jobs and let you send Estimates with pulling your wallet</li>
<li>Manage your Clients and stay in touch with them</li>
<li>Network with other professionals to grow your business </li>
          </ul>
        </div>
      </div>
      <div class="col-md-6">
        <div class="text_secound_img"> <img class="post_iphone responsive_post_img1" src="/images/iPhone6s_1.png" alt="#" style="height: 400px;"> </div>
      </div>
      
      
      
      
      
    </div>

<div class="app_detail8">
<div class="container">
<p class="p_app_detail8">  We are committed to improve our Mobile App and Add features which will make sense to you. I am sure you are convinced now! Register today</p>
<div class="z_btn"> <a href="/login#register"   class="a_app_detail8">Register</a> </div>



</div>      
</div>

<div class="z_subscribe">
<div class="container">

<p class="dubscribe_u_text"> If you are still thinking then leave us your email and we will send you some more information in coming weeks which will help you decide( We do not spam) </p>
<form id="form1" runat="server" class="col-md-offset-2 form-inline">
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" 
        runat="server" ErrorMessage="Please enter valid email" SetFocusOnError="true" 
        Text="Please enter valid email" ForeColor="Red" ControlToValidate="txtEmail" ValidateRequestMode="Enabled"
         ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>  
        <asp:RequiredFieldValidator ID="RequiredValidator2" ValidationGroup="a" 
        runat="server" ErrorMessage="Please enter email" SetFocusOnError="true" 
        Text="Please enter email" ForeColor="Red" ControlToValidate="txtEmail" ValidateRequestMode="Enabled"></asp:RequiredFieldValidator>    
    <div style="width:100% !important" class="input-group input-group-lg input-large">
          <asp:TextBox ID="txtEmail" runat="server" type="text" ValidationGroup="a" ValidateRequestMode="Enabled" CausesValidation="true"  placeholder="Enter E-mail" CssClass="form-control"></asp:TextBox>
            
          <span class="input-group-btn">
          <asp:Button ID="btnSubscribe" runat="server" ValidationGroup="a" CausesValidation="true" 
               style="text-transform: capitalize; font-size: 16px;"  CssClass="btn green"
               Text="Subscribe" OnClick="btnSubscribe_Click" />
          </span> </div>
      </form>


</div>
</div>


<div class="footer_black"> <div class="col-md-12 coming-soon-footer"> 2016 © Refer Locals, All Right Reserved. </div> </div>
<div class="backstretch" style="left: 0px; top: 0px; overflow: hidden; margin: 0px; padding: 0px; height: 975px; width: 1903px; z-index: -999999; position: fixed;"><img style="position: absolute; margin: 0px; padding: 0px; border: medium none; width: 1903px; height: 1427.76px; max-height: none; max-width: none; z-index: -999999; left: 0px; top: -426.378px;" src="/images/ForProfessionals.png" alt=""></div>

<!-- BEGIN GLOBAL MANDATORY STYLES -->
<link rel="stylesheet" href="/css/main.css">
<link rel="stylesheet" href="/css/media-queries.css">
<link href="//fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
<link href="//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.css" rel="stylesheet">
<link href="//cdnjs.cloudflare.com/ajax/libs/simple-line-icons/2.2.3/css/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css">
<link href="/css/select2.min.css" rel="stylesheet" type="text/css" />
<link href="/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
<link href="/css/uniform.default.css" rel="stylesheet" type="text/css" />
<link href="/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
<!-- END GLOBAL MANDATORY STYLES --> 
<!-- BEGIN PAGE LEVEL PLUGINS --> 
<!-- END PAGE LEVEL PLUGINS --> 
<!-- BEGIN THEME GLOBAL STYLES -->
<link href="/css/all.css" rel="stylesheet" type="text/css" />
<link href="/css/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
<link href="/css/plugins-md.min.css" rel="stylesheet" type="text/css" />
<!-- END THEME GLOBAL STYLES --> 
<!-- BEGIN PAGE LEVEL STYLES -->
<link href="/css/coming-soon.min.css" rel="stylesheet" type="text/css" />
<!-- END PAGE LEVEL STYLES --> 
<!-- BEGIN THEME LAYOUT STYLES --> 
<!-- END THEME LAYOUT STYLES --> 

<!-- BEGIN CORE PLUGINS --> 
<script src="/js/jquery.min.js" type="text/javascript"></script> 
<script src="/js/bootstrap.min.js" type="text/javascript"></script> 
<script src="/js/js.cookie.min.js" type="text/javascript"></script> 
<script src="/js/bootstrap-hover-dropdown.min.js" type="text/javascript"></script> 
<script src="/js/jquery.slimscroll.min.js" type="text/javascript"></script> 
<script src="/js/jquery.blockui.min.js" type="text/javascript"></script> 
<script src="/js/jquery.uniform.min.js" type="text/javascript"></script> 
<script src="/js/bootstrap-switch.min.js" type="text/javascript"></script> 
<!-- END CORE PLUGINS --> 
<!-- BEGIN PAGE LEVEL PLUGINS --> 
<script src="/js/jquery.countdown.min.js" type="text/javascript"></script> 
<script src="/js/jquery.backstretch.min.js" type="text/javascript"></script> 
<!-- END PAGE LEVEL PLUGINS --> 
<!-- BEGIN THEME GLOBAL SCRIPTS --> 
<script src="/js/app.min.js" type="text/javascript"></script> 
<script src="/js/form-icheck.min.js" type="text/javascript"></script> 
<!-- END THEME GLOBAL SCRIPTS --> 
<!-- BEGIN PAGE LEVEL SCRIPTS --> 
<!--<script src="/js/coming-soon.min.js" type="text/javascript"></script> -->
<!-- END PAGE LEVEL SCRIPTS --> 
<!-- BEGIN THEME LAYOUT SCRIPTS --> 
<!-- END THEME LAYOUT SCRIPTS --> 

<script>
$(document).on('click','.ancher_udown', function(event) {
    event.preventDefault();
    var target = "#" + this.getAttribute('data-target');
    $('html, body').animate({
        scrollTop: $(target).offset().top
    }, 2000);
});
</script>
</body>
</html>
