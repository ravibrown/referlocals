<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="ReferLocals.ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #twitter-widget-0 {
            position: relative !important;
            opacity: 0;
            z-index: 150;
        }

        .xl .btn, btn-o, widget {
            width: 100%;
            height: 57px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--==================================== Start Search Result Mid HTML ====================================-->

    <div class="contact">


        <%--<div class="c-content-contact-1 c-opt-1">
            <div class="row" data-auto-height=".c-height">
                <div class="col-lg-8 col-md-6 c-desktop"></div>
                <div class="col-lg-4 col-md-6">
                    <div class="c-body">
                        <div class="c-section">
                            <h3>REFER LOCALS.</h3>
                        </div>
                        <div class="c-section">
                            <div class="c-content-label uppercase bg-blue">Address</div>
                            <p>
                                1141 Noble Ave,<br />
                                Lantana,<br />
                                TX-76226
                            </p>
                        </div>
                        <div class="c-section">
                            <div class="c-content-label uppercase bg-blue">Contacts</div>
                            <p>
                                <strong>T</strong> 800 123 0000
                                <br />
                                <strong>F</strong> 800 123 8888
                            </p>
                        </div>
                        <div class="c-section">
                            <div class="c-content-label uppercase bg-blue">Social</div>
                            <br />
                            <ul class="c-content-iconlist-1 ">
                                <li><a href="//twitter.com/referlocals" target="_blank"><i class="fa fa-twitter"></i></a></li>
                                <li><a href="//facebook.com/referlocals" target="_blank"><i class="fa fa-facebook"></i></a></li>
                                <li><a target="_blank" href="//plus.google.com/b/104861721798938267548/104861721798938267548"><i class="fa fa-google-plus"></i></a></li>
                                <li><a href="//instagram.com/referlocals" target="_blank"><i class="fa fa-instagram"></i></a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div id="gmapbg" class="c-content-contact-1-gmap" style="height: 615px;"></div>
        </div>--%>

        <div class="container">
            <div class="col-md-12 quotes_h1" style="clear:both;overflow:hidden;">
                <h1>Contact Us </h1>
            </div>
            <div class="c-content-feedback-1 c-option-1" style="margin-bottom: 20px;">
                <div class="row">
                    <div class="col-md-6">
                        <div class="c-container bg-green">
                            <div class="c-content-title-1 c-inverse">
                                <h3 class="uppercase">Need to know more?</h3>
                                <div class="c-line-left"></div>
                                <p class="c-font-lowercase">Try visiting our About Us page to learn more about our greatest ever website, Referlocals.</p>
                                <a href="/aboutus" class="btn grey-cararra font-dark">About Us</a>
                            </div>
                        </div>
                        <div class="c-container bg-grey-steel">
                            <div class="c-content-title-1">
                                <h3 class="uppercase">Have a question?</h3>
                                <div class="c-line-left bg-dark"></div>
                                <!--<form action="#">
                <div class="input-group input-group-lg c-square">
                  <input type="text" class="form-control c-square" placeholder="Ask a question" />
                  <span class="input-group-btn">
                  <button class="btn uppercase" type="button">Go!</button>
                  </span> </div>
              </form>-->
                                <p>Have any questions ?  Let our dedicated customer service help you look through our FAQs to get your questions answered!</p>
                                <a href="/faq" class="btn grey-cararra font-dark" style="margin: 30px 0 0 0">FAQ</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="c-contact">
                            <div class="c-content-title-1">
                                <h3 class="uppercase">Keep in touch</h3>
                                <div class="c-line-left bg-dark"></div>
                                <p class="c-font-lowercase">Our helpline is always open to receive any inquiry or feedback. Please feel free to drop us an email from the form below and we will get back to you as soon as we can.</p>
                            </div>

                            <div class="form-group">
                                <input type="text" id="txtName" runat="server" clientidmode="static" placeholder="Your Name" class="form-control input-md">
                            </div>
                            <div class="form-group">
                                <input type="text" id="txtEmail" runat="server" clientidmode="static" placeholder="Your Email" class="form-control input-md">
                            </div>
                            <div class="form-group">
                                <input type="text" id="txtPhone" runat="server" clientidmode="static" placeholder="Contact Phone" class="form-control input-md">
                            </div>
                            <div class="form-group">
                                <textarea rows="8" name="message" id="txtMessage" clientidmode="static" runat="server" placeholder="Write comment here ..." class="form-control input-md"></textarea>
                            </div>
                            <asp:Button ID="btnSubmit" runat="server" type="submit" OnClientClick="return ValidateEmailAndName();" class="btn grey" Text="Submit" OnClick="btnSubmit_Click" />
                            <asp:Label ID="lbMsg" ForeColor="Red" runat="server"></asp:Label>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--==================================== End Search Result Mid HTML ====================================-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <%--<script src="http://maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script> --%>
    <%--<script src="/js/gmaps.min.js" type="text/javascript"></script>--%>
    <link href="/css/contact.min.css" rel="stylesheet" type="text/css" />
    <%--<script src="/js/contact.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        function ValidateEmailAndName() {
            var ErrorMessage = "";
            var name = $('#txtName').val();
            var email = $('#txtEmail').val();
            var msg = $('#txtMessage').val();

            if (name == "") {

                ErrorMessage += "Please enter  name" + "\r\n";

                $('#txtName').css("border-color", "red");
            }
            else {
                $('#txtName').css("border-color", "#c2cad8");
            }

            if (email == "") {

                ErrorMessage += "Please enter email" + "\r\n";

                $('#txtEmail').css("border-color", "red");
            }
            else {
                $('#txtEmail').css("border-color", "#c2cad8");
            }

            if (msg == "") {

                ErrorMessage += "Please enter message";

                $('#txtMessage').css("border-color", "red");
            }
            else {
                $('#txtMessage').css("border-color", "#c2cad8");
            }
            if (ErrorMessage != "") {
                alert(ErrorMessage);
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</asp:Content>
