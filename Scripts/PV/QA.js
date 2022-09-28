
function GetOption(Param) {
    $.ajax({
        url: baseUrl + "/ApiPVEtana/GetOption",
        type: "POST",
        data: {
            Param: Param
        },
        success: function (data) {
            //console.log("Data", data);
            if (Param == "GetQACustomerType") {
                $("#QAIdCustomerType").empty();
                $('#QAIdCustomerType')
                    .append($("<option></option>")
                        .attr("value", "")
                        .text("Pilih / Choose "));
                var obj = data.Return;
                $.each(obj, function (key, value) {
                    $('#QAIdCustomerType')
                        .append($("<option></option>")
                            .attr("value", obj[key].Id)
                            //.attr("id-distributor", obj[key].Id)
                            .text(obj[key].Name));
                });
            }

            if (Param == "GetQAComplaintCategory") {
                $("#QAIdComplaintCategory").empty();
                $('#QAIdComplaintCategory')
                    .append($("<option></option>")
                        .attr("value", "")
                        .text("Pilih / Choose "));
                var obj = data.Return;
                $.each(obj, function (key, value) {
                    $('#QAIdComplaintCategory')
                        .append($("<option></option>")
                            .attr("value", obj[key].Id)
                            //.attr("id-distributor", obj[key].Id)
                            .text(obj[key].Name));
                });
            }
            if (Param == "GetQAComplaintCriteria") {
                $("#QAIdComplaintCriteria").empty();
                $('#QAIdComplaintCriteria')
                    .append($("<option></option>")
                        .attr("value", "")
                        .text("Pilih / Choose "));
                var obj = data.Return;
                $.each(obj, function (key, value) {
                    $('#QAIdComplaintCriteria')
                        .append($("<option></option>")
                            .attr("value", obj[key].Id)
                            //.attr("id-distributor", obj[key].Id)
                            .text(obj[key].Name));
                });
            }
            if (Param == "GetProduct") {
                $("#QAIdProduct").empty();
                $('#QAIdProduct')
                    .append($("<option></option>")
                        .attr("value", "")
                        .text("Pilih / Choose "));
                var obj = data.Return;
                $.each(obj, function (key, value) {
                    $('#QAIdProduct')
                        .append($("<option></option>")
                            .attr("value", obj[key].Id)
                            //.attr("id-distributor", obj[key].Id)
                            .text(obj[key].Name));
                });
            }

            
        }
    })
}



$(document).ready(function () {
    GetOption("GetQACustomerType")
    GetOption("GetQAComplaintCategory")
    GetOption("GetQAComplaintCriteria")
    GetOption("GetProduct")


    $("#QAIdCustomerType").change(function () {
        if ($("#QAIdCustomerType").val() == "8") {
            $(".othersCT").show();
            $("#QACustomerTypeOthers").prop("required", true);
        } else {
            $(".othersCT").hide();
            $("#QACustomerTypeOthers").prop("required", false);
        }
    });


    //save QA
    $("#SubmitFormQA").on("submit", function (event) {

        event.preventDefault();

        var response = grecaptcha.getResponse();
        console.log("response", response)
        if (grecaptcha.getResponse() != "") {

            loading();

            $.ajax({
                url: baseUrl + "/ApiPVEtana/InsertQA",
                type: "POST",
                data: {
                    QACreator: $("#QACreator").val(),
                    QAStatus: $("#QAStatus").val(),
                    QAIdCustomerType: $("#QAIdCustomerType").val(),
                    QACustomerTypeOthers: $("#QACustomerTypeOthers").val(),
                    QAAddress: $("#QAAddress").val(),
                    QATelephone: $("#QATelephone").val(),
                    QAIdProduct: $("#QAIdProduct").val(),
                    QABatchNo: $("#QABatchNo").val(),
                    QAExpDate: $("#QAExpDate").val(),
                    QABuyDate: $("#QABuyDate").val(),
                    QAAmountComplained: $("#QAAmountComplained").val(),
                    QAExampleAmount: $("#QAExampleAmount").val(),
                    QAIdComplaintCategory: $("#QAIdComplaintCategory").val(),
                    QAIdComplaintCriteria: $("#QAIdComplaintCriteria").val(),
                    QAProductCondition: $("#QAProductCondition").val(),
                    QADescription: $("#QADescription").val(),
                    QAReceiver: $("#QAReceiver").val(),
                    QALetterNo: $("#QALetterNo").val(),
                    QAUserNameProduct: $("#QAUserNameProduct").val()
                },
                success: function (data) {

                    setTimeout(function () {
                        if (data.Status == "1") {
                            alertSuccess("Terima Kasih , Laporan Berhasil Di Kirim", baseUrl + "/Complaint")
                        } else {
                            alertError(data.Message, baseUrl)
                        }

                    }, 3000)


                }
            })
        } else {
            alertError("Please Checked Google Captcha !")
        }

        });



})