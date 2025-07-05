$(document).ready(function () {
    $('.btn-add').click(function () {
        let productId = $(this).data('id');
        let quantity = $(`.quantity-input[data-id=${productId}]`).val();

        $.ajax({
            url: '/UserCart/AddToCart',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ productId: productId, quantity: quantity }),
            success: function () {
                alert("Proizvod dodan u košaricu!");
            },
            error: function () {
                alert("Greška prilikom dodavanja.");
            }
        });
    });
});