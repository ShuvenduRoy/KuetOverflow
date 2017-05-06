$(function () {
    jQuery.ajaxSetup({ async: true });

    $('#lectureList').on('click', '.pager a', function () {
        var url = $(this).attr('href');

        $('#mainContent').load(url);

        //jQuery.ajax({
        //    url: url,
        //    success: function (result) {
        //        if (result.isOk == false) alert(result.message);
        //        document.getElementById('mainContent').innerHTML = result;
        //    },
        //    async: true
        //});

        return false;
    })

})