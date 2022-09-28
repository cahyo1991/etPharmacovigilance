function GetDetailQA(Id) {
    $.ajax({
        url: baseUrl + "/ApiPVEtana/GetFormQA",
        type: "POST",
        data: {
            Param: "DetailReport",
            Id : Id
        },
        success: function (data) {
            if (data.Status == "1") {
                console.log("data", data)
                var obj = data.Return;
                var content = [];
                for (var i = 0; i < obj.length; i++) {
                    $("#QAId").val(obj[i].Id);
                    $("#QADate").val(obj[i].CreatedDate);
                    $("#QACreator").val(obj[i].CreatedBy);
                    $("#QAReporter").val(obj[i].CreatedBy);
                    
                    $("#QAUpdatedBy").val(obj[i].UpdatedBy);
                    $("#QAUpdatedDate").val(obj[i].UpdatedDate);
                    $("#QAStatus").val(obj[i].Status);
                    $("#QAIdCustomerType").val(obj[i].CustomerType);
                    $("#QACustomerTypeOthers").val(obj[i].CustomerTypeOthers);
                    $("#QAAddress").val(obj[i].Address);
                    $("#QATelephone").val(obj[i].Telephone);
                    $("#QAIdProduct").val(obj[i].IdProduct);
                    $("#QABatchNo").val(obj[i].BatchNo);
                    $("#QAExpDate").val(obj[i].ExpDate);
                    $("#QABuyDate").val(obj[i].BuyDate);
                    $("#QAAmountComplained").val(obj[i].AmountComplained);
                    $("#QAExampleAmount").val(obj[i].ExampleAmount);
                    $("#QAIdComplaintCategory").val(obj[i].IdComplaintCategory);
                    $("#QAComplaintCriteria").val(obj[i].ComplaintCriteria);
                    $("#QAProductCondition").val(obj[i].ProductCondition);
                    $("#QADescription").val(obj[i].Description);
                    $("#QAReceive").val(obj[i].Receiver);
                    $("#QANoLetter").val(obj[i].LetterNo);
                    $("#QACustomerType").val(obj[i].CustomerType);
                    $("#QAProduct").val(obj[i].Product);
                    $("#QAComplaintCategory").val(obj[i].ComplaintCategory);
                    $("#QAUsername").val(obj[i].UsernameProduct);

                    //content.push(
                    //    [
                    //        obj[i].LetterNo,
                    //        obj[i].CreatedBy,
                    //        obj[i].Telephone,
                    //        obj[i].Product,
                    //        obj[i].ComplaintCategory,
                    //        obj[i].Description,
                    //        "<center><a target='_blank' href='" + baseUrl + "/Admin/DetailQA/?Idx=" + obj[i].LetterNo + "'class='btn btn-primary'><i class='fa fa-eye' aria-hidden='true'></i> Detail</button></a>"

                    //    ])

                }

   
            } else {
                alertError(data.Message)
            }
        }
    })
}

$(document).ready(function () {
    GetDetailQA(NoLetter);
})