let selectedProductId = null;
let selectedQuantity = 1;
let selectedProductName = '';

$(document).ready(function () {

    $('.btn-add').on('click', function () {
        selectedProductId = $(this).data('id');
        selectedQuantity = parseInt($(`.quantity-input[data-id=${selectedProductId}]`).val()) || 1;
        selectedProductName = $('.product-name').text() || 'ovaj proizvod';

        $('#confirmModalMessage').text(`Potvrdi dodavanje ${selectedQuantity}x "${selectedProductName}" u košaricu.`);

        var modal = new bootstrap.Modal(document.getElementById('confirmAddModal'));
        modal.show();
    });

    $('#confirmAddBtn').on('click', function () {
        $.ajax({
            url: '/UserCart/AddToCart',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ productId: selectedProductId, quantity: selectedQuantity }),
            success: function () {
                $('#confirmAddModal').modal('hide');
            },
            error: function () {
                alert("Greška prilikom dodavanja.");
            }
        });
    });
});
