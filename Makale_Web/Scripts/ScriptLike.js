/// <reference path="jquery-3.6.1.min.js" />

$(function () {

    var notid_dizi = [];

    $("div[data-notid]").each(function (i, e) {
        notid_dizi.push($(e).data("notid"));
    });

    $.ajax({
        method: "POST",
        url: "/Not/GetLikes",
        data: { id_dizi: notid_dizi }
    }).done(function (data) {
        if (data.sonuc != null && data.sonuc.length > 0)
        {
            for (var i = 0; i < data.sonuc.length; i++)
            {
                var id = data.sonuc[i];
                var div = $("div[data-notid=" + id + "]");
                var btn = div.find("button[data-like]");
                btn.data("like", true);

                var span = btn.children().first();
                var span2 = btn.find("span.like-kalp");

                span.removeClass("glyphicon-heart-empty");
                span.addClass("glyphicon-heart");
            }
        }

    }).fail(function () {

    });

});