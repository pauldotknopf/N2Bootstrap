$(function () {

    // initialize the ajax loading modal dialog
    $("#ajax-loading").modal({
        backdrop: 'static',
        show: false,
        keyboard: false
    });
    
    // make the freeform disable submit buttons on click
    $(".freeform .submit").click(function(e) {
        $(this).addClass("disabled");
    });

    // give invalid tokens tooltip help
    $('span.invalid-token').each(function () {
        $(this).tooltip({
            html: $(this).attr("title")
        });
    });
});