function DetailKtd() {
    $.ajax({
        url: baseUrl + "/ApiPVEtana/GetReportDetailKTD",
        type: "POST",
        data: {
            Id: IdFormKtd
        },
        success: function (data) {
            console.log("Data", data)
            if (data.Status == "1") {
                var obj = data.Return;
                $("#Name").val(obj[0].Name)
                $("#PhoneEmail").val(obj[0].PhoneEmail)
                $("#Address").val(obj[0].Address)
                $("#City").val(obj[0].City)
                $("#PostalCode").val(obj[0].PostalCode)

                $("#InitialName").val(obj[0].InitialName)
                $("#YearAge").val(obj[0].YearAge)
                $("#MonthAge").val(obj[0].MonthAge)
                $("#HeightBody").val(obj[0].HeightBody)
                $("#WeightBody").val(obj[0].WeightBody)
                $("#Sex").val(obj[0].Sex)
                $("#Profession").val(obj[0].Profession)

                $("#DescribeAdverseEvent").val(obj[0].DescribeAdverseEvent)
                $("#OtherReleventInformation").val(obj[0].OtherReleventInformation)

                $("#SideEffectStart").val(obj[0].SideEffectStart)
                $("#SideEffectStop").val(obj[0].SideEffectStop)
                $("#FeelingPatient").val(obj[0].FeelingPatient)
                $("#ReportBPOM").val(obj[0].ReportBPOM)
                $("#ReceivedBy").val(obj[0].ReceivedBy)
                $("#LetterNo").html(obj[0].LetterNo)
                $("#WillingContacted").val(obj[0].WillingContacted)

                var ArrSideEffect = obj[0].SideEffectBad.split(';');
                var SdEffect = "";

                var ArrImages = obj[0].Images.split(';');
                var SdImages = "";



                for (var i = 0; i < (ArrImages.length - 1); i++) {
                    //console.log("biji" + i + ArrSideEffect[i])
                    var Chxbox = `
<div class='checkbox'>
                        <label><input checked disabled type='checkbox' >
 <a href="` + baseUrl + `/Asset/ImageKTD/` + ArrImages[i] + `">` + baseUrl + `/Asset/ImageKTD/` + ArrImages[i] + `</a>
</label>
                    </div>
`;
                    SdImages = SdImages + Chxbox;
                }
                $(".SupportingFiles").html(SdImages)




                for (var i = 0; i < (ArrSideEffect.length - 1); i++) {
                    //console.log("biji" + i + ArrSideEffect[i])
                    var Chxbox = `
<div class='checkbox'>
                        <label><input checked disabled type='checkbox' >
`+ ArrSideEffect[i]+`
</label>
                    </div>
`;
                    SdEffect = SdEffect + Chxbox;
                }
                $(".SideEffectDetail").html(SdEffect)
            } else {
                alertError(data.Message)
            }
        }
    })
}

$(document).ready(function () {
    DetailKtd()
})