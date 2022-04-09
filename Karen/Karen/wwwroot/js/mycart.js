$(document).ready(function () {

    $(document).on('click', '.btn-plus', function () {
        var cartitemid = $(this).parents('.product').attr('data-cartitemid');
        
        $.ajax({
            url: "/Cart/PlusCartItem",
            method: "POST",
            data: { cartitemid },
            success: function (data) {
                if (data) {
                    window.location.href = "/Cart/Cart";
                } 
            }
        });
    });

    $(document).on('click', '.btn-minus', function () {
        var cartitemid = $(this).parents('.product').attr('data-cartitemid');

        $.ajax({
            url: "/Cart/MinusCartItem",
            method: "POST",
            data: { cartitemid },
            success: function (data) {
                if (data) {
                    window.location.href = "/Cart/Cart";
                }
            }
        });
    });

    $(document).on('click', '.btn-delete', function () {
        var cartitemid = $(this).parents('.product').attr('data-cartitemid');

        $.ajax({
            url: "/Cart/DeleteCartItem",
            method: "POST",
            data: { cartitemid },
            success: function (data) {
                if (data) {
                    window.location.href = "/Cart/Cart";
                }
            }
        });
    });

    
});


function AddOrder() {
    $.ajax({
        url: "/Cart/AddOrder",
        method: "POST",
        data: {},
        success: function (data) {
            if (data) {
                window.location.href = "/Cart/Cart";
                alert("Ваш заказ был принят!");
            }
        }
    });
};