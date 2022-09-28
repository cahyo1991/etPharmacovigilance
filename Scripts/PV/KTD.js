var rowPushProduct = [];
var idrowPushProduct = 1;

var rowPushProductExternal = [];
var idrowPushProductExternal = 1;



function onSubmit(token) {
    alert(token)
}


function TitlePushProduct(i) {
    return "<div class='row' style='margin-top:80px;'><div class='col-md-5 col-sm-5 col-xs-5'> <hr /></div><div class='col-md-2 col-sm-2 col-xs-2'> <h3 style='text-align:center;margin-top:0px;'>Product " + (i + 1) +"</h3> </div><div class='col-md-4 col-sm-4 col-xs-4'> <hr /></div> <div class='col-md-1 col-sm-1 col-xs-1'> <button class='btn btn-danger' onclick='DeleteRowIP("+i+")'>Hapus / Delete</button> </div> </div>";
}

function RowOnePushProduct(i) {
    return "<div class='row'><div class='col-md-4'><p class='titleQ'>Nama Produk</p><p class='SubtitleQ'>Name Of Product<b class='required'>*</b></p><select class='ListProduct form-control' id='KTDIPIdProduct"+i+"' required></select></div><div class='col-md-4'><p class='titleQ'>Nomor Bets</p><p class='SubtitleQ'>Batch Number</p><input type='text' class='form-control' id='KTDIPBatchNo"+i+"' name='name' value='' /></div><div class='col-md-4'><p class='titleQ'>Produsen</p><p class='SubtitleQ'>Manufactur </p><input type='text' class='form-control' id='KTDIPManufactur"+i+"' name='name' value='' /></div></div>";
}

function RowTwoProduct(i) {
    return `
 <div class='row'>

                      <div class='col-md-4'>
                          <p class='titleQ'>Kategori Produk <b class='required'>*</b> </p>
                          <p class='SubtitleQ'>Product Category <b class='required'>*</b> </p>
                          <select class='form-control' id='KTDIPIdProductCategory`+i+`' required>

                          </select>
                      </div>
                      <div class='col-md-4'>
                          <p class='titleQ'>Tanggal Kadaluarsa</p>
                          <p class='SubtitleQ'>Expire Date</p>
                          <input type='date' id='KTDIPExpDate`+ i +`' class='form-control' name='name'  />
                      </div>
                      <div class='col-md-4'>
                          <p class='titleQ'>Tanggal Mulai Di Gunakan </p>
                          <p class='SubtitleQ'>Start Date Of Used</p>
                          <input type='date' id='KTDIPStartDate`+ i +`' class='form-control' name='name'  />
                      </div>
                  </div>
`;
}


function RowThreeProduct(i) {
    return `
  <div class='row'>

                      <div class='col-md-4'>
                          <p class='titleQ'>Dosis</p>
                          <p class='SubtitleQ'>Dosage  </p>
                          <input type='text'  class='form-control' name='name' id='KTDIPDossage`+i+`' />
                      </div>
                     <div class='col-md-4'>
                          <p class='titleQ'>Kondisi Obat</p>
                          <p class='SubtitleQ'>Drug Condition</p>
                           <select onchange='OtherDrugCondition(`+ i +`)' class='form-control' id='KTDIPDrugCondition`+ i + `'>
                        <option value='Baik'>Baik / Intact</option>
                        <option value='Cacat'>Cacat / Rusak - Defect / Damage</option>
                        <option value='Lainnya'>Lainnya (Jelaskan) / Others (Explain)</option>
                    </select>
<br/>
                    <input style='display:none;' type='text' class='form-control' name='name' id='KTDIPOtherDrugCondition`+ i +`' placeholder='Input Others Drug Condition' />
                      </div>
                      <div class='col-md-4'>
                          <p class='titleQ'>Durasi terapi</p>
                          <p class='SubtitleQ'>Therapy duration</p>
                          <input type='text' class='form-control' name='name' id='KTDIPTherapyDuration`+ i +`' />
                      </div>
                  </div>
`;
}

