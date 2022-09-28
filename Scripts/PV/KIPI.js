var rowPushVaccine = [];
var idrowPushVaccine = 1;


function ContentHtml(i) {
    var ContentHtmls = `
 <div class='VaccinteItem'>
     <div class='row' style='margin-top:80px;'><div class='col-md-5 col-sm-5 col-xs-5'> <hr /></div><div class='col-md-2 col-sm-2 col-xs-2'> <h3 style='text-align:center;margin-top:0px;'>Vaccine ` + (i + 1) + `</h3> </div><div class='col-md-4 col-sm-4 col-xs-4'> <hr /></div> <div class='col-md-1 col-sm-1 col-xs-1'> <button class='btn btn-danger' onclick='DeleteRowVaccine(` + i + `)'>Hapus / Delete</button> </div> </div>
            <div class='row'>
                <div class='col-md-4'>
                    <p class='titleQ'>Nama Vaccine <b class='required'>*</b></p>
                    <p class='SubtitleQ'>Vaccine Name<b class='required'>*</b></p>
                    <select class='form-control' id='V_IdVaccine`+ i + `'>
                        <option value='value'>text</option>
                    </select>
                </div>
                <div class='col-md-4'>
                    <p class='titleQ'>Nomor Bets</p>
                    <p class='SubtitleQ'>Batch Number</p>
                    <input type='text' class='form-control' id='V_BatchNo`+ i + `' name='name' />
                </div>
                <div class='col-md-4'>
                    <p class='titleQ'>Produsen</p>
                    <p class='SubtitleQ'>Manufactur</p>
                    <input type='text' class='form-control' id='V_Manufactur`+ i + `' name='name' />
                </div>
            </div>
            <br />
            <br />
            <div class='row'>
                <h4 class='title black'>Informasi Pemberian Vaksin</h4>
                <h5 class='subtitle leftmargin'><i>Vaccine Administration Information</i></h5>
            </div>
            <div class='row'>
                <div class='col-md-4'>
                    <p class='titleQ'>Tanggal</p>
                    <p class='SubtitleQ'>Date</p>
                    <input type='date' class='form-control' id='V_Date`+ i + `' name='name' />
                </div>
                <div class='col-md-4'>
                    <p class='titleQ'>Rute Pemberian</p>
                    <p class='SubtitleQ'>Route Of Administration</p>
                    <select class='form-control' id='V_Route`+ i + `'>
                             <option value="">Pilih / Choose</option>
                            <option value="Oral">Oral / Oral</option>
                            <option value="Intrakutan">Intrakutan / Intracutaneous</option>
                            <option value="Subkutan">Subkutan / Subcutaneous</option>
                            <option value="i.m">i.m / Intra muscular</option>
</select>
                </div>
                <div class='col-md-4'>
                    <p class='titleQ'>Lokasi Penyuntikan</p>
                    <p class='SubtitleQ'>Injection Site</p>
                    <input type='text' class='form-control' id='V_InjectionSite`+ i + `' name='name' />
                </div>
            </div>


            <div class='row'>
                <div class='col-md-4'>
                    <p class='titleQ'>Jumlah Dosis</p>
                    <p class='SubtitleQ'>Total Dose</p>
                    <input type='text' class='form-control' id='V_TotalDose`+ i + `' name='name' />
                </div>
                <div class='col-md-4'>
                    <p class='titleQ'>Kondisi Obat</p>
                    <p class='SubtitleQ'>Drug Condition</p>
                    <select onchange="OtherDrugCondition(`+ i + `)" class='form-control' id='V_DrugCondition` + i + `'>
                        <option value="">Pilih / Choose</option>

<option value='Intact'>Baik / Intact</option>
                        <option value='Deffect/damage'>Cacat/Rusak / Deffect/damage</option>
                        <option value='Others'>Lainnya (jelaskan) / Other (explain)</option>

                    </select>
                    <br />
                    <input type='text' style='display:none;' placeholder='Other Drug Condition' id='V_OtherDrugCondition`+ i + `' class='form-control' name='name' />
                </div>
                <div class='col-md-4'>
                    <p class='titleQ'>Nama fasilitas kesehatan tempat imunisasi</p>
                    <p class='SubtitleQ'>Name of healthcare facility where immunization given</p>
                    <input type='text' class='form-control' id='V_HealthcareFacility`+ i + `' name='name' value='' />
                </div>
            </div>
                <div class="row">
                    <div class="col-md-4">
                        <p class="titleQ">Waktu</p>
                        <p class="SubtitleQ">Time</p>
                        <input type="time" class="form-control" id="V_Time`+i+`" name="name" value="" />
                    </div>
                </div>

        </div>

            `;
    return ContentHtmls;
}

function addRow() {
    var b = "";
    rowPushVaccine.push(b);


    for (var i = 1; i < (rowPushVaccine.length + 1); i++) {
     
        b = b + ContentHtml(i);
        GetProductVaccine("V_IdVaccine" + i)
        //GetProduct('KTDIPIdProduct' + i)
        //GetProductCategory('KTDIPIdProductCategory' + i)
    }




    $(".MoreVaccineItem").html(b)


}

