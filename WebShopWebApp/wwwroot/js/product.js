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
                showToast("Proizvod je uspješno dodan u vašu košaricu.");
            },
            error: function () {
                showToast("Greška prilikom dodavanja u košaricu.", "danger");
            }
        });
    });
});

function showToast(message, type = 'success') {
    const toastId = `toast-${Date.now()}`;
    const bgClass = type === 'success' ? 'bg-success' : type === 'danger' ? 'bg-danger' : 'bg-primary';

    const toastHtml = `
    <div id="${toastId}" class="toast align-items-center text-white ${bgClass} border-0 mb-5" role="alert" aria-live="assertive" aria-atomic="true">
        <div class="d-flex">
            <div class="toast-body">
                ${message}
            </div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
    </div>`;

    $('#toast-container').append(toastHtml);

    const toastEl = document.getElementById(toastId);
    const toast = new bootstrap.Toast(toastEl, { delay: 3000 });
    toast.show();

    toastEl.addEventListener('hidden.bs.toast', () => {
        toastEl.remove();
    });
}
