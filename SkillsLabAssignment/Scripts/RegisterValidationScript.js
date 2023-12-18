$(document).ready(function () {
    $('form').submit(function (event) {
        event.preventDefault();

        $('#errorMessages').empty();

        $.ajax({
            url: '/Register/HandleRegistration', 
            type: 'POST',
            data: $(this).serialize(),
            success: function (response) {
                if (response.success) {
                    window.location.href = response.redirectUrl;
                } else {
                    displayErrors(response.errors);
                }
            },
            error: function (xhr, status, error) {
                console.log('Error submitting form:', error);
            }
        });
    });

    function displayErrors(errors) {
        $('#errorMessages').html('<ul>' + errors.map(e => '<li>' + e + '</li>').join('') + '</ul>');
    }
});



 