$(document).ready(function () {
    $('.quantity-input').on('change', function () {
        var quantity = $(this).val();
        var productId = $(this).closest('tr').data('id');
        $.ajax({
            url: updateCartItemUrl,
            type: 'PUT',
            data: { productId: productId, quantity: quantity },
            success: function (response) {
                location.reload();
            },
            error: function () {
                alert('Došlo je do greške prilikom ažuriranja količine.');
            }
        });
    });
});