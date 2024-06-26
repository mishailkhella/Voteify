var AddEdit = function (id) {
  
    var url = "/Question/AddEdit?id=" + id;

    if (id > 0) {
        $('#titleExtraBigModal').html("Edit Question");
    }
    else {
        $('#titleExtraBigModal').html("Add Question");


    }
    loadExtraBigModal(url);
};


function Save() {
   
    var _frmQuestion = $("#frmQuestion").serialize();
    var isEdit = $("#Id").val() > 0; // Assuming you have an input field with id="VoteId" in your form
    $("#btnSave").val("Please Wait");
    $('#btnSave').attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "/Question/AddEdit",
        data: _frmQuestion,
        success: function (response) {
            Swal.fire({
                title: isEdit ? "Updated Successfully" : "Added Successfully",
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $("#btnSave").val("Save");
                $('#btnSave').removeAttr('disabled');
                $('#tblQuestion').DataTable().ajax.reload(null, false); // Use null and false to reset paging
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
function tblVotesDataTable() {
    $('#tblQuestion').DataTable({
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
                url: "/Question/Delete?id=" + id,
                success: function (result) {
                    var message = "Vote has been deleted successfully.";
                    Swal.fire({
                        title: message,
                        icon: 'info',
                    });

                    var table = $('#tblQuestion').DataTable();
                    table.clear();
                    table.destroy();
                    tblQuestionDataTable()
                }
            });
        }
    });
};