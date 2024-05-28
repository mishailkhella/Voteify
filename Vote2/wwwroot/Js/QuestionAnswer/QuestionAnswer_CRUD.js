
var AddEdit = function (id) {
    debugger
    var url = "/QuestionAnswer/AddEdit?id=" + id;

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
    //if (!$("#frmVote").valid()) {
    //    return;
    //}
  
    var _frmQuestionAnswer = $("#frmQuestionAnswer").serialize();
    $("#btnSave").val("Please Wait");
    $('#btnSave').attr('disabled', 'disabled');
    $.ajax({
        type: "POST",
        url: "/QuestionAnswer/AddEdit",
        data: _frmQuestionAnswer,
        success: function (response) {
            Swal.fire({
                title: "Updated Successfully",
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $("#btnSave").val("Save");
                $('#btnSave').removeAttr('disabled');
                $('#frmQuestionAnswer').DataTable().ajax.reload();
            });
        },
        error: function (errormessage) {
            SwalSimpleAlert(errormessage.responseText, "warning");
        }
    });
}

var Delete = function (id) {


    Swal.fire({
        title: 'Do you want to delete this Vote?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/QuestionAnswer/Delete?id=" + id,
                success: function (result) {
                    var message = "Vote has been deleted successfully.";
                    Swal.fire({
                        title: message,
                        icon: 'info',
                    });

                    var table = $('#tblQuestionAnswer').DataTable();
                    table.clear();
                    table.destroy();
                    tblQuestionAnswerDataTable()
                }
            });
        }
    });
};