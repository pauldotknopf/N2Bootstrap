$(function () {
    // give invalid tokens tooltip help
    $('span.invalid-token').each(function() {
        $(this).tooltip({
            html: $(this).attr("title")
        });
    });
});