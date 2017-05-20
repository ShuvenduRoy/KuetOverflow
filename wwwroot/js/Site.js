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

            });

            return false;
        });

    $('#chat_user').on('click',
        'a',
        function () {
            setInterval("getAllMessaged()", 1000);


            var url = $(this).attr('href');
            var id = $(this).attr('user');

            $('#url').html(id);

            $('#messages').load(url);
            return false;

        }
    );



});


$(document).ready(function() {
    $.getJSON("/api/notifications",
        function (notifications) {
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

        $.getJSON(url,
            function(data) {
                $(obj).attr("src", data.picture.data.url);
            });
    });

    $('#addFollower').on('click',
        'a',
        function () {
            var url = $(this).attr('href');

        }
    );

    $('#addFollower').on('click',function() {
        url = $(this).attr('href');

        $.post(url).done(function(val) {

            if (val == 1) {
                $('#addFollower').addClass('btn-danger')
                    .html('Unfollow')
                    .removeClass('btn-primary');
            } else {
                $('#addFollower').addClass('btn-primary')
                    .html('Follow')
                    .removeClass('btn-danger');
            }
        });
    });

    $('#popularQuestion').load('/Questions/PopularQuestion');

    //$('#popular-people').load('/Tweets/WhoToFollow');

    $.get('/Tweets/WhoToFollow').done(function(val) {
        $('#popular-people').html(val);

        $('.user-image').each(function (i, obj) {
            var url = $(obj).attr("src");

            $.getJSON(url,
                function (data) {
                    $(obj).attr("src", data.picture.data.url);
                });
        });
    });


});



function getAllMessaged() {
    var url = $('#url').text();


    if (url != '#') {
        $.get('/messages/getallunreadmessages/' + url).done(function(data) {
            $('#row_Message').append(data);
        });
    }
    return false;
}



function sent_message() {
    var message = $('#message').val();
    var url = $('#url').text();


    var data = {
        To: url,
        Body: message
    }

    $('#message').val('');

    var dataType = 'application/x-www-form-urlencoded; charset=utf-8';
    $.ajax({
        type: "POST",
        url: "/Messages/SentMessage",
        dataType: 'json',
        contentType: dataType,
        data: data,
        success: function (result) {
            
        },
        error: function (d) {

            $('#row_Message').append(d.responseText);
        }
    });


}