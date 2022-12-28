
var notid = -1;

$(function () {
    $('#modal1').on('show.bs.modal', function (e) {

        var button = $(e.relatedTarget);
        notid = button.data("notid");

        $('#modal1_body').load("/Yorum/YorumGoster/" + notid);

    });
});

function yorumislem(btn, islem, yorumid, yorumtext)
{
    var button = $(btn);
    var editmod = button.data("edit");

    if (islem === "update")
    {
        if (!editmod) {
            button.data("edit", true);
            button.removeClass("btn-warning");
            button.addClass("btn-success");
            var span = button.find("span");
            span.removeClass("glyphicon-edit");
            span.addClass("glyphicon-ok");

            $(yorumtext).attr("contenteditable", true);
            $(yorumtext).focus();

        }
        else
        {
            button.data("edit", false);
            button.removeClass("btn-success");
            button.addClass("btn-warning");
            var span = button.find("span");
            span.removeClass("glyphicon-ok");
            span.addClass("glyphicon-edit");

            $(yorumtext).attr("contenteditable", false);

            var yenitext = $(yorumtext).text();

            $.ajax({
                method: "POST",
                url: "/Yorum/Edit/" + yorumid,
                data: { text: yenitext }
            }).done(function (data) {
                if (data.sonuc)
                    //yorumlar yeniden yüklenir
                    $('#modal1_body').load("/Yorum/YorumGoster/" + notid);
                else
                    alert("Yorum güncellenemedi");

            }).fail(function () {
                alert("Sunucu ile bağlantı kurulamadı.");
            })

        }

    }
    else if (islem === "delete")
    {
        var mesaj = confirm("Yorum silinsin mi?");
        if (!mesaj)
            return false;

        $.ajax({
            method: "GET",
            url: "/Yorum/Delete/" + yorumid
        }).done(function (data) {
            if (data.sonuc)
                //yorumlar yeniden yüklenir
                $('#modal1_body').load("/Yorum/YorumGoster/" + notid);
            else
                alert("Yorum silinemedi");
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });

    }
    else if (islem === "create")
    {
        var yorum = $("#yorum_text").val();

        $.ajax({
            method: "Post",
            url: "/Yorum/Create",
            data: { Text: yorum, notid: notid }
        }).done(function (data) {
            if (data.sonuc)
                //yorumlar yeniden yüklenir
                $('#modal1_body').load("/Yorum/YorumGoster/" + notid);
            else
                alert("Yorum eklenemedi");
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });
    }

}