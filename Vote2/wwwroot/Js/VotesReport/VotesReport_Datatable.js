﻿$(document).ready(function () {
    document.title = 'Votes';
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
            "url": "/VotesReport/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },


        "columns": [
            {
                "data": "Id", "name": "Id", render: function (data, type, row) {
                    return "<a href='#' class='fa fa-eye' onclick=ReportDetails('" + row.Id + "');>" + row.VoteName + "</a>";
                }
            },
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
                    return date.getDate() + "/" + (month.length > 1 ? month : month) + "/" + date.getFullYear();
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
                    return date.getDate() + "/" + (month.length > 1 ? month : month) + "/" + date.getFullYear();
                }
            },
            { "data": "ModifiedBy", "name": "ModifiedBy" },
        ],

        'columnDefs': [{
            'targets': [0],
            'orderable': false,
        }],

        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });

}

var ReportDetails = function (VoteId) {
    location.href = "/VotesReport/Report?VoteId=" + VoteId;
}