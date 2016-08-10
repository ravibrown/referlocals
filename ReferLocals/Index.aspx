<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ReferLocals.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--==================================== Start Home SliderEnd ====================================-->

    <div class="video-container" style="position: static; z-index: 1;">
        <div class="black_background">
            <div class="carousel-caption c_caption">
                <h3 class="wow slideInLeft animated animated hide_mediasmall">
                    <img src="/images/icon-logo.png" alt="#" />
                </h3>
                <h2 class="wow slideInRight animated animated">Referrals that you trust</h2>
                <h3 class="wow slideInLeft animated animated"><a href="<%=DataAccess.HelperClasses.Common.IOSAppLink %>" target="_blank">
                    <img src="/images/apple-store-icon.png" alt="" class="brandname" />
                </a><%--<a href="#">
                    <img src="/images/google-play-icon.png" alt="" class="brandname" />
                </a>--%></h3>
                <a href="#responsive" class="locationpop" data-toggle="modal" style="text-align: center; float: none; color: #ffffff;"><i style="color: #ffffff;" class="fa currentLocation-icon fa-map-marker"></i>
                    <asp:Label ID="lblLocation" ClientIDMode="Static" Style="color: #ffffff;" runat="server"></asp:Label>
                    ( Change Your location ) </a>
                <div id="responsive" class="modal fade" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header backgroundtitle">
                                <button type="button" class="close btnLocationClose" data-dismiss="modal" aria-hidden="true"></button>
                                <h4 class="modal-title">Please enter your City or ZIP code</h4>
                            </div>
                            <div class="modal-body">
                                <div class="scroller" style="height: auto" data-always-visible="1" data-rail-visible1="1">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <div class="col-md-12" style="position: relative;">
                                                    <input type="text" id="txtSearchCity" autocomplete="off" placeholder="Enter City or Zipcode" class="form-control" />
                                                    <div id="divLoader" style="display: none;">
                                                        <img src="/images/377.gif" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="div_SearchContent">
                                </div>
                            </div>
                            <div class="modal-footer">
                                <a onclick="SetCurrentPosition();">Use my current location</a>
                                <button type="button" data-dismiss="modal" class="btn dark btn-outline">Close</button>
                                <button type="button" class="btn green" onclick="return ChangeLocation();">Save Changes</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <video autoplay loop muted id="video-bg">
            <asp:Repeater ID="rptVideos" runat="server">
                <ItemTemplate>
                    <source src="<%=DataAccess.HelperClasses.Common.HomeVideoPath%><%#Eval("Video") %>" type="video/mp4" />
                </ItemTemplate>
            </asp:Repeater>

        </video>
    </div>

    <!--==================================== End Home SliderEnd ====================================-->

    <!--==================================== Start Features ====================================-->

    <!--<a href="#t_botem"><div class="scrolldiv">  <img src="img/scroll-down-icon.png" alt=""></div></a>-->
    <div class="scrolldiv">
        <a href="#t_botem">
            <img src="/images/scroll-down-icon.png" alt="" />
        </a>
    </div>
    <div class="featurepart" id="t_botem">
        <div class="container">
            <asp:Repeater ID="rptCards" runat="server">
                <ItemTemplate>
                    <div class="col-md-6">
                        <div class="cardd12 cardbackground">
                            <div style="background-image: url('<%=DataAccess.HelperClasses.Common.HomeCardImagePath %><%#Eval("Image") %>'); background-position: center center; background-size: cover;" class="image">
                                <img style="display: none;" src="<%=DataAccess.HelperClasses.Common.HomeCardImagePath %><%#Eval("Image") %>" alt="<%#Eval("Image") %>" />
                                <div class="filter"></div>
                                <div class="contentproduct">
                                    <a href="http://<%#Eval("Link") %>" class="wow  animated animated animated bgimg">
                                        <img class="iconimage" src="<%=DataAccess.HelperClasses.Common.HomeCardIconImagePath %><%#Eval("IconImage") %>" alt="<%#Eval("IconImage") %>" />
                                        <h4 class="titlelist"><%#Eval("Title") %></h4>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <!--==================================== End Features ====================================-->

    <!--==================================== Start Referred HTML ====================================-->

    <div class="Referred">
        <div class="container">
            <div class="sec-title text-center mb50 wow animated colortext" data-wow-duration="500ms">
                <%--<h2>Professional <span class="">Referred </span>by your City Neighbors </h2>--%>
                <h2>Top <span class="">Local Professionals </span>by Category</h2>

            </div>
            <div class="small_images">
                <asp:Repeater ID="rptSubCategory" runat="server">
                    <ItemTemplate>
                        <div class="col-md-3">
                            <div class="imageSmall">
                                <img src="<%=DataAccess.HelperClasses.Common.SubCategoryImagesPath %><%#Eval("Image") %>?height=265&width=255&mode=crop&scale=both" alt="<%#Eval("Image") %>" />
                                <div class="smallBlack">
                                    <h2 class=""><%#Eval("Name") %> <a href="/SearchResult?type=<%#Eval("Name") %>">View Details </a></h2>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <!--==================================== End Referred HTML ====================================-->

    <!--==================================== Start Referred HTML ====================================-->

    <div class="Referred">
        <div class="container">
            <div class="sec-title text-center mb50 wow animated colortext" data-wow-duration="500ms">
                <%--<h2>Jobs posted by <span class="">People </span>From your City  </h2>--%>
                <h2>Top <span class="">Local Jobs </span>for Professionals  </h2>

            </div>
            <div class="small_images">
                <asp:Repeater ID="rptJobCategory" runat="server">
                    <ItemTemplate>
                        <div class="col-md-3">
                            <div class="imageSmall">
                                <img src="<%=DataAccess.HelperClasses.Common.SubCategoryImagesPath %><%#Eval("Image") %>?height=265&width=255&mode=crop&scale=both" alt="<%#Eval("Image") %>" />
                                <div class="smallBlack">
                                    <h2 class=""><%#Eval("Name") %> jobs <a href="/jobboard?type=<%#Eval("Name") %>">View Details </a></h2>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

    <!--==================================== End Referred HTML ====================================-->

    <!--==================================== Start Testimonial HTML ====================================-->

    <div class="testimonail" style="display:none;">
        <div class="container">
            <h1 class="wow animated animated one_testimonail">Testimonial </h1>
            <h4 class="wow animated animated two_testimonail">" just a few people who have found ReferLocals useful " </h4>
            <div class="test_monial">
                <asp:Repeater ID="rptTestimonial" runat="server" Visible="false">
                    <ItemTemplate>
                        <div class="test_monial1">
                            <div class="col-md-4 col-md-offset-1">
                                <div class="texttesti">
                                    <p><%#Eval("Description") %> </p>
                                    <a href="#" class="wow  animated animated animated bgimg animated">- <%#Eval("Name") %> </a>
                                </div>
                            </div>
                            <div class="col-md-6 col-md-offset-1">
                                <img src="<%=DataAccess.HelperClasses.Common.TestimonialImagesPath%><%#Eval("Image") %>" alt="<%#Eval("Image") %>" class="test_img" />
                            </div>
                        </div>
                    </ItemTemplate>

                    <AlternatingItemTemplate>
                        <div class="test_monial1">
                            <div class="col-md-6 col-md-offset-1">
                                <img src="<%=DataAccess.HelperClasses.Common.TestimonialImagesPath%><%#Eval("Image") %>" alt="<%#Eval("Image") %>" class="test_img" />
                            </div>
                            <div class="col-md-4 col-md-offset-1">
                                <div class="texttesti">
                                    <p><%#Eval("Description") %> </p>
                                    <a href="#" class="wow  animated animated animated">- <%#Eval("Name") %> </a>
                                </div>
                            </div>
                        </div>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div id="divWelcomeModal" class="modal fade estimateFade estimateFade_v step_fade" aria-hidden="false">
        <div class="modal-dialog modal-dialog1">
            <div class="modal-content">
                <div class="modal-header backgroundtitle">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title"><strong>Welcome</strong> </h4>
                </div>
                <div class="modal-body">
                    <div class="scroller" style="height: 120px" data-always-visible="1" data-rail-visible1="1">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="centetext_popup" style="text-align: center; float: none;">

                                    <p>We are building something strong and trustworthy. Become a part of Referlocals</p>
                                </div>
                                <div class="form-group groupestimate" style="margin-top: 0px;">
                                    <a href="/login#register" class="btn green" type="button">Register</a>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--==================================== End Testimonial HTML ====================================-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <script id="CitySearch_template" type="text/html">
        <label class="autocomplete_section" data-id="${Id}" data-text="${City} ${State}" onclick="return SetLocation(this);">
            ${City}, ${State}
        </label>
    </script>
    <script type="text/javascript">
        function HideWelcomeModal() {
            $("#divWelcomeModal").modal("hide");
        }
        function ShowWelcomeModal() {
            var isWelcomeModalShown = localStorage.getItem("WelcomeModalShown");
            if (isWelcomeModalShown == null) {
                $("#divWelcomeModal").modal();
                localStorage.setItem("WelcomeModalShown", true);
            }



        }
        $(document).ready(function () {
            setTimeout(ShowWelcomeModal, 10000);
            $(".locationpop").on("click", function () {
                $("#responsive").modal();
            })
            var checksession = '<%=Session["CheckSession"]%>';
            var userIDSession = '<%=Session["UserId"]%>';

            if (userIDSession != "") {
                IsLoggedIn = true;
            }
            else {
                IsLoggedIn = false;
            }

            var timeout;
            $('#txtSearchCity').keyup(function () {
                clearTimeout(timeout);
                var txtval = $(this).val();
                if (txtval.length > 1) {
                    $("#divLoader").show();
                    timeout = setTimeout(function () { GetLocation(txtval); }, 1000);
                }
                else {
                    $("#div_SearchContent").empty();
                    $("#divLoader").hide();
                }
            });
        });

        

        function ChangeLocation() {
            var id = $("#hdnLocationId").val();
            var location = $("#txtSearchCity").val();
            $.ajax({
                url: '/Profile.aspx/SetLocation',
                type: "POST",
                data: '{ "Id":"' + parseInt(id) + '","Location":"' + location.toString() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d == "done") {
                        $("#lblLocation").text(location);
                        $(".btnLocationClose").trigger('click');



                    }
                }
            });

        }

        function GetLocation(searchContent) {
            if (searchContent != "") {
                $.ajax({
                    url: '/Index.aspx/GetLocations',
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{ "Keyword":"' + searchContent + '"}',
                    success: function (result) {
                        if (result.d != null) {
                            if (result.d.length > 0) {
                                $("#divLoader").hide();
                                $("#div_SearchContent").empty();
                                $("#div_SearchContent").hide();
                                $("#CitySearch_template").tmpl(result.d).appendTo("#div_SearchContent");
                                $("#div_SearchContent").slideDown('slow');
                            }
                        }
                        else {
                            $("#divLoader").hide();
                            $("#div_SearchContent").empty();
                            $("#div_SearchContent").html('<label class="autocomplete_section">No Search Result Found!</label>');
                            $("#div_SearchContent").slideDown('slow');
                        }
                    }
                });
            }
            else {
                $("#divLoader").hide();
                $("#div_SearchContent").css("display", "none");
                $("#div_SearchContent").empty();
            }
        }
        function SetLocation(btn) {
            var id = $(btn).attr("data-id");
            var text = $(btn).attr("data-text");
            $("#txtSearchCity").val(text);
            $("#hdnLocationId").val(id);
            $("#div_SearchContent").css("display", "none");
            $("#div_SearchContent").empty();
        }

    </script>
    
    <script type="text/javascript" src="//maps.googleapis.com/maps/api/js?key=AIzaSyD2jTfQktxkZgpMSzkHnbbUc9vKbkvc1Hs&sensor=false"></script>
    <script type="text/javascript" src="/js/currentlocation.js"></script>
    
</asp:Content>
