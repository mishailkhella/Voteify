
var AddEdit = function (id) {
    debugger
    var url = "/Vote/AddEdit?id=" + id;

    if (id > 0) {
        $('#titleExtraBigModal').html("Edit Book");
    }
    else {
        $('#titleExtraBigModal').html("Add Book");


    }
    loadExtraBigModal(url);
};

var Save = function () {
    debugger
    if (!$("#frmVote").valid()) {
        return;
    }

    var _frmVote = $("#frmVote").serialize();
    $("#btnSave").val("Please Wait");
    $('#btnSave').attr('disabled', 'disabled');
    $.ajax({
        type: "POST",
        url: "/Vote/AddEdit",
        data: _frmVote,
        success: function (result) {
            Swal.fire({
                title: result,
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $("#btnSave").val("Save");
                $('#btnSave').removeAttr('disabled');
                $('#tblBook').DataTable().ajax.reload();
            });
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}
