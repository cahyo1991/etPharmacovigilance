function GetAllFormKTD() {
    $.ajax({
        url: baseUrl + "/ApiPVEtana/ListFormKIPI",
        type: "GET",
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
                            obj[i].ReporterName,
                            obj[i].PasienName,
                            obj[i].Vaccine,
                            obj[i].Sysmptoms,
                            obj[i].ReceivedBy,
                            obj[i].ReceivedBy == "" ? "<button data-toggle='modal' data-target='#myModal' onclick='InputReceiver(" + obj[i].Id + ")' class='btn btn-success'><i class='fa fa-user-md' aria-hidden='true'></i> Input Penerima</button>" : obj[i].ReceivedBy,
                            obj[i].Parent,
                            obj[i].Age,
                            obj[i].City,
                            obj[i].Sex,
                            obj[i].Phone,
                            obj[i].Email,
                            obj[i].RepoterPhone,
                            obj[i].RepoterEmail,
                            obj[i].ReporterPostalCode,
                            obj[i].ImmunizationGiver,
                            obj[i].FinalConditionPatient,
                            obj[i].SymptomsImmunized,
                            obj[i].SymptomsNotImmunized,
                            obj[i].TreatmentKIPI,
                            obj[i].OtherHealthInformation,
                            obj[i].WillingContacted,
                            obj[i].Status,
                            "<center><a target='_blank' href='" + baseUrl + "/Admin/DetailKIPI/?Idx=" + obj[i].Id + "" + "" + " 'class='btn btn-primary'><i class='fa fa-eye' aria-hidden='true'></i> Detail</button></a> " +
                            "</br ></br > <a href='javascript:' data-toggle='modal' data-target='#modalUpdateStatusKTD' onclick='InputReceiver(" + obj[i].Id + ")' class='btn btn-warning'><i class='fa fa-pencil-square-o' aria-hidden='true' ></i> Update Status</button></a ></center>"

                        ])

                }

                $('.ListFormKTD').DataTable({
                    dom: 'Bfrtip',
                    buttons: [
                        { extend: 'excel', className: 'btn btn-success' }
                    ],
                    orderCellsTop: true,
                    fixedHeader: true,
                    data: content,
                    "createdRow": function (row, data, dataIndex) {
                        if (data[6] == "") {
                            $(row).addClass('redClass');
                        }
                    },
                    "order": [[0, "desc"]],
                    "columnDefs": [
                        {
                            "targets": [6],
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
    $("#zId").val(Id)
}

$(document).ready(function () {
    GetAllFormKTD();


    $("#UpdatePenerima").on("submit", function (event) {

        event.preventDefault();
        loading();
        $.ajax({
            url: baseUrl + "/ApiPVEtana/UpdateReceiverKIPI",
            type: "POST",
            data: {
                Code: Code,
                Receiver: $("#Receiver").val(),
                Id: $("#IdLaporan").val()
            },
            success: function (data) {
                setTimeout(function () {

                    if (data.Status == "1") {
                        alertSuccess("Penerima Laporan Berhasil Di Tambahkan !", baseUrl + "/Admin/FormKIPI");
                    } else {
                        alertError(data.Message);
                    }
                }, 2000)
            }
        })

    });




    $("#UpdateStatusKTD").on("submit", function (event) {
        event.preventDefault();
        //alert("Tester")
        loading();
        $.ajax({
            url: baseUrl + "/ApiPVEtana/HistoryUpdateForm",
            type: "POST",
            data: {
                UFUpdatedBy: Code,
                UFStatus: $("#ListStatus").val(),
                UFIdForm: $("#zId").val(),
                UFTypeForm: "KIPI"
            },
            success: function (data) {

                setTimeout(function () {

                    if (data.Status == "1") {
                        alertSuccess("Status Berhasil Di Ubah", baseUrl + "/Admin/FormKIPI")
                    } else {
                        alertError(data.Message)
                    }

                }, 3000)


            }
        })
    })











})