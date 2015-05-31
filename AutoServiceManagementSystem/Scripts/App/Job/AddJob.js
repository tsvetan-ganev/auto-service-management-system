/// <reference path="../../Vendor/jquery-2.1.4.js" />
/// <reference path="../../Vendor/jquery-2.1.4.intellisense.js" /> 

$(document).ready(function () {

  function validateDynamicFormInput(element) {
    var currForm = element.closest("form");
    currForm.removeData("validator");
    currForm.removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(currForm);
    currForm.validate(); // This line is important and added for client side validation to trigger, without this it didn't fire client side errors.
  }

  // dynamically subscribes all remove item buttons
  $(document).on('click', '.remove-item', function (event) {
    event.preventDefault();
    $(this).parents('.spare-part-form').remove();
    getTotal();
  });

  // Add new spare part button event
  $('#add-spare-part').on('click', function addSparePart(event) {
    event.preventDefault();
    $.get('/Jobs/AddSparePart').done(function (html) {
      $('#spare-parts-list').append(html);
      var form = $('#job-form');
      validateDynamicFormInput(form);
    });
  });

  // test
  //$.ajax({
  //  url: 'GetSupplierDiscountById',
  //  method: 'GET',
  //  datatype: 'json',
  //  cache: false,
  //  data: { supplierId: 2 }
  //}).done(function (data) {
  //  console.log(data);
  //}).error(function (err) {
  //  console.log(err.statusText);
  //  console.log(err.statusCode);
  //});


  function getTotal() {
    var i,
      $total = document.getElementById('total'),
      $quantities = $('.spare-part-form > .spare-part-quantity input'),
      $prices = $('.spare-part-form > .spare-part-price input'),
      quantities = [],
      prices = [],
      total = 0.0;

    $quantities.each(function () {
      quantities.push(parseInt($(this).val()));
    });

    $prices.each(function () {
      prices.push(parseFloat($(this).val()));
    });

    for (i = 0; i < prices.length; i++) {
      total += quantities[i] * prices[i];
    }

    console.log(total);
    $total.innerHTML = total.toFixed(2);
  }

  $(document).on('click', '.spare-part-form', getTotal);


});