

function Login(User,Pass) {
    $.ajax({
        url: baseUrl + "/ApiPVEtana/LoginAdmin",
        type: "POST",
        data: {
            Code: User,
            Password: Pass
        },
        success: function (data) {
            if (data.Status == "1") {
                console.log(data)
                if (data.Message == "QA") {
                    
                    alertSuccess("Berhasil Login, Tekan OK Untuk Melanjutkan !", baseUrl + "/Admin/FormQA");
                } else {
                    alertSuccess("Berhasil Login, Tekan OK Untuk Melanjutkan !", baseUrl + "/Admin/FormKTD");
                }
                
            } else {
                alertError(data.Message)
            }
        }
    })
}




$(document).ready(function () {
    //alert("Tester")
    $("#LoginForm").on("submit", function (event) {
        event.preventDefault();
        loading();

        setTimeout(function () {
            Login($("#Username").val(), $("#Password").val())
        }, 2000
        )

        
        


    });
})

