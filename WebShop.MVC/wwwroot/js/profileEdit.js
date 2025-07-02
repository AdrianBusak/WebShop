$(document).ready(function () {
    $('#editProfileForm').submit(function (e) {
        e.preventDefault();

        let form = $(this);
        let actionUrl = form.attr('action');

        let formArray = form.serializeArray();
        let formDataObj = {};
        formArray.forEach(field => {
            formDataObj[field.name] = field.value;
        });
        console.log(formDataObj.Id);
        let formData = JSON.stringify(formDataObj);

        $.ajax({
            type: "PUT",
            url: actionUrl,
            contentType: 'application/json',
            data: formData,
            success: function (response) {
                alert("Profil uspješno ažuriran!");
                window.location.href = "/User/ProfileDetails";
            },
            error: function (xhr) {
                let errorMsg = xhr.responseText || "Greška pri spremanju.";
                alert(errorMsg);
            }
        });
    });
});