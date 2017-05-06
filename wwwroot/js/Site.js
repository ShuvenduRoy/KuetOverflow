$(function () {
    jQuery.ajaxSetup({ async: true });

    $('#lectureList').on('click', '.pager a', function () {
        var url = $(this).attr('href');

        $('#mainContent').load(url);

        return false;
    })

})