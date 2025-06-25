$(document).ready(function () {
    const jwt = localStorage.getItem("JWT");

    if (!jwt) {
        alert("Niste prijavljeni.");
        window.location.href = "../../Login/login.html";
        return;
    }

    loadLogs(jwt);
});

function loadLogs(jwt) {
    $.ajax({
        method: "GET",
        url: "http://localhost:5131/api/Log",
        headers: {
            "Authorization": "Bearer " + jwt
        }
    }).done(function (logs) {
        logs.forEach(log => {
            console.log(`Log =`, log);

            $("#logs-table tbody").append(`
                <tr scope="row">
                    <td>${log.id}</td>
                    <td>${log.level}</td>
                    <td>${log.message}</td>
                    <td>${formatTimestamp(log.timestamp)}</td>
                </tr>
            `);

            $("#loading-spinner").hide();
        });

    }).fail(function () {
        alert("Greška prilikom učitavanja logova.");
    });
}

function formatTimestamp(timestamp) {
    const date = new Date(timestamp);
    return date.toLocaleString();
}
