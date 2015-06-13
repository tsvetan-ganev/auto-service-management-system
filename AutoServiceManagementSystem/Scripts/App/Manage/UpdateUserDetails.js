/// <reference path="../../Vendor/jquery-2.1.4.js" />

//$(function () {
  var displaySuccessMessage = function() {
    var msg = document.getElementById('update-on-success');
    $(msg).toggleClass('hidden');
  }

  var displayErrorMessage = function () {
    var msg = document.getElementById('update-on-error');
    $(msg).toggleClass('hidden');
  }

  $(document.getElementById('submit-btn')).click(function (event) {
    $.ajax({
      url: "Manage/_EditUserDetails",
      method: "POST",
      data: $("#user-details").serialize()
    })
      .success(displaySuccessMessage)
      .error(displayErrorMessage)
  });
