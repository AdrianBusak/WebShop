$(document).ready(function () {
    $('#btn-delete').on('click', function (e) {
        e.preventDefault();
        const url = $(this).attr('href');
        $('#confirmModalMessage').text('Are you sure you want to delete this category?');
        $('#confirmDeleteBtn').off('click').on('click', function () {
            window.location.href = url;
        });
        $('#confirmDeleteModal').modal('show');
    });
});