
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="comingsoon.aspx.cs" EnableEventValidation="true"  Inherits="ReferLocals.comingsoon" %>

<%@ Register Src="~/UserControl/TryOurBeta.ascx" TagPrefix="uc1" TagName="TryOurBeta" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
<title>ReferLocals | Coming Soon</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta content="width=device-width, initial-scale=1" name="viewport" />
<meta content="" name="description" />
<meta content="" name="author" />
<link rel="shortcut icon" href="favicon.ico" />
      <!-- BEGIN GLOBAL MANDATORY STYLES -->
<link rel="stylesheet" href="/css/main.css">
<link rel="stylesheet" href="/css/media-queries.css">
<link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0//css/font-awesome.css" rel="stylesheet">
<link href="https://cdnjs.cloudflare.com/ajax/libs/simple-line-icons/2.2.3//css/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6//css/bootstrap.min.css">
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
    <script src="//load.sumome.com/" data-sumo-site-id="cee3f27513f11d2f103ef10c4d37d27da6c600f00ff5ecde5733079588b76986" async="async"></script>
</head>
<body>
    <form id="form1" runat="server">
  <div class="container">
  <div class="row">
    <div class="col-md-12 coming-soon-header"> <a class="brand" href="index.html"> <img src="/images/logo.png" alt="logo" /> </a>
      <div class="updownnav">
        <ul class="up1_downnav1">
          <li><a href="#" class="ancher_udown" data-target="a1"> What Is Coming </a></li>
          <li><a href="#" class="ancher_udown" data-target="a2"> For Professionals </a></li>
          <li><a href="#" class="ancher_udown" data-target="a3"> Beta Site </a></li>
        </ul>
      </div>
    </div>
  </div>
  <div class="row">
    <div class="col-md-6 coming-soon-content">
      <h1>Coming Soon!</h1>
      <p> Imagine finding Professionals who were referred by your neighbors, friends at one place. Imagine Posting your Professional needs with few clicks and getting Estimates from these Professionals. Check out our Beta Website while we work on our Mobile App and complete site </p>
      <br>
     
          <uc1:TryOurBeta runat="server" id="TryOurBeta" />
      
      <ul class="social-icons margin-top-20">
        <li> <a target="_blank" href="//facebook.com/referlocals" data-original-title="Facebook" class="facebook"> </a> </li>
        <li> <a target="_blank" href="//twitter.com/referlocals" data-original-title="Twitter" class="twitter"> </a> </li>
        <li> <a target="_blank" href="//plus.google.com/b/104861721798938267548/104861721798938267548" data-original-title="Goole Plus" class="googleplus"> </a> </li>
        <li> <a target="_blank" href="//instagram.com/referlocals" data-original-title="Instagram" class="instagram"> </a> </li>
      </ul>
    </div>
    <div class="col-md-6 coming-soon-countdown">
      <div id="defaultCountdown"> </div>
    </div>
  </div>
</div>