function RowLastProduct(i){
    return `
             <div class='row'>
                      <div class='col-md-6'>
                          <p class='titleQ'>Apa tujuan pemberian produk?</p>
                          <p class='SubtitleQ'>What was the product administered for?</p>
                          <textarea id='KTDIPProductAdministered`+ i +`' class='form-control'></textarea>
                      </div>
                      <div class='col-md-6'>
                          <p class='titleQ'>Bagaimana rute pemberian produk( melalui oral, injeksi, topical, dll)?</p>
                          <p class='SubtitleQ'>How was the product administered (by oral, by injection, etc)?</p>
                          <textarea class='form-control' id='KTDIPRouteProductAdministered`+i+`'></textarea>
                      </div>
                  </div>

                  <div class='row'>
                      <div class='col-md-6'>
                          <p class='titleQ'>Apakah produk ini masih digunakan?</p>
                          <p class='SubtitleQ'>Is the product still being taken? </p>
                          <select class='form-control' id='KTDIPProductTaken`+ i +`'>
                              <option value='Yes'>Ya / Yes</option>
                              <option value='No'>Tidak / No</option>
                          </select>
                      </div>
                      <div class='col-md-6'>
                          <p class='titleQ'>Bila produk digunakan Kembali, apakah kejadian tidak diinginkan terjadi kembali</p>
                          <p class='SubtitleQ'>If the product was restarted, did the side effect return?</p>
                          <select class='form-control' id='KTDIPProductSideEffectReturn`+ i +`'>
                              <option value='Yes'>Ya / Yes</option>
                              <option value='No'>Tidak / No</option>
                              <option value='DoesNotApply'>Tidak Sesuai / Does Not Apply</option>
                          </select>
                      </div>

                  </div>

                  <div class='row'>
                      <div class='col-md-6'>
                      </div>
                      <div class='col-md-6'>
                          <p class='titleQ'>Setelah produk dihentikan penggunaanya, apakah kejadian tidak diinginkan masih berlangsung?</p>
                          <p class='SubtitleQ'>After the usage was stopped, is the adverse event still happening? </p>
                          <select class='form-control' id='KTDIPProductStopStillHappening`+ i +`'>
                              <option value='Yes'>Ya / Yes</option>
                              <option value='No'>Tidak / No</option>
                              <option value='DoesNotApply'>Tidak Sesuai / Does Not Apply</option>
                          </select>
                      </div>

                  </div>

`;
}

function RowFourProduct(i) {
    return `

                  <div class='row'>

                      <div class='col-md-4'>
                          <p class='titleQ'>Kekuatan</p>
                          <p class='SubtitleQ'>Strength  </p>
                          <input type='text' class='form-control' name='name' id='KTDIPStrength`+i+`'  />
                      </div>
                      <div class='col-md-4'>
                          <p class='titleQ'>Kuantitas   </p>
                          <p class='SubtitleQ'>Quantity</p>
                          <input type='text' class='form-control' name='name' id='KTDIPQuantity`+ i +`'  />
                      </div>
                      <div class='col-md-4'>
                          <p class='titleQ'>Frekuensi</p>
                          <p class='SubtitleQ'>Frequency</p>
                          <input type='text' class='form-control' name='name' id='KTDIPFrequency`+ i +`' />
                      </div>
                  </div>
`
}


function RowExternalProduct(i) {
    return `
 <div class='row'>
            <div class='col-md-2'>
                <p class='titleQ'>Nama Produk</p>
                <p class='SubtitleQ'>Name of Product</p>
                <input type='text' class='form-control' name='name'  id='KTDEPProductName`+i+`' />
            </div>
            <div class='col-md-1'>
                <p class='titleQ'>Dosis</p>
                <p class='SubtitleQ'>Dosage</p>
                <input type='text' class='form-control' name='name' id='KTDEPDossage`+ i +`' />
            </div>
            <div class='col-md-1'>
                <p class='titleQ'>Frekuensi</p>
                <p class='SubtitleQ'>Frequency</p>
                <input type='text' class='form-control' name='name' id='KTDEPFrequency`+ i +`' />
            </div>
            <div class='col-md-2'>
                <p class='titleQ'>Rute</p>
                <p class='SubtitleQ'>Route</p>
                <input type='text' class='form-control' name='name' id='KTDEPRoute`+ i +`' />
            </div>
            <div class='col-md-2'>
                <p class='titleQ'>Tanggal mulai</p>
                <p class='SubtitleQ'>Start Date</p>
                <input type='date' class='form-control' name='name' id='KTDEPStartDate`+ i +`' />
            </div>
            <div class='col-md-2'>
                <p class='titleQ'>Durasi Terapi</p>
                <p class='SubtitleQ'>Therapy Duration</p>
                <input type='text' class='form-control' name='name' id='KTDEPTherapyDuration`+ i +`' />
            </div>
            <div class='col-md-2'>
<div class='row'>
                <div class='col-md-10'>
                <p class='titleQ'>Indikasi</p>
                <p class='SubtitleQ'>Indication</p>
                <input type='text' class='form-control' name='name'  id='KTDEPIndication`+ i +`' />
</div>
<div class='col-md-2'>
                  <button onclick='DeleteEP(`+i+`)' class='btn btn-danger' style='margin-top:35px;'>Hapus / Delete</button>
</div>
</div>
            </div>
        </div>
        <br />
`;
}




