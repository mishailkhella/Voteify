
var AddEdit = function (id) {

    var url = "/QuestionAnswer/AddEdit?id=" + id;

    if (id > 0) {
        $('#titleExtraBigModal').html("Edit Answer");
    }
    else {
        $('#titleExtraBigModal').html("Add Answer");


    }
    loadExtraBigModal(url);
};

function Save() {
 
    var _frmQuestionAnswer = $("#frmQuestionAnswer").serialize();
    var isEdit = $("#Id").val() > 0; // Assuming you have an input field with id="VoteId" in your form
    $("#btnSave").val("Please Wait");
    $('#btnSave').attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "/QuestionAnswer/AddEdit",
        data: _frmQuestionAnswer,
        success: function (response) {
            Swal.fire({
                title: isEdit ? "Updated Successfully" : "Added Successfully",
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $("#btnSave").val("Save");
                $('#btnSave').removeAttr('disabled');
                $('#tblQuestionAnswer').DataTable().ajax.reload(null, false); // Use null and false to reset paging
            });
        },
        error: function () {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Something went wrong!",
                footer: '<a href="#">Why do I have this issue?</a>'
            });
            $("#btnSave").val("Save");
            $('#btnSave').removeAttr('disabled');
        }
    });
}

// Ensure tblVotesDataTable() is defined to initialize #tblVotes DataTable
function tblQuestionAnswerDataTable() {
    $('#tblQuestionAnswer').DataTable({
        // DataTable configuration options
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