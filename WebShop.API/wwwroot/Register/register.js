let adminPassword = "adminho";

function register() {
    $("#spinner-placeholder").addClass("spinner");
    $("#register-button").prop("disabled", true);

    if ($("#password-admin-check").is(":checked") && $("#passwordAdmin").val() !== adminPassword) {
        alert("Passwords do not match.");
        $("#spinner-placeholder").removeClass("spinner");
        $("#register-button").prop("disabled", false);
        return;
    }

    let registerUrl = "http://localhost:5131/api/User/Register";
    let registerData = {
        "firstName": $("#firstName").val(),
        "lastName": $("#lastName").val(),
        "email": $("#email").val(),
        "phone": $("#phone").val(),
        "username": $("#username").val(),
        "password": $("#password").val(),
        "roleId": $("#password-admin-check").is(":checked") ? 1 : 2 // 1 = Admin, 2 = User
    };

    $.ajax({
        method: "POST",
        url: registerUrl,
        data: JSON.stringify(registerData),
        contentType: 'application/json'
    }).done(function (tokenData) {
        jwtLogin();
    }).fail(function (err) {
        alert(err.responseText);
        localStorage.removeItem("JWT");
        $("#spinner-placeholder").removeClass("spinner");
    });
}

$(document).ready(function () {
    function toggleAdminContainer() {
        if ($("#password-admin-check").is(":checked")) {
            $("#password-admin-container").show();
        } else {
            $("#password-admin-container").hide();
        }
    }

    $("#password-admin-check").on("change", toggleAdminContainer);
    toggleAdminContainer();
});
