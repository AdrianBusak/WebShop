$(document).ready(function () {
    $('#add-admin-check').change(function () {
        if ($(this).is(':checked')) {
            $('#password-admin-container').show();
        } else {
            $('#password-admin-container').hide();
            $('input[name="PasswordAdmin"]').val('');
        }
    });

    if ($('#add-admin-check').is(':checked')) {
        $('#password-admin-container').show();
    }
});