function GetAllFormMI() {
    $.ajax({
        url: baseUrl + "/ApiPVEtana/ListInputMI",
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
                            obj[i].ProductName,
                            obj[i].Name,
                            obj[i].Question,
                            obj[i].Approriate,
                            obj[i].Email + " / " + obj[i].Telephone,
                            obj[i].Country,
                            obj[i].ReceivedBy,
                            obj[i].ReceivedBy == "" ? "<button data-toggle='modal' data-target='#myModal' onclick='InputReceiver(" + obj[i].Id + ")' class='btn btn-success'><i class='fa fa-user-md' aria-hidden='true'></i> Input Penerima</button>" : obj[i].ReceivedBy,
                            obj[i].Status,
                            "<center><a target='_blank' href='" + baseUrl + "/Admin/DetailMI/?Idx=" + obj[i].Id + "" + "" + " 'class='btn btn-primary'><i class='fa fa-eye' aria-hidden='true'></i> Detail</button></a>" +
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
                        if (data[8] == "") {
                            $(row).addClass('redClass');
                        }
                    },
                    "order": [[0, "desc"]],
                    "columnDefs": [
                        {
                            "targets": [8],
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
    GetAllFormMI()

    $("#UpdatePenerima").on("submit", function (event) {

        event.preventDefault();
        loading();
        $.ajax({
            url: baseUrl + "/ApiPVEtana/UpdateReceiverMI",
            type: "POST",
            data: {
                Code: Code,
                Receiver: $("#Receiver").val(),
                Id: $("#IdLaporan").val()
            },
            success: function (data) {
                setTimeout(function () {

                    if (data.Status == "1") {
                        alertSuccess("Penerima Laporan Berhasil Di Tambahkan !", baseUrl + "/Admin/FormMI");
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
                UFTypeForm: "MI"
            },
            success: function (data) {

                setTimeout(function () {

                    if (data.Status == "1") {
                        alertSuccess("Status Berhasil Di Ubah", baseUrl + "/Admin/FormMI")
                    } else {
                        alertError(data.Message)
                    }

                }, 3000)


            }
        })
    })



})