<div class="secound_partDiv" id="a1" >
  <div class="container3">
    <div class="main_c_soon">
      <h1 class="c_soon_h1"> What is Coming </h1>
      <h4 class="c_soon_h4"> What you can do with our App </h4>
    </div>
    <div class="fill_csoon text_secound_img_phone">
      <div class="col-md-6">
        <div class="text_secound_img "> <img src="/images/ppst-a-job.png" alt="#" class="post_iphone responsive_post_img1"> </div>
      </div>
      <div class="col-md-6">
        <div class="text_secound_text">
          <h2 class="post_c1"> Post a Job </h2>          
          <ul class="textUl">
            <li> Where is the Plumber, something is Leaking </li>
            <li> My Home needs some sunshine, find a great maid </li>
            <li> Need to sell my house, I wonder how much is its worth , Find a Local Realtor </li>
            <li> I want to watch The Jungle Book Movie, Find me Best babysitter </li>
          </ul>
          <p class="c_p_soon"> You can Post the Job in Seconds. Get Estimates from Top Professionals. Best Part: Your personal detail will only be shared once you accept the quote. </p>
        </div>
      </div>
    </div>
    
    <div class="fill_csoon">
      <div class="col-md-6">
        <div class="text_secound_text">
          <h2 class="post_c1"> Receive Estimates </h2>
          <ul class="textUl">
            <li> Top Professionals whom we can trust</li>
            <li>They should be able to do the Best job in the world</li>
            <li>Its Best if they were coming as Referral from other</li>
            <li style="font-size:20px; font-weight:bold;">You get Best deal in the town </li>
          </ul>
          <p class="c_p_soon"> Receive Estimates on your Jobs. Accept the Best Quote. Set up appointment right from the App </p >
        </div>
      </div>
      <div class="col-md-6">
        <div class="text_secound_img"> <img src="/images/iphone3.png" alt="#" class="post_iphone"> </div>
      </div>
    </div>
    
    <div class="fill_csoon text_secound_img_phone">
      <div class="col-md-6">
        <div class="text_secound_img"> <img src="/images/iphone-feat.png" alt="#" class="post_iphone"> </div>
      </div>
      <div class="col-md-6">
        <div class="text_secound_text">
          <h2 class="post_c1"> Top Professionals </h2>
          <p class="c_p_soon"> Who doesn't want Top of the Line Professionals </p>
          <ul class="textUl">
            <li> Best part is if those Professionals are Local and serve your neighborhood</li>
            <li>See Referrals from your neighbors, friends and people living close to you that you can trust</li>
            <li> See a List of Top Professional by their trade</li>
            <li style="font-size:20px; font-weight:bold; ">Share your Recommendations with your Friends, and world</li>
          </ul>
        </div>
      </div>
    </div>
    
    <div class="fill_csoon">
      <div class="col-md-6">
        <div class="text_secound_text">
          <h2 class="post_c1"> Refer a Professional </h2>
          <p class="c_p_soon"> When everyone is expecting Referrals from others then I guess you have to start giving back by Referring Professionals. Why?</p>
          <ul class="textUl">
            <li>Do your Friends and neighbors a favor by recommending them the top professionals or Warning them if someone is not good sport 😎</li>
            <li> Helping a Professional who helped you with your Professional needs will be a good Karma</li>
            <li>Create your Referral list and share with world or just friends! ( So that they stop nagging you everytime they need someone)</li>
          </ul>
        </div>
      </div>
      <div class="col-md-6">
        <div class="text_secound_img"> <img class="post_iphone" alt="#" src="/images/form.png"> </div>
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


<div class="secound_partDiv" id="a3" >
  <div class="container3">
    <div class="main_c_soon main_c_soon_bg ">
      <h1 class="c_soon_h1 c_soon_h_h1"> Beta Site </h1>
      <!--<h4 class="c_soon_h4 c_soon_h_h4"> Hello guys, nice to have you on the platform! </h4>-->
      <p class="c_soon_p_text"> Imagine finding Professionals who were referred by your neighbors, friends at one place. Imagine Posting your Professional needs with few clicks and getting Estimates from these Professionals. Check out our Beta Website while we work on our Mobile App and complete site </p>
      
      <div class="ancher_betalink">
           <uc1:TryOurBeta runat="server" id="TryOurBeta1" />
        <%--<div class="input-group input-group-lg input-large input_d_large">
          <input type="text" placeholder="Enter E-mail" class="form-control">
          <span class="input-group-btn" style="text-align: left;">
          <button style="text-transform: capitalize; font-size: 16px;" type="button" class="btn green"> Try our beta site</button>
          </span> </div>
          
        <div class="ccyy" style="margin:20px 0 35px 1px; float:left; width:100%;">
          <div class="container">
            <div class="icheck-list">
              <label style="color:#fff;">
                <input type="checkbox" class="icheck" data-checkbox="icheckbox_flat-grey">
                I am a Professional and would like to know when your App is coming out </label>
            </div>
          </div>
        </div>--%>
      </div>
      
    </div>
  </div>
</div>


<div class="footer_black">
  <div class="col-md-12 coming-soon-footer"> 2016 © Refer Locals, All Right Reserved. </div>
</div>
<div class="backstretch" style="left: 0px; top: 0px; overflow: hidden; margin: 0px; padding: 0px; height: 975px; width: 1903px; z-index: -999999; position: fixed;"><img style="position: absolute; margin: 0px; padding: 0px; border: medium none; width: 1903px; height: 1427.76px; max-height: none; max-width: none; z-index: -999999; left: 0px; top: -426.378px;" src="/images/1.jpg" alt=""></div>

    </form>
  
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
<script src="/js/coming-soon.js" type="text/javascript"></script> 
<!-- END PAGE LEVEL SCRIPTS --> 
<!-- BEGIN THEME LAYOUT SCRIPTS --> 
<!-- END THEME LAYOUT SCRIPTS --> 

<script type="text/javascript">
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
