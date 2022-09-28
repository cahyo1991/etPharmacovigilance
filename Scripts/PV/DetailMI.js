
$(document).ready(function () {

    $.ajax({
        url: baseUrl + "/ApiPVEtana/ListDetailMI",
        type: "POST",
        data: {
            Id: Id
        },
        success: function (data) {
            if (data.Status == "1") {
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

                $("#ProductName").val(Obj[0].ProductName);
                $("#Question").val(Obj[0].Question);
                $("#Name").val(Obj[0].Name);
                $("#Approriate").val(Obj[0].Approriate);
                $("#Telephone").val(Obj[0].Telephone);
                $("#Email").val(Obj[0].Email);
                $("#Country").val(Obj[0].Country);
                $("#Id").val(Obj[0].Id);
                $("#ReceivedBy").val(Obj[0].ReceivedBy);
                $("#ProductName").attr('readonly', 'true');
                $("#Question").attr('readonly', 'true');
                $("#Name").attr('readonly', 'true');
                $("#Approriate").attr('readonly', 'true');
                $("#Telephone").attr('readonly', 'true');
                $("#Email").attr('readonly', 'true');
                $("#Country").attr('readonly', 'true');
                $("#Id").attr('readonly', 'true');
                $("#ReceivedBy").attr('readonly', 'true');
            } else {
                alertError(data.Message)
            }
        }
    })

})