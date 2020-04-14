var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').dataTable({
        "ajax": {
            "url": "/movies/displaymovieinfo/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "start Time", "width": "20%" },
            { "data": "salon", "width": "20%" },
            { "data": "seats Left", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Movies/BookTicketView?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}