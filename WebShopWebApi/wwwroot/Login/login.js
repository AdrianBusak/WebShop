let loginUrl = "http://localhost:5131/api/User/Login";

function jwtLogin() {
    $("#spinner-placeholder").addClass("spinner");
    $("#login-button").prop("disabled", true);

    let loginData = {
        "username": $("#username").val(),
        "password": $("#password").val()
    }
    $.ajax({
        method: "POST",
        url: loginUrl,
        data: JSON.stringify(loginData),
        contentType: 'application/json'
    }).done(function (tokenData) {

        localStorage.setItem("JWT", tokenData);

        $("#spinner-placeholder").removeClass("spinner");
        $("#login-button").prop("disabled", false);

        // redirect
        window.location.href = "../Logs-site/logs.html";
    }).fail(function (err) {
        alert(err.responseText);

        localStorage.removeItem("JWT");
        $("#spinner-placeholder").removeClass("spinner");
    });
}
function jwtLogout() {
    localStorage.removeItem("JWT");

    // redirect
    window.location.href = "../Login/login.html";
}