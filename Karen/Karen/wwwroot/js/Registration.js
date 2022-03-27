$(document).ready(function () {
    $("#submit-reg").click(function () {


        var error = false;

        var login = $("#reg-form").find("[name='login']");
        var password = $("#reg-form").find("[name='password']");
        var password_repeat = $("#reg-form").find("[name='password_repeat']");

        

        if (password.val() != password_repeat.val()) {
            $("#passwordrepeat-error").text("Passwords don't equals");
            error = true;
        }

        if (!error) {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "/User/Registration",
                data: { login: login.val(), password: password.val()},
                success: function (result) {
                    if (result) {
                        window.location.href = "/User/RegistrationAccess";
                    }
                    else {
                        $("#button-error").text("Òàêîé ïîëüçîâàòåëü óæå íàéäåí!");

                    }

                }




            })

        }



    })
});