function OtherDrugCondition(index) {
    if ($("#" + "KTDIPDrugCondition" + index).val() == "Lainnya") {
        $("#KTDIPOtherDrugCondition" + index).attr('required', true);
        $("#KTDIPOtherDrugCondition" + index).show();
    } else {
        $("#KTDIPOtherDrugCondition" + index).attr('required', false);
        $("#KTDIPOtherDrugCondition" + index).hide();
    }
}


function GetProduct(IdSelector) {

    //$("#" + IdSelector).select2({
    //    placeholder: "Cari Produk / Search Product ...",
    //    minimumInputLength: 1,
    //    allowClear: true,
    //    ajax: {
    //        url: baseUrl + "/ApiPVEtana/GetProvinces",
    //        dataType: 'json',
    //        delay: 250,
    //        cache: false,
    //        data: function (params) {

    //            return {
    //                term: params.term,
    //                page: params.page || 1,
    //            };
    //        },
    //        processResults: function (data, params) {
    //            params.page = params.page || 1;
    //            //console.log('page =' + data.results)

    //            return {
    //                results: data.results,
    //                pagination: {
    //                    more: (params.page * 10) < data.count_filtered
    //                }
    //            };
    //        },
    //    }
    //});


    $.ajax({
        url: baseUrl + "/ApiPVEtana/GetOption",
        type: "POST",
        data: {
            Param: "GetProduct"
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


function GetProductCategory(IdSelector) {
    $.ajax({
        url: baseUrl + "/ApiPVEtana/GetOption",
        type: "POST",
        data: {
            Param: "GetProductCategoy"
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

function addRowExternalProduct() {
    var ab = "";
    rowPushProductExternal.push(ab);


    for (var i = 1; i < (rowPushProductExternal.length + 1); i++) {
        ab = ab + RowExternalProduct(i);
        
        
    }




    $(".MoreItemInteralProductExternal").html(ab)


}

function DeleteEP(index) {
    rowPushProductExternal.splice((index - 1), 1);
    var ab = "";



    for (var i = 1; i < (rowPushProductExternal.length + 1); i++) {
        ab = ab + RowExternalProduct(i);


    }




    $(".MoreItemInteralProductExternal").html(ab)

}

function DeleteRowIP(index) {

    rowPushProduct.splice((index - 1), 1);
    var b = "";

    for (var i = 1; i < (rowPushProduct.length + 1); i++) {
        b = b + TitlePushProduct(i) + RowOnePushProduct(i) + RowTwoProduct(i) + RowThreeProduct(i) + RowLastProduct(i);
        GetProduct('KTDIPIdProduct' + i)
        GetProductCategory('KTDIPIdProductCategory' + i)
    }




    $(".MoreItemInteralProduct").html(b)
}

function addRow() {
    var b = "";
    rowPushProduct.push(b);
    

    for (var i = 1; i < (rowPushProduct.length + 1); i++) {
        b = b + TitlePushProduct(i) + RowOnePushProduct(i) + RowTwoProduct(i) + RowThreeProduct(i) +  RowLastProduct(i);
        GetProduct('KTDIPIdProduct' + i)
        GetProductCategory('KTDIPIdProductCategory'+i)
    }




    $(".MoreItemInteralProduct").html(b)
    

}


function OthersSideEffect() {
    if ($("#sidefffect6").is(':checked')) {
        $("#sidefffect8").show();
        $("#sidefffect8").attr('required', true);
    } else {
        $("#sidefffect8").hide();
        $("#sidefffect8").attr('required', false);
    }
}


    function onClick(e) {
        e.preventDefault();
    grecaptcha.ready(function() {
        grecaptcha.execute('6Ld_nK4fAAAAAEj-0IODO0qh9WhwnShZp4vz7WgR', { action: 'submit' }).then(function (token) {
            // Add your logic to submit to your backend server here.
            $("#token_generate").val(token)
        });
        });
      }

function ShowUploadFiles() {
    $(".loadingImage").hide();
    $(".UploadedImage").hide();
    $(".UploadImage").show();
    $("#MultiFileName").val("")
}


function InsertFormKTDExternalProduct(IdFormKTD) {
    var obj = [];


    for (var i = 0; i < idrowPushProductExternal; i++) {

        var zKTDEPProductName = $("#KTDEPProductName" + i).val();
    var zKTDEPDossage = $("#KTDEPDossage" + i).val();
    var zKTDEPStartDate = $("#KTDEPStartDate" + i).val();
    var zKTDEPTherapyDuration = $("#KTDEPTherapyDuration" + i).val();
    var zKTDEPIndication = $("#KTDEPIndication" + i).val();
    var zKTDEPFrequency = $("#KTDEPFrequency" + i).val();
    var zKTDEPRoute = $("#KTDEPRoute" + i).val();


            if (zKTDEPProductName != "") {
                obj.push({
                    KTDEPIdFormKTD: IdFormKTD,
                    KTDEPProductName: zKTDEPProductName,
                    KTDEPDossage: zKTDEPDossage,
                    KTDEPStartDate: zKTDEPStartDate,
                    KTDEPTherapyDuration: zKTDEPTherapyDuration,
                    KTDEPIndication: zKTDEPIndication,
                    KTDEPFrequency: zKTDEPFrequency,
                    KTDEPRoute: zKTDEPRoute
                })
            }

    }

    console.log("length prodct external",obj.length)


    if (obj.length > 0) {
        $.ajax({
            url: baseUrl + "/ApiPVEtana/LoopInsertKTDExternalProduct",
            type: "POST",
            data: {
                obj: JSON.stringify(obj)
            },
            success: function (data) {
                if (data.Status == "1") {
                    setTimeout(function () {
                        alertSuccess("Terima Kasih , Laporan Berhasil Di Kirim", baseUrl + "/Complaint")

                        //alertSuccess("Data Berhasil Di Ajukan", baseUrl + "/Home/Discount")
                    }, 2000)
                    //alertSuccess("Data Berhasil Di Ajukan", baseUrl + "/Home/Discount")
                } else {
                    alertError(data.Message)
                }
            }

        })
    } else {
        alertSuccess("Terima Kasih , Laporan Berhasil Di Kirim", baseUrl + "/Complaint")

    }
 
   
}


function InsertFormKTDInternalProduct(IdFormKTD) {
    var obj = [];
    //alert(idrowPushProduct)
    for (var i = 0; i < idrowPushProduct; i++) {
        var xIdProduct = $("#KTDIPIdProduct" + i).val();
        var xBatchNo = $("#KTDIPBatchNo" + i).val();
        var xManufactur = $("#KTDIPManufactur" + i).val();
        var xExpDate = $("#KTDIPExpDate" + i).val();
        var xStartDate = $("#KTDIPStartDate" + i).val();
        var xDossage = $("#KTDIPDossage" + i).val();
        var xDrugCondition = $("#KTDIPDrugCondition" + i).val();
        var xProductAdministered = $("#KTDIPProductAdministered" + i).val();
        var xRouteProductAdministered = $("#KTDIPRouteProductAdministered" + i).val();
        var xProductTaken = $("#KTDIPProductTaken" + i).val();
        var xProductSideEffectReturn = $("#KTDIPProductSideEffectReturn" + i).val();
        var xProductStopStillHappening = $("#KTDIPProductStopStillHappening" + i).val();
        var xIdProductCategory = $("#KTDIPIdProductCategory" + i).val();
        var xTherapyDuration = $("#KTDIPTherapyDuration" + i).val();
        var xOtherDrugCondition = $("#KTDIPOtherDrugCondition" + i).val();

        if (xIdProduct != null) {
            obj.push({
                IdFormKTD: IdFormKTD,
                IdProduct: xIdProduct,
                BatchNo: xBatchNo,
                Manufactur: xManufactur,
                ExpDate: xExpDate,
                StartDate: xStartDate,
                Dossage: xDossage,
                DrugCondition: xDrugCondition,
                ProductAdministered: xProductAdministered,
                RouteProductAdministered: xRouteProductAdministered,
                ProductTaken: xProductTaken,
                ProductSideEffectReturn: xProductSideEffectReturn,
                ProductStopStillHappening: xProductStopStillHappening,
                IdProductCategory: xIdProductCategory,
                TherapyDuration: xTherapyDuration,
                OtherDrugCondition: xOtherDrugCondition
            })
        }

    }

    $.ajax({
        url: baseUrl + "/ApiPVEtana/LoopInsertKTDInternalProduct",
        type: "POST",
        data: {
            obj: JSON.stringify(obj)
        },
        success: function (data) {
            if (data.Status == "1") {
                InsertFormKTDExternalProduct(IdFormKTD)
                //alertSuccess("Data Berhasil Di Ajukan", baseUrl + "/Home/Discount")
            } else {
                alertError(data.Message)
            }
        }

    })

}

$(document).ready(function () {

    //


    // check input checkbox

    //if ($('input.checkbox_check').is(':checked')) {
    //    alertError("Belum d Checked")
    //} else {
       
    //}

    $('#IdProvince').select2({
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


    GetProduct('KTDIPIdProduct0')
    GetProductCategory('KTDIPIdProductCategory0')
    $(".MoreItemProduct").click(function () {
        addRow();
        idrowPushProduct = idrowPushProduct + 1;
    })


    $(".MoreItemProductExternal").click(function () {
        
        addRowExternalProduct();
        idrowPushProductExternal = idrowPushProductExternal + 1;
    })



    $(".loadingImage").hide();
    $(".UploadedImage").hide();




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


    //save QA
    $("#InsertFormKTD").on("submit", function (event) {
 

        event.preventDefault();
        //InsertFormKTDInternalProduct("fds");

        var response = grecaptcha.getResponse();
        console.log("response",response)
        if (grecaptcha.getResponse() != "") {
            loading();
            var BadSideEffect = "";
            for (var i = 0; i < 8; i++) {
                if ($("#sidefffect" + i).is(':checked') && i != 6) {
                    BadSideEffect = BadSideEffect + $("#sidefffect" + i).val() + ";"
                }

                if ($("#sidefffect" + i).is(':checked') && i == 6) {
                    BadSideEffect = BadSideEffect + "Others : " + $("#sidefffect8").val() + ";";
                }

            }




            setTimeout(function () {
                $.ajax({
                    url: baseUrl + "/ApiPVEtana/InsertFormKTD",
                    type: "POST",
                    data: {
                        Name: $("#Name").val(),
                        Address: $("#Address").val(),
                        IdProvince: $("#IdProvince").val(),
                        PhoneEmail: $("#PhoneEmail").val(),
                        PostalCode: $("#PostalCode").val(),
                        InitialName: $("#InitialName").val(),
                        YearAge: $("#YearAge").val(),
                        MonthAge: $("#MonthAge").val(),
                        HeightBody: $("#HeightBody").val(),
                        WeightBody: $("#WeightBody").val(),
                        Sex: $("#Sex").val(),
                        Profession: $("#Profession").val(),
                        DescribeAdverseEvent: $("#DescribeAdverseEvent").val(),
                        OtherReleventInformation: $("#OtherReleventInformation").val(),
                        SideEffectStart: $("#SideEffectStart").val(),
                        SideEffectStop: $("#SideEffectStop").val(),
                        FeelingPatient: $("#FeelingPatient").val(),
                        ReportBPOM: $("#ReportBPOM").val(),
                        SideEffectBad: BadSideEffect,
                        Images: $("#MultiFileName").val(),
                        WillingContacted: $("#WillingContacted").val()
                    },
                    success: function (data) {
                        setTimeout(function () {
                            if (data.Status == "1") {
                                //alertSuccess(data.Message, baseUrl + "/Complaint")
                                InsertFormKTDInternalProduct(data.Message);
                            } else {
                                alertError(data.Message)
                            }
                        }, 2000)

                    }
                })
            }, 3000)

        } else {
            //not validated or not clicked
            alertError("Please Checked Google Captcha !")
        }


      




    });


})