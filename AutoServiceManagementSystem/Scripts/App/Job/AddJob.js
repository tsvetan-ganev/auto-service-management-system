/// <reference path="../../Vendor/jquery-1.10.2.js" />
/// <reference path="../../Vendor/jquery-1.10.2.intellisense.js" 

$(document).ready(function () {
    
    function validateDynamicFormInput(element) {
        var currForm = element.closest("form");
        currForm.removeData("validator");
        currForm.removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(currForm);
        currForm.validate(); // This line is important and added for client side validation to trigger, without this it didn't fire client side errors.
    }

    // dynamically subscribes all remove buttons
    $(document).on('click', '.remove-item', function (event) {
        event.preventDefault()
        $(this).parents('.spare-part-form').remove()
    })

    // Add new spare part button event
    $('#add-spare-part').on('click', function addSparePart(event) {
        event.preventDefault()
        $.get('/Jobs/AddSparePart').done(function (html) {
            $('#spare-parts-list').append(html)
            var form = $('#job-form')
            validateDynamicFormInput( form )
            $.validator.methods.subscribeUIToValidation();
        })
    })
});