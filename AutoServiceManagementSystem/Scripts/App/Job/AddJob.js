/// <reference path="../../Vendor/jquery-2.1.4.js" />
/// <reference path="../../Vendor/jquery-2.1.4.intellisense.js" /> 
/// <reference path="../app.js" />

$(document).ready(function () {

  app.subscribeInputsToAutocomplete('.spare-part-name-input', app.sparePartNamesAutocompleteSource);

  // dynamically subscribes all remove item buttons
  $(document).on('click', '.remove-item', app.removeSparePartForm);

  // Add new spare part button event
  $('#add-spare-part').on('click', app.addSparePartForm);

  $(document).on('click blur', '.spare-part-form', app.getTotal);
});