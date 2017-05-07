$(function () {
    jQuery.ajaxSetup({ async: true });

    $('#lectureList').on('click',
        '#paging a',
        function() {
            var url = $(this).attr('href');

            $('#mainContent').load(url);

            return false;
        });
    $('#vote').on('click',
        'a',
        function () {
            var url = $(this).attr('href');

            $('#vote').load(url);

            return false;
        });
   

})


