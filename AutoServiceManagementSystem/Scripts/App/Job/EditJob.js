/// <reference path="../../Vendor/jquery-1.10.2.js" />
/// <reference path="../../Vendor/jquery-1.10.2.intellisense.js" 

$(document).ready(function () {
  // dynamically subscribes all remove buttons
  $(document).on('click', '.remove-item', function (event) {
    event.preventDefault()
    $(this).parents('.spare-part-form').remove()
  });

  // Add new spare part button event
  $('#add-spare-part').on('click', app.addSparePartForm);

  function validateDynamicFormInput(element) {
    var currForm = element.closest("form");
    currForm.removeData("validator");
    currForm.removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(currForm);
    // This line is important and added for client side validation to trigger,
    // without this it didn't fire client side errors.
    currForm.validate();
  }

  $(document).on('click load', '.spare-part-form', app.getTotal);
});