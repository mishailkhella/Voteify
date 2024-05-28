$(document).ready(function () {

    document.title = 'Vote';
    tblQuestionAnswerDataTable();

});
var tblQuestionAnswerDataTable = function () {
    $("#tblQuestionAnswer").DataTable({
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
            "url": "/QuestionAnswer/GetDataTabelData",
            "type": "POST",
            "datatype": "json"
        },


        "columns": [
            //{
            //    data: "Id", "name": "Id", render: function (data, type, row) {
            //        return "<a href='#' class='fa fa-eye' onclick=Details('" + row.Id + "');>" + row.Id + "</a>";
            //    }
            //},
            { "data": "VoteName", "name": "VoteName" },
            { "data": "QuestionName", "name": "QuestionName" },
            { "data": "AnswerName", "name": "AnswerName" },


            {
                "data": "CreatedDate",
                "name": "CreatedDate",
                "autoWidth": true,
                render: function (data) {
                    var date = new Date(data);
                    var day = date.getDate().toString().padStart(2, '0');
                    var month = (date.getMonth() + 1).toString().padStart(2, '0');
                    var year = date.getFullYear();
                    return `${day}/${month}/${year}`;
                }
            },
            { "data": "CreatedBy", "name": "CreatedBy" },
            {
                data: "ModifiedDate",
                name: "ModifiedDate",
                autoWidth: true,
                render: function (data) {
                    var date = new Date(data);
                    var day = date.getDate().toString().padStart(2, '0');
                    var month = (date.getMonth() + 1).toString().padStart(2, '0');
                    var year = date.getFullYear();
                    return `${day}/${month}/${year}`;
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
            'targets': [6, 7],
            'orderable': false,
        }],

        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
    });
}

















































//{

//    $("#tblQuestionAnswer").DataTable({
//        paging: true,
//        select: true,
//        "order": [[0, "desc"]],
//        dom: 'Bfrtip',


//        buttons: [
//            'pageLength',
//        ],
//        "processing": true,
//        "serverSide": true,
//        "filter": true, //Search Box
//        "orderMulti": false,
//        "stateSave": true,

//        "ajax": {
//            "url": "/QuestionAnswer/GetDataTabelData",
//            "type": "POST",
//            "datatype": "json"
//        },


//        "columns": [
//            //{
//            //    data: "Id", "name": "Id", render: function (data, type, row) {
//            //        return "<a href='#' class='fa fa-eye' onclick=Details('" + row.Id + "');>" + row.Id + "</a>";
//            //    }
//            //},
//            { "data": "VoteName", "name": "VoteName" },
//            { "data": "QuestionName", "name": "QuestionName" },
//            { "data": "AnswerName", "name": "AnswerName" },


//            {
//                "data": "CreatedDate",
//                "name": "CreatedDate",
//                "autoWidth": true,
//                render: function (data) {
//                    var date = new Date(data);
//                    var day = date.getDate().toString().padStart(2, '0');
//                    var month = (date.getMonth() + 1).toString().padStart(2, '0');
//                    var year = date.getFullYear();
//                    return `${day}/${month}/${year}`;
//                }
//            },
//            { "data": "CreatedBy", "name": "CreatedBy" },
//            {
//                data: "ModifiedDate",
//                name: "ModifiedDate",
//                autoWidth: true,
//                render: function (data) {
//                    var date = new Date(data);
//                    var day = date.getDate().toString().padStart(2, '0');
//                    var month = (date.getMonth() + 1).toString().padStart(2, '0');
//                    var year = date.getFullYear();
//                    return `${day}/${month}/${year}`;
//                }
//            },
//            { "data": "ModifiedBy", "name": "ModifiedBy" },

//            {
//                data: null, render: function (data, type, row) {
//                    return "<a href='#' class='btn btn-info btn-xs' onclick=AddEdit('" + row.Id + "');>Edit</a>";
//                }
//            },
//            {
//                data: null, render: function (data, type, row) {
//                    return "<a href='#' class='btn btn-danger btn-xs' onclick=Delete('" + row.Id + "'); >Delete</a>";
//                }
//            }
//        ],

//        'columnDefs': [{
//            'targets': [6, 7],
//            'orderable': false,
//        }],

//        "lengthMenu": [[20, 10, 15, 25, 50, 100, 200], [20, 10, 15, 25, 50, 100, 200]]
//    });
//}


