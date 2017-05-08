$(function() {
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
        function() {
            var url = $(this).attr('href');

            $('#vote').load(url);

            return false;
        });


});

$(document).ready(function() {
    $.getJSON("/api/notifications",
        function(notifications) {
            $(".js-notifications-count").text(notifications.length)
                .addClass("animated bounceInDown")
                .removeClass("hide");
        });

    $(".notification").popover({
        html: true,
        title: "Notification",
        content: function() {
            return "hi";
        },
        placement: "bottom"

    });
});


