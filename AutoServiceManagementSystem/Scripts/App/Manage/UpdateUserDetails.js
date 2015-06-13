/// <reference path="../../Vendor/jquery-2.1.4.js" />

$(function () {
  var displaySuccessMessage = function () {
    var msg = document.getElementById('update-on-success');
    $(msg).show('slow').hide(4000);
  }

  var displayErrorMessage = function () {
    var msg = document.getElementById('update-on-error');
    $(msg).show('slow').hide(4000);
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

});