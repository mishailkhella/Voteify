$(document).ready(function () {
    
    document.title = 'Vote';
    tblVotesDataTable();
   
});
var tblVotesDataTable = function () {
    
    $("#tblVotes").DataTable({
        paging: true,
        select: true,
        "order": [[0, "desc"]],
        dom: 'Bfrtip',


        buttons: [
            'pageLength',
        ],
        "processing": true,
        "serverSide": true,
        "filter": true, //Search Box
        "orderMulti": false,
        "stateSave": true,

        "ajax": {
            "url": "/Vote/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },


        "columns": [
            {
                data: "Id", "name": "Id", render: function (data, type, row) {
                    return "<a href='#' class='fa fa-eye' onclick=Details('" + row.Id + "');>" + row.Id + "</a>";
                }
            },
            { "data": "VoteName", "name": "VoteName" },
            { "data": "FacultyName", "name": "FacultyName" },
            { "data": "DepartmentName", "name": "DepartmentName" },
            { "data": "SectionName", "name": "SectionName" },
            { "data": "LevelName", "name": "LevelName" },
            {
                "data": "CreatedDate",
                "name": "CreatedDate",
                "autoWidth": true,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (date.getDate() + "/" + month.length > 1 ? month : month) + "/" +  date.getFullYear();
                }
            },
            { "data": "CreatedBy", "name": "CreatedBy" },

            {
                "data": "ModifiedDate",
                "name": "ModifiedDate",
                "autoWidth": true,
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return ( date.getDate() + "/" + month.length > 1 ? month : month) + "/" + date.getFullYear();
                }
            },
            { "data": "ModifiedBy", "name": "ModifiedBy" },

            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-info btn-xs' onclick=AddEdit('" + row.Id + "');>Edit</a>";
                }
            },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-danger btn-xs' onclick=Delete('" + row.Id + "'); >Delete</a>";
                }
            }
        ],

        'columnDefs': [{
            'targets': [7, 8],
            'orderable': false,
        }],

        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });

} 

