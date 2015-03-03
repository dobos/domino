<%@ Page Language="C#" Theme="" %>
<%

    Response.ContentType = "text/javascript";
    Response.AppendHeader("Content-Disposition", "inline;filename=timeout.js");

%>
$(function () {

    var timeout = <%= Session.Timeout / 3 * 60000 %>;

    if ($("#timer").length == 1) {
        $(document).ready(timerInit());
    }

    function timerInit() {
        timerRefresh();
    }

    function timerRefresh() {

        var min = Math.floor(timeout / 60000);
        var sec = Math.floor((timeout / 1000) % 60);

        var mins = ("00" + min.toString()).slice(-2);
        var secs = ("00" + sec.toString()).slice(-2);

        $("#timer").text(mins + ":" + secs);

        if (timeout <= 0) {
            alert("<%= Resources.Labels.SessionTimeout %>");
            window.navigate("<%= Complex.Domino.Util.Url.ToAbsoluteUrl(ResolveClientUrl(Complex.Domino.Web.Auth.SignOut.GetUrl())) %>");
        }
        else {
            timeout -= 1000;
            setTimeout(timerRefresh, 1000);
        }
    }
});