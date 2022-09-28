function GetProduct(IdSelector) {

    $.ajax({
        url: baseUrl + "/ApiPVEtana/GetOption",
        type: "POST",
        data: {
            Param: "GetAllProduct"
        },
        success: function (data) {
            //console.log("Data", data);
            var Ids = "#" + IdSelector;

            $(Ids).empty();
            $(Ids)
                .append($("<option></option>")
                    .attr("value", "")
                    .text("Pilih / Choose "));
            var obj = data.Return;
            $.each(obj, function (key, value) {
                $(Ids)
                    .append($("<option></option>")
                        .attr("value", obj[key].Id)
                        //.attr("id-distributor", obj[key].Id)
                        .text(obj[key].Name));
            });



        }
    })
}

function ShowUploadFiles() {
    $(".loadingImage").hide();
    $(".UploadedImage").hide();
    $(".UploadImage").show();
    $("#MultiFileName").val("")
}

$(document).ready(function () {
    GetProduct("ProductName");

    $("#Approriate").change(function () {
        if ($("#Approriate").val() == "Other") {
            $("#OtherApproriate").show();
        } else {
            $("#OtherApproriate").val("");
            $("#OtherApproriate").hide();
        }
    })


    $("form#UploadFileSupport").on("submit", function (event) {
        event.preventDefault();
        var formdata = new FormData(this);

        $(".UploadImage").hide();
        $(".loadingImage").show();

        $.ajax({
            url: baseUrl + '/ApiPVEtana/UploadImageKTD',
            method: 'POST',
            data: formdata,
            processData: false,
            contentType: false,
            success: function (data) {
                var Status = data.Status;

                setTimeout(function () {
                    if (Status == "1") {
                        $("#MultiFileName").val(data.Message)
                        //alertSuccess(data.Message)
                        $(".loadingImage").hide();
                        $(".UploadedImage").show();
                    } else {
                        alertError(data.Message)
                        $(".loadingImage").hide();
                        $(".UploadedImage").hide();
                        $(".UploadImage").show();
                    }
                }, 2000)


            }
        })

    })


    $("form#InsertFormMI").on("submit", function (event) {
        event.preventDefault();

        var response = grecaptcha.getResponse();
        console.log("response", response)

        if (grecaptcha.getResponse() != "") {

            loading();
            $.ajax({
                url: baseUrl + "/ApiPVEtana/InsertFormMedicalInquiry",
                type: "POST",
                data: {
                    MI_ProductName: $("#ProductName").val(),
                    MI_Question: $("#Question").val(),
                    MI_Name: $("#Name").val(),
                    MI_Approriate: $("#Approriate").val(),
                    MI_OtherApproriate: $("#OtherApproriate").val(),
                    MI_Telephone: $("#Telephone").val(),
                    MI_Email: $("#Email").val(),
                    MI_Country: $("#Country").val(),
                    MI_Images: $("#MultiFileName").val()
                },
                success: function (data) {
                    if (data.Status == "1") {
                        setTimeout(function () {
                            alertSuccess("Terima Kasih , Laporan Berhasil Di Kirim", baseUrl + "/Complaint")
                        }, 3000)
                    } else {
                        alertError(data.Message)
                    }
                }
            })

        } else {
            alertError("Please Checked Google Captcha !")

        }


    });



})