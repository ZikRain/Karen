$(document).ready(function () {

    $(document).on('click', '.btn-add-cart', function () {
        var productid = $(this).parents('.product').attr('data-productid');
        var button = $(this);
        $.ajax({
            url: "/Cart/AddCartItem",
            method: "POST",
            data: { productid },
            success: function (data) {
                if (data) {
                    button.hide();
                } else {
                    window.location.href = "/User/SignInPage";
                }
            }
        });
    });
});