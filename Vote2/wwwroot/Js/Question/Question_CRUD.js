
var AddEdit = function (id) {
    debugger
    var url = "/Question/AddEdit?id=" + id;

    if (id > 0) {
        $('#titleExtraBigModal').html("Edit Question");
    }
    else {
        $('#titleExtraBigModal').html("Add Question");


    }
    loadExtraBigModal(url);
};

//function Save() {
//    var _frmVote = $("#tblQuestion").serialize();
//    $("#btnSave").val("Please Wait");
//    $('#btnSave').attr('disabled', 'disabled');

//    $.ajax({
//        type: "POST",
//        url: "/Vote/AddEdit",
//        data: _frmVote,
//        success: function (response) {
//            Swal.fire({
//                title: "Updated Successfully",
//                icon: "success"
//            }).then(function () {
//                document.getElementById("btnClose").click();
//                $("#btnSave").val("Save");
//                $('#btnSave').removeAttr('disabled');
//                $('#tblQuestion').DataTable().ajax.reload(null, false); // Use null and false to reset paging
//            });
//        },
//        error: function () {
//            Swal.fire({
//                icon: "error",
//                title: "Oops...",
//                text: "Something went wrong!",
//                footer: '<a href="#">Why do I have this issue?</a>'
//            });
//            $("#btnSave").val("Save");
//            $('#btnSave').removeAttr('disabled');
//        }
//    });
//}




var Save = function () {
    debugger;
    // Uncomment the validation if necessary
    // if (!$("#frmVote").valid()) {
    //     return;
    // }

    var _frmQuestion = $("#frmQuestion").serialize();
    $("#btnSave").val("Please Wait");
    $('#btnSave').attr('disabled', 'disabled');

    $.ajax({
        type: "POST",
        url: "/Question/AddEdit",
        data: _frmQuestion,
        success: function (response) {
            Swal.fire({
                title: "Updated Successfully",
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $("#btnSave").val("Save");
                $('#btnSave').removeAttr('disabled');

                //// Assuming there is a table inside the form with id 'frmQuestionTable'
                //var questionTable = $('#frmQuestion').DataTable();
                //if (questionTable) {
                //    questionTable.ajax.reload(null, false);
                //}

                //// Handling the '#tblVotes' DataTable
                var votesTable = $('#tblQuestion').DataTable();
             
                    votesTable.clear();
                    votesTable.destroy();
                    tblQuestionDataTable();
                
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
};

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