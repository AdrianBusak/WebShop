$(document).ready(function () {
    const jwt = localStorage.getItem("JWT");

    if (!jwt) {
        alert("Niste prijavljeni.");
        window.location.href = "../../Login/login.html";
        return;
    }

    $('#pageSizeSelect').on('change', function () {
        loadLogs(jwt);
    });

    loadLogs(jwt);
});

function loadLogs(jwt) {
    $("#loading-spinner").show();
    let logsShowCount = parseInt($("#pageSizeSelect").val()) || 10;
    console.log(logsShowCount);
    $("#logs-table tbody").empty();


    $.ajax({
        method: "GET",
        url: `http://localhost:5131/api/Log/${logsShowCount}`,
        headers: {
            "Authorization": "Bearer " + jwt
        }
    }).done(function (logs) {
        logs.forEach(log => {

            $("#logs-table tbody").append(`
                <tr scope="row">
                    <td>${log.id}</td>
                    <td>${log.level}</td>
                    <td>${log.message}</td>
                    <td>${formatTimestamp(log.timestamp)}</td>
                </tr>
            `);

        });
        $("#loading-spinner").hide();
    }).fail(function () {
        alert("Greška prilikom učitavanja logova.");
    });
}

function formatTimestamp(timestamp) {
    const date = new Date(timestamp);
    return date.toLocaleString();
}
