// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Show alerts for TempData messages
$(document).ready(function () {
    // Check for success message
    var successMessage = $('body').data('success-message');
    if (successMessage) {
        showAlert(successMessage, 'success');
    }

    // Check for error message
    var errorMessage = $('body').data('error-message');
    if (errorMessage) {
        showAlert(errorMessage, 'danger');
    }

    // Check for warning message
    var warningMessage = $('body').data('warning-message');
    if (warningMessage) {
        showAlert(warningMessage, 'warning');
    }

    // Check for info message
    var infoMessage = $('body').data('info-message');
    if (infoMessage) {
        showAlert(infoMessage, 'info');
    }
});

function showAlert(message, type) {
    var alertHtml = '<div class="alert alert-' + type + ' alert-dismissible fade show alert-fixed" role="alert">' +
        message +
        '<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>' +
        '</div>';

    $('body').append(alertHtml);

    // Auto hide after 5 seconds
    setTimeout(function () {
        $('.alert-fixed').fadeOut('slow', function () {
            $(this).remove();
        });
    }, 5000);
}