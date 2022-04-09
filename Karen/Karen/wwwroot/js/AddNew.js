
function additem() {
    var name = $("#name").val();
    var type = $("type").val();
    var image = $("#image").prop('files')[0];
    var count = $("#count").val();
    var place1 = $("#place1").val();
    var place2 = $("#place2").val();
    var price = $("#price").val();
    var error = false;

    var formdata = new FormData;
    formdata.append('name', name);
    formdata.append('type', type);
    formdata.append('image', image);
    formdata.append('count', count);
    formdata.append('place1', place1);
    formdata.append('place2', place2);
    formdata.append('price', price);



    if (name == "" | name == null) {
        $("#name-error").text("name dont empty");
        error = true;
    }
    if (count == "" | !(count > 0)) {
        $("#count-error").text("count dont empty");
        error = true;
    }
    if (place1 == "" | !(place1 > 0)) {
        $("#place1-error").text("place1 dont empty");
        error = true;
    }
    if (place2 == "" | !(place2 > 0)) {
        $("#place2-error").text("place2 dont empty");
        error = true;
    }
    if (price == "" | !(price > 0)) {
        $("#price-error").text("price dont empty");
        error = true;
    }

    if (!error) {
        $.ajax({
        type: 'POST',
        dataType: "json",
        url: "/Product/AddNewProduct",
        processData: false,
        contentType: false,
        cache: false,
        data:formdata
            ,
            success: function (result) {
                if (result) {
                    window.location.href = "/Home/Index";
                }
            }
    })
    }

}
