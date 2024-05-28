
var AddEdit = function (id) {

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
    //if (!$("#frmVote").valid()) {
    //    return;
    //}

    var _frmVote = $("#frmVote").serialize();
    $("#btnSave").val("Please Wait");
    $('#btnSave').attr('disabled', 'disabled');
    $.ajax({
        type: "POST",
        url: "/Vote/AddEdit",
        data: _frmVote,
        success: function (response) {
            Swal.fire({
                title: "Updated Successfully",
                icon: "success"
            }).then(function () {
                document.getElementById("btnClose").click();
                $("#btnSave").val("Save");
                $('#btnSave').removeAttr('disabled');
                $('#tblVotes').DataTable().ajax.reload();
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
                url: "/Vote/Delete?id=" + id,
                success: function (result) {
                    var message = "Vote has been deleted successfully.";
                    Swal.fire({
                        title: message,
                        icon: 'info',
                    });

                    var table = $('#tblVotes').DataTable();
                    table.clear();
                    table.destroy();
                    tblVotesDataTable()
                }
            });
        }
    });
};