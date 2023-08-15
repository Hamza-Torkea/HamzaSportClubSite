//Picture


//create
$(function () {

    $("#save").click(function () {
        $("#view-err").append("Enter Picture");
        if ($("#save").val == "") {
            $("#err").fadeIn();
            $("#view-err").append("Enter Picture");
            return false;
        }
    })
})
