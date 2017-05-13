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

    $('#user-image-div').on('click',
        'img',
        function () {
            var url = $(this).attr('src');
            $.getJSON(url, function (data) {
                console.log(data);
            });

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

    $('.user-image').each(function(i, obj) {
        var url = $(obj).attr("src");
        console.log(url);

        $.getJSON(url,
            function(data) {
                console.log(data);
                $(obj).attr("src", data.picture.data.url);
            });
    });

    $('#addFollower').on('click',
        'a',
        function () {
            var url = $(this).attr('href');
            console.log(url);

        }
    );

    $('#addFollower').on('click',function() {
        url = $(this).attr('href');
        console.log(url);

        $.post(url).done(function() {
            console.log('done');
        });
    });


});


