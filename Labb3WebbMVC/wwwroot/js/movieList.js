$(document).ready(function () {
    $('#receiptDiv').html('<img src="loading.gif"/>')
        .load('BookingConfirmation/Movies');
});

var receipt;

$(document).ready(function () {
    loadReceipt();
});

function loadReceipt() {
    receipt = $('#receiptDiv').load(function () {
        $.ajax({
            url: ""
        })
    })
}