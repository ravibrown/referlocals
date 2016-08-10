<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="aboutus.aspx.cs" Inherits="ReferLocals.aboutus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/about.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_aboutUs">

<div class="row margin-bottom-40 about-header about-headerImg">
  <div class="col-md-12">
    <h1>About Us</h1>
    <h2>Life is either a great adventure or nothing</h2>
    <a href="/login#register"  class="btn btn-danger">JOIN US TODAY</a>
  </div>
</div>

 

<div class="row margin-bottom-40 paddingnone">
  <div class="col-lg-6">
    <div class="portlet light about-text aboutSize">
      <h4> <i class="fa fa-check icon-info"></i> About ReferLocals</h4>
      <p class="margin-top-20"> ReferLocals is based in Dallas area and our goal is extremely simple: Connecting you with Local Professionals who are referred by your Friends, Neighbors and people living close to you. <br/>
        <br/>
        Every time we need a Plumber, Handyman, Electrician, Landscaping professional, Babysitter, Real Estate Agent, Loan officer, Dog grooming services, Maid Services or any other Service or professional we ask our Friends, Family , Neighbors. At Referlocals we want to give you access to all of this thru our simple, user friendly App which will be available soon. We are about to shake things a bit yet keep it real simple for everyone to use and share our product. We are officially launching our iOS App and Full Website on July 1, 2016. </p>
    </div>
    
    <div class="about_social_icon">
      <h2 class="follow_us_about"> Follow us on </h2>
      <ul class="icon_social_about">
        <li> <a href="//twitter.com/referlocals" class="tewwt_about" target="_blank"> <i class="fa fa-twitter"></i> </a> </li>
        <li> <a href="//facebook.com/referlocals" class="facebook_about" target="_blank"> <i class="fa fa-facebook"></i> </a> </li>
        <li> <a href="//plus.google.com/b/104861721798938267548/104861721798938267548" target="_blank" class="google_about"> <i class="fa fa-google-plus"></i> </a> </li>
        <li> <a href="//instagram.com/referlocals" class="insta_kabout" target="_blank"> <i class="fa fa-instagram"></i> </a> </li>
          
      </ul>
    </div>
  </div>
  
  
  <div class="col-lg-6"> <img src="/images/img2.png" alt="#" class="about_side_img"> </div>
</div>

</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
