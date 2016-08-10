<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BreadCrumbs.ascx.cs" Inherits="ReferLocals.UserControl.BreadCrumbs" %>
<ul class="page-breadcrumb">
    <li><a href="/Index" class="active">Home</a> <i class="fa fa-angle-double-right"></i></li>
    <%if (Request.Url.AbsoluteUri.ToLower().Contains("/user/referrals")||Request.Url.AbsoluteUri.ToLower().Contains("/pro/referrals"))
        {%>
    <li>Referrals</li>
    <%}
      
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/favorites"))
        {%>
    <li>Favorites</li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/followers"))
        {%>
    <li>Followers</li>
    <%}
         else if (Request.Url.AbsoluteUri.ToLower().Contains("/inbox"))
        {%>
    <li>Inbox</li>
    <%}
         else if (Request.Url.AbsoluteUri.ToLower().Contains("/messages"))
        {%>
    <li>Chat Messages</li>
    <%}  else if (Request.Url.AbsoluteUri.ToLower().Contains("/jobinbox"))
        {%>
    <li>Job Messages</li>
    <%} 
        
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/referprofessional/search") && !Request.Url.AbsoluteUri.ToLower().Contains("/referprofessional/searchjob"))
        {%>
    <li>Search Professional</li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/searchresult?type="))
        {%>
    <li><a href="/referprofessional/Search" class="active">Search Professional</a><i class="fa fa-angle-double-right"></i></li>
    <li><%=Request.QueryString["type"] %></li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("referdetail"))
        {%>
    <li><%=BreadCrumbName %></li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("addreferal") || Request.Url.AbsoluteUri.ToLower().Contains("referme"))
        {%>
    <li>Referral</li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/referprofessional/refer"))
        {%>
    <li>Refer Professional</li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/searchreferprofessional/"))
        {%>
    <li><a href="/referprofessional/Refer" class="active">Refer Professional</a><i class="fa fa-angle-double-right"></i></li>
    <li>
        <%=Request.Url.AbsoluteUri.Substring(Request.Url.AbsoluteUri.LastIndexOf("/")+1) %>
    </li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/searchresult?email"))
        {%>
    <li>Search Result</li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/addnewjob"))
        {%>
    <li>Post Job</li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/referprofessional/searchjob"))
        {%>
    <li>Job Board</li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/jobboard?type="))
        {%>
    <li><a href="/referprofessional/searchjob" class="active">Job Board</a><i class="fa fa-angle-double-right"></i></li>
    <li><%=Request.QueryString["type"] %> Jobs</li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/jobdetail/"))
        { %>
    <li><%=BreadCrumbName%></li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/user_dashboard") || Request.Url.AbsoluteUri.ToLower().Contains("/professional_dashboard"))
        { %>
    <li>My Dashboard</li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("/user_dashboard") || (Request.Url.AbsoluteUri.ToLower().Contains("/profile")&&!Request.Url.AbsoluteUri.ToLower().Contains("/profile/")))
        { %>
    <li>My Account</li>
    <%} else if (Request.Url.AbsoluteUri.ToLower().Contains("/user/profile/"))
        { %>
    <li><%=BreadCrumbName%></li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("changepassword"))
        { %>
    <li>Change Password</li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("aboutus"))
        { %>
    <li>About Us</li>
    <%}
        else if (Request.Url.AbsoluteUri.ToLower().Contains("contactus"))
        {%>
    <li>Contact Us</li>
    <%}%>
</ul>

