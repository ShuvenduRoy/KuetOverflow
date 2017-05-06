$(function () {
    jQuery.ajaxSetup({ async: true });

    $('#lectureList').on('click', '#paging a', function () {
        var url = $(this).attr('href');

        $('#mainContent').load(url);

        return false;
    })

})