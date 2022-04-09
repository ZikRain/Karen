$(document).ready(function () {
    $("#SignIn").click(function () {
        var error = false;
        var login = $("#Login").val();
        var password = $("#Password").val();

        if (login == "" | login == null) {
            $("#Login-error").text("Введите логин");
            error = true;
        }

        if (password == "" | password == null) {
            $("#Password-error").text("Введите пароль");
            error = true;
        }

        if (!error) {
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "/User/SignIn",
                data: { login: login, password: password },
                success: function (result) {
                    if (result) {
                        window.location.href = "/Home/Index";
                    }
                    else {
                        $("#SignIn-error").text("Такой пользователь не существует!");

                    }

                }




            })

        }

    })
});