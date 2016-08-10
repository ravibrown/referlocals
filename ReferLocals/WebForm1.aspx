<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ReferLocals.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/fullcalendar.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <link href="/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <script src="/js/jquery.min.js" type="text/javascript"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" type="text/javascript"></script>
    
    <script src="/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="/js/test.js"></script>--%>
    <link href="//raw.githubusercontent.com/Eonasdan/bootstrap-datetimepicker/master/build/css/bootstrap-datetimepicker.min.css" type="text/css" />
    <script type="text/javascript">
        
        $(function () {
            var gotodate = new Date();
            gotodate = gotodate.setMonth(10);
            $('#calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },

                
                editable: true,
                eventLimit: true, // allow "more" link when too many events
                events: [
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    }, {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },  
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    },
                    {
                        'title': 'All Day Event',
                        'start': '2016-06-01'
                    }
                    ,
                    {
                        title: 'Long Event1',
                        start: '2016-06-07',
                        end: '2016-06-10'
                    },
                    {
                        title: 'Long Event2',
                        start: '2016-06-07',
                        end: '2016-06-10'
                    },
                    {
                        title: 'Long Event3',
                        start: '2016-06-07',
                        end: '2016-06-10'
                    },
                    {
                        title: 'Long Event4',
                        start: '2016-06-07',
                        end: '2016-06-10'
                    },
                    {
                        title: 'Long Event5',
                        start: '2016-06-07',
                        end: '2016-06-10'
                    },
                    {
                        title: 'Long Event6',
                        start: '2016-06-07',
                        end: '2016-06-10'
                    },
                    {
                        title: 'Long Event7',
                        start: '2016-06-07',
                        end: '2016-06-10'
                    },
                     {
                         title: 'Long Event7',
                         start: '2016-06-07',
                         end: '2016-06-10'
                     }, {
                         title: 'Long Event7',
                         start: '2016-06-07',
                         end: '2016-06-10'
                     }, {
                         title: 'Long Event7',
                         start: '2016-06-07',
                         end: '2016-06-10'
                     },
                  
                ]
            });
            $('.fc-next-button').on("click", function () {

                alert("hi");


            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="calendar" class="has-toolbar"> </div>
    </form>
    <script src="/js/moment.min.js" type="text/javascript"></script>
    
    <script src="/js/fullcalendar.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
       
        
    </script>
</body>
</html>
