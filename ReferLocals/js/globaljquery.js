function ChangeUTCToLocalDateTime() {
    //var shortDateFormat = 'DDD MMM yyyy HH:mm:ss';
    //var longDateFormat = 'dd/MM/yyyy HH:mm:ss';

    var LocalDateFormat = "D MMM yyyy HH:mm";
    jQuery(".LocalDateFormat").each(function (idx, elem) {
        var oldDate;
        if (jQuery(elem).is(":input")) {
            oldDate = jQuery(elem).val();

        } else {
            oldDate = jQuery(elem).text();
            
        }
        //jQuery(elem).text(jQuery.format.date(jQuery(elem).text(), LocalDateFormat));
       var newLocalDate = new Date(oldDate + ' UTC');
       jQuery(elem).text(jQuery.format.date(newLocalDate, LocalDateFormat));
    });
    
}