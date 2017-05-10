$(function() {
    jQuery.ajaxSetup({ async: true });

    $('#lectureList').on('click',
        '#paging a',
        function () {
            $(".list-group-item").removeClass("active");
            $(this).addClass("active");

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
        function (notifications) {
            console.log(notifications);
            if (notifications.length > 0) {
                $("#js-notifications-count").text(notifications.length)
                    .addClass("animated bounceInDown")
                    .removeClass("hide");

                $(".notification").popover({
                    html: true,
                    title: "Notification",
                    content: function () {
                        var compiled = _.template($("#notifications-template").html());
                        var html = compiled({ notifications: notifications });
                        return html;
                    },
                    placement: "bottom"

                }).on("shown.bs.popover", function () {
                    console.log("pop over shown");
                    $.post("/api/notifications/MarkAsRead")
                        .done(function () {
                            $("#js-notifications-count").text(notifications.length)
                                .addClass("hide");

                        });
                });
            }

        });

   
});


