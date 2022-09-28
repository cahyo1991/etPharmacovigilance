
function DetailKIPI() {
    $.ajax({
        url: baseUrl + "/ApiPVEtana/ListDetailKIPI",
        type: "POST",
        data: {
            Id :Id
        },
        success: function (data) {
            if (data.Status == "1") {





                $("#Name").attr('readonly', 'true')
                $("#PostalCode").attr('readonly', 'true')
                $("#Parent").attr('readonly', 'true')
                $("#Age").attr('readonly', 'true')
                $("#Address").attr('readonly', 'true')
                $("#Sex").attr('readonly', 'true')
                $("#City").attr('readonly', 'true')
                $("#Wus").attr('readonly', 'true')
                $("#Phone").attr('readonly', 'true')
                $("#KU").attr('readonly', 'true')
                $("#Email").attr('readonly', 'true')
                $("#ReporterName").attr('readonly', 'true')
                $("#RepoterPhone").attr('readonly', 'true')
                $("#ReporterAddress").attr('readonly', 'true')
                $("#RepoterEmail").attr('readonly', 'true')
                $("#ReporterCity").attr('readonly', 'true')
                $("#ReporterPostalCode").attr('readonly', 'true')
                $("#ImmunizationGiver").attr('readonly', 'true')
                $("#FinalConditionPatient").attr('readonly', 'true')
                $("#FinalConditionPatientDieDate").attr('readonly', 'true')
                $("#SymptomsImmunized").attr('readonly', 'true')
                $("#SymptomsNotImmunized").attr('readonly', 'true')
                $("#TreatmentKIPI").attr('readonly', 'true')
                $("#OtherHealthInformation").attr('readonly', 'true')
                $("#WillingContacted").attr('readonly', 'true')
                $("#ReceivedBy").attr('readonly', 'true')
                console.log(data)
                var Obj = data.Return;


                var ArrImages = Obj[0].Images.split(';');
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

                $("#LetterNo").html(Obj[0].LetterNo);
                $("#Name").val(Obj[0].Name);
                $("#PostalCode").val(Obj[0].PostalCode);
                $("#Parent").val(Obj[0].Parent);
                $("#Age").val(Obj[0].Age);
                $("#Address").val(Obj[0].Address);
                $("#Sex").val(Obj[0].Sex);
                $("#City").val(Obj[0].City);
                $("#Wus").val(Obj[0].Wus);
                $("#Phone").val(Obj[0].Phone);
                $("#KU").val(Obj[0].KU);
                $("#Email").val(Obj[0].Email);
                $("#ReporterName").val(Obj[0].ReporterName);
                $("#RepoterPhone").val(Obj[0].RepoterPhone);
                $("#ReporterAddress").val(Obj[0].ReporterAddress);
                $("#RepoterEmail").val(Obj[0].RepoterEmail);
                $("#ReporterCity").val(Obj[0].ReporterCity);
                $("#ReporterPostalCode").val(Obj[0].ReporterPostalCode);
                $("#ImmunizationGiver").val(Obj[0].ImmunizationGiver);
                $("#FinalConditionPatient").val(Obj[0].FinalConditionPatient);
                if (Obj[0].FinalConditionPatient == "Sembuh") {
                    $(".die").hide();
                } else {
                    $(".die").show();
                }
                $("#FinalConditionPatientDieDate").val(Obj[0].FinalConditionPatientDieDate);
                $("#SymptomsImmunized").val(Obj[0].SymptomsImmunized);
                $("#SymptomsNotImmunized").val(Obj[0].SymptomsNotImmunized);
                $("#TreatmentKIPI").val(Obj[0].TreatmentKIPI);
                $("#OtherHealthInformation").val(Obj[0].OtherHealthInformation);
                $("#WillingContacted").val(Obj[0].WillingContacted);
                $("#ReceivedBy").val(Obj[0].ReceivedBy);
            } else {
                alertError(data.Message)
            }
        }
    })
}




$(document).ready(function () {
    DetailKIPI()
})