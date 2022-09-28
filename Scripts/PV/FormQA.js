
var NoDocument = '' ;
var EffectiveDate = '';

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

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [day, month,year].join('-');
}

function GetHeaderForm() {
    $.ajax({
        url: baseUrl + "/ApiPVEtana/GetHeaderForm",
        type: "POST",
        data: {
            Form : "QA"
        },
        success: function (data) {
            if (data.Status == "1") {
                console.log("data", data)
                var obj = data.Return;
                var content = [];
                for (var i = 0; i < obj.length; i++) {
                    NoDocument = obj[i].NoDocument;
                    EffectiveDate = obj[i].EffectiveDate;
                    content.push(
                        [
                            obj[i].NoDocument,
                            obj[i].EffectiveDate,
                            "<button data-toggle='modal' data-target='#modalHeaderLaporan' onclick='InputHeaderQA(" + obj[i].Id +")' class='btn btn-danger'><i class='fa fa-wrench' aria-hidden='true'></i> UPDATE </button>"

                        ])

                }

                $('.HeaderDocQA').DataTable({
                    orderCellsTop: true,
                    fixedHeader: true,
                    data: content,
                    
                    "order": [[0, "desc"]],

                });
            } else {
                alertError(data.Message)
            }
        }
    })
}



function InputHeaderQA(Id) {
    $("#zId").val(Id)
}
function GetAllFormQA() {
    $.ajax({
        url: baseUrl + "/ApiPVEtana/GetFormQA",
        type: "POST",
        data: {
            Param: "ShowAllReport"
        },
        success: function (data) {
            if (data.Status == "1") {
                console.log("data", data)
                var obj = data.Return;
                var content = [];
                for (var i = 0; i < obj.length; i++) {

                    content.push(
                        [
                            obj[i].LetterNo,
                            obj[i].CreatedDate,
                            obj[i].CreatedBy,
                            obj[i].Telephone,
                            obj[i].Product,
                            obj[i].ComplaintCategory,
                            obj[i].Description,
                            obj[i].Receiver,
                            obj[i].Receiver == "" ? "<button data-toggle='modal' data-target='#myModal' onclick='InputReceiver(" + obj[i].Id + ")' class='btn btn-success'><i class='fa fa-user-md' aria-hidden='true'></i> Input Penerima & Kriteria Keluhan</button>" : obj[i].Receiver,
                            "<center><a target='_blank' href='" + baseUrl + "/Admin/DetailQA/?Idx=" + obj[i].LetterNo + "&&NoDocument=" + NoDocument.replace(/\//g, '_') + "&&EffectiveDate=" + EffectiveDate.replace(/\//g, '_') + " 'class='btn btn-primary'><i class='fa fa-eye' aria-hidden='true'></i> Detail</button></a>"

                        ])

                }
                
                $('.ListFormQA').DataTable({
                    dom: 'Bfrtip',
                    buttons: [
                        { extend: 'excel', className: 'btn btn-success' }
                    ],
                    orderCellsTop: true,
                    fixedHeader: true,
                    data: content,
                    "createdRow": function (row, data, dataIndex) {
                        if (data[7] == "") {
                            $(row).addClass('redClass');
                        }
                    },
                    "order": [[0, "desc"]],
                    "columnDefs": [
                        {
                            "targets": [7],
                            "visible": false,
                            
                        },
                    ]

                });
            } else {
                alertError(data.Message)
            }
        }
    })
}





function InputReceiver(Id) {
    $("#IdLaporan").val(Id);
}


$(document).ready(function () {
    GetOption("GetQAComplaintCriteria")
    GetHeaderForm();
    GetAllFormQA();

    

    $("#UpdateHeaderQA").on("submit", function (event) {
        event.preventDefault();
        loading();
        $.ajax({
            url: baseUrl + "/ApiPVEtana/UpdateHeaderForm",
            type: "POST",
            data: {
                NoDocument: $("#zNoDocument").val(),
                EffectiveDate: $("#zEffectiveDate").val(),
                Id: $("#zId").val()
            },
            success: function (data) {
                setTimeout(function () {

                    if (data.Status == "1") {
                        alertSuccess("Header Laporan Berhasil Di Update !", baseUrl + "/Admin/FormQA");
                    } else {
                        alertError(data.Message);
                    }
                }, 2000)
            }
        })

    })
    $("#UpdatePenerima").on("submit", function (event) {

        event.preventDefault();
        loading();
        $.ajax({
            url: baseUrl + "/ApiPVEtana/UpdateReceiver",
            type: "POST",
            data: {
                Param: "UpdateReceiver",
                Code: Code,
                Receiver: $("#Receiver").val(),
                Id: $("#IdLaporan").val(),
                IdComplaintCriteria: $("#QAIdComplaintCriteria").val()
            },
            success: function (data) {
                setTimeout(function () {

                    if (data.Status == "1") {
                        alertSuccess("Penerima Laporan Berhasil Di Tambahkan !", baseUrl + "/Admin/FormQA");
                    } else {
                        alertError(data.Message);
                    }
                }, 2000)
            }
        })

    });
})