function GetProductVaccine(IdSelector) {



    $.ajax({
        url: baseUrl + "/ApiPVEtana/GetOption",
        type: "POST",
        data: {
            Param: "GetProductVaccine"
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

function DeleteRowVaccine(index) {


    rowPushVaccine.splice((index - 1), 1);
    
    var b = "";

    for (var i = 1; i < (rowPushVaccine.length + 1); i++) {
       
        b = b + ContentHtml(i);
        GetProductVaccine("V_IdVaccine"+i)
        //GetProduct('KTDIPIdProduct' + i)
        //GetProductCategory('KTDIPIdProductCategory' + i)
    }




    $(".MoreVaccineItem").html(b)
}

function OtherDrugCondition(index) {
    //alert($("#" + "V_DrugCondition" + index).val())
    if ($("#" + "V_DrugCondition" + index).val() == "Others") {
        $("#V_OtherDrugCondition" + index).attr('required', true);
        $("#V_OtherDrugCondition" + index).show();
    } else {
        $("#V_OtherDrugCondition" + index).attr('required', false);
        $("#V_OtherDrugCondition" + index).hide();
    }
}


function TreatmentChecked() {
    $("#TreatmentAction0").attr('required', false);
}

function SymptomsChecked(index) {

    $("#Symptoms1").attr('required', false);

    var idSysm = "Symptoms" + index;
    var dateSysm = "#DateSymptoms" + index;
    var SymptomsDetail = "#SymptomsDetail" + index;
    if (document.getElementById(idSysm).checked) {
        $(dateSysm).attr('required', true);
        if (index > 17) {
            $(SymptomsDetail).attr('required', true);
        }
    } else {
        $(dateSysm).attr('required', false);
        if (index > 17) {
            $(SymptomsDetail).attr('required', false);
        }
    }
}


function ListCity() {
    $('.ListCity').select2({
        placeholder: "Cari Kota / Search City ...",
        minimumInputLength: 1,
        allowClear: true,
        ajax: {
            url: baseUrl + "/ApiPVEtana/GetProvinces",
            dataType: 'json',
            delay: 250,
            cache: false,
            data: function (params) {

                return {
                    term: params.term,
                    page: params.page || 1,
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;
                //console.log('page =' + data.results)

                return {
                    results: data.results,
                    pagination: {
                        more: (params.page * 10) < data.count_filtered
                    }
                };
            },
        }
    });
}


function InsertFormKIPIInternal(IdFormKIPI) {
    var obj = [];
    //alert(idrowPushProduct)
    for (var i = 0; i < idrowPushVaccine; i++) {
        var xIdVaccine = $("#V_IdVaccine" + i).val();
        var xBatchNo = $("#V_BatchNo" + i).val();
        var xManufactur = $("#V_Manufactur" + i).val();
        var xDate = $("#V_Date" + i).val();
        var xRoute = $("#V_Route" + i).val();
        var xInjectionSite = $("#V_InjectionSite" + i).val();
        var xTotalDose = $("#V_TotalDose" + i).val();
        var xDrugCondition = $("#V_DrugCondition" + i).val();
        var xOtherDrugCondition = $("#V_OtherDrugCondition" + i).val();
        var xHealthcareFacility = $("#V_HealthcareFacility" + i).val();
        var xVtime = $("#V_Time" + i).val();
        if (xIdVaccine != null) {
            obj.push({
                KIPIIPIdVaccine: xIdVaccine,
                KIPIIPBatchNo: xBatchNo,
                KIPIIPManufactur: xManufactur,
                KIPIIPDate: xDate,
                KIPIIPRoute: xRoute,
                KIPIIPInjectionSite: xInjectionSite,
                KIPIIPTotalDose: xTotalDose,
                KIPIIPDrugCondition: xDrugCondition,
                KIPIIPOtherDrugCondition: xOtherDrugCondition,
                KIPIIPHealthcareFacility: xHealthcareFacility,
                KIPIIPIdFormKIPI: IdFormKIPI,
                KIPIIPTime: xVtime
            })
        }
    }



    $.ajax({
        url: baseUrl + "/ApiPVEtana/LoopInsertFormKIPIInternal",
        type: "POST",
        data: {
            obj: JSON.stringify(obj)
        },
        success: function (data) {
            if (data.Status == "1") {
                setTimeout(function () {
                    InsertFormKIPISysmptom(IdFormKIPI)
                },2000)
                
            } else {
                alertError(data.Message)
            }
        }

    })



}


function InsertFormKIPISysmptom(IdFormKIPI) {
    var obj = [];
    //alert(idrowPushProduct)
    for (var i = 0; i < 20; i++) {


        if ($('#Symptoms'+i).is(':checked'))  {
            obj.push({
                KIPISTitle: $("#SymptomsDetail" + i).val(),
                KIPISDateSymptoms: $("#DateSymptoms" + i).val(),
                KIPISTimeSymptoms: $("#TimeSymptoms" + i).val(),
                KIPISDurationSymptoms: $("#DurationSymptoms" + i).val(),
                KIPISDayStmptoms: $("#DayStmptoms" + i).val(),
                KIPISIdFormKIPI: IdFormKIPI
            })
        }
    }


    console.log("datax",obj)



    $.ajax({
        url: baseUrl + "/ApiPVEtana/LoopInsertFormKIPISysmptoms",
        type: "POST",
        data: {
            obj: JSON.stringify(obj)
        },
        success: function (data) {
            if (data.Status == "1") {
                setTimeout(function () {
                    InsertFormKIPITreatment(IdFormKIPI)
                }, 2000)
                
                
            } else {
                alertError(data.Message)
            }
        }

    })



}



function InsertFormKIPITreatment(IdFormKIPI) {
    var obj = [];
    //alert(idrowPushProduct)
    for (var i = 0; i < 4; i++) {


        if ($('#TreatmentAction' + i).is(':checked')) {

            obj.push({
                KIPITName: $("#TreatmentAction" + i).val(),
                KIPITDateTreatmentAction: i != 0 ? $("#DateTreatmentAction" + i).val() : '',
                KIPITHospitalTreatmentAction : i == 3 ? $("#HospitalTreatmentAction" + i).val() : '' ,
                KIPITIdFormKIPI : IdFormKIPI
            })
        }
    }


    console.log("datax", obj)



    $.ajax({
        url: baseUrl + "/ApiPVEtana/LoopInsertFormKIPITreatment",
        type: "POST",
        data: {
            obj: JSON.stringify(obj)
        },
        success: function (data) {
            if (data.Status == "1") {
                setTimeout(function () {
                    alertSuccess("Terima Kasih , Laporan Berhasil Di Kirim", baseUrl + "/Complaint")
                },2000)
                
            } else {
                alertError(data.Message)
            }
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

    $("#Symptoms1").attr('required', true);
    $("#TreatmentAction0").attr('required', true);


    ListCity();
    GetProductVaccine("V_IdVaccine0")
    $(".MoreVaccineProduct").click(function () {
        addRow();
        idrowPushVaccine = idrowPushVaccine + 1;
    });


    $("#FormKIPI").on("submit", function (event) {
        event.preventDefault();
        var response = grecaptcha.getResponse();
        console.log("response", response)
        loading();
        if (grecaptcha.getResponse() != "") {


            

            var FinalConditionPatient = "";
            var DateFinalConditionPatient = "";
            if ($('#FinalConditionPatient0').is(':checked')) {
                FinalConditionPatient = $("#FinalConditionPatient0").val();
            }
            if ($('#FinalConditionPatient1').is(':checked')) {
                FinalConditionPatient = $("#FinalConditionPatient1").val();
                DateFinalConditionPatient = $("#DateFinalConditionPatient1").val();
            }
            $.ajax({
                url: baseUrl + "/ApiPVEtana/InsertFormKIPI",
                type: "POST",
                data: {
                    KIPIName: $("#Name").val()
                    , KIPIPostalCode: $("#PostalCode").val()
                    , KIPIParent: $("#Parent").val()
                    , KIPIAge: $("#Age").val()
                    , KIPIAddress: $("#Address").val()
                    , KIPISex: $("#Sex").val()
                    , KIPIIdCity: $("#City").val()
                    , KIPIWus: $("#Wus").val()
                    , KIPIPhone: $("#Phone").val()
                    , KIPIKU: $("#KU").val()
                    , KIPIEmail: $("#Email").val()
                    , KIPIReporterName: $("#ReporterName").val()
                    , KIPIRepoterPhone: $("#RepoterPhone").val()
                    , KIPIReporterAddress: $("#ReporterAddress").val()
                    , KIPIRepoterEmail: $("#RepoterEmail").val()
                    , KIPIIdReporterCity: $("#ReporterCity").val()
                    , KIPIReporterPostalCode: $("#ReporterPostalCode").val()
                    , KIPIImmunizationGiver: $("#ImmunizationGiver").val()
                    , KIPIFinalConditionPatient: FinalConditionPatient
                    , KIPIFinalConditionPatientDieDate: DateFinalConditionPatient
                    , KIPISymptomsImmunized: $("#SymptomsImmunized").val()
                    , KIPISymptomsNotImmunized: $("#SymptomsNotImmunized").val()
                    , KIPITreatmentKIPI: $("#TreatmentKIPI").val()
                    , KIPIOtherHealthInformation: $("#OtherHealthInformation").val()
                    , KIPIWillingContacted: $("#WillingContacted").val()
                    , KIPICreatedBy: $("#Name").val()
                    , KIPICreatedDate: $("#CreatedDate").val()
                    , KIPIReceivedBy: $("#ReceivedBy").val()
                    , KIPIImages: $("#MultiFileName").val()
                    , KIPILetterNo: $("#LetterNo").val()
                },
                success: function (data) {
                    if (data.Status == "1") {
                        InsertFormKIPIInternal(data.Message)

                    } else {
                        alertError(data.Message)
                    }
                }
            })

        } else {
            alertError("Please Checked Google Captcha !")

        }
    });



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

})