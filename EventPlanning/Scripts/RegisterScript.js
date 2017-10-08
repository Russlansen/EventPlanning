$(function () {
    $("#submit").prop("disabled", true);
    $("#Login").blur(function () {
        $.ajax({
            url: "/Users/Check",
            method: "POST",
            data: { name: $(this).val() },
            success: function (response) {
                $("#errorMsg").text(response).addClass("text-success").removeClass("text-danger");
                $("#submit").prop("disabled", false);
            },
            error: function (error) {
                $("#errorMsg").text(error.responseText).addClass("text-danger").removeClass("text-success");
                $("#submit").prop("disabled", true);
            }
        });
    });
});