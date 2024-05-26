var loadExtraBigModal = function (url) {
    $("#ExtraBigModalDiv").load(url, function () {
        $("#ExtraBigModal").modal({
            show: false,
            backdrop: 'static'
        });
        $("#ExtraBigModal").modal("show");

    });
};