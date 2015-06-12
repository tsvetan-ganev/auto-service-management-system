/// <reference path="../../Vendor/jquery-2.1.4.js" />
/// <reference path="../../Vendor/jquery-2.1.4.intellisense.js" />

var app = app || {};

app.sparePartNamesAutocompleteSource = ["Alternator", "Alternator bearing", "Alternator bracket", "Alternator fan", "Battery", "Battery cable",
    "Battery plate", "Voltage regulator", "Ammeter", "Clinometer", "Dynamometer", "Fuel gauge", "Hydrometer",
    "Odometer", "Speedometer", "Tachometer", "Voltmeter", "Water temperature meter", "Coil wire", "Distributor",
    "Electronic timing controller", "Ignition box", "Ignition coil", "Ignition magneto",
    "Spark plug", "Fog light", "Halogen", "Headlight", "Interior light", "License plate lamb", "Side lighting", "Tail light", "Airbag sensors",
    "Automatic transmission speed sensor", "Camshaft position sensor", "Crankshaft position sensor", "Engine sensor", "Fuel level sensor",
    "Fuel pressure sensor", "Knock sensor", "Light sensor", "Oil level sensor", "Oil pressure sensor", "O2 sensor", "Mass flow sensor",
    "Starter", "Glowplug", "Door switch", "Ignition switch", "Steering column switch", "Thermostat", "A/C harness", "Engine harness",
    "Interior harness", "Main harness", "Floor harness", "Control harness", "Air bag control module", "Alarm", "Central locking system",
    "Chassis control computer", "Ground strap", "Performance chip", "Performance monitor", "Cruise control computer", "Door contact",
    "Engine computer and management system", "Engine control unit", "Fuse", "Fuse box", "Transmission computer", "ABS", "Bleed nipple",
    "Brake backing plate", "Brake backing pad", "Brake disc", "Brake drum", "Brake pad", "Brake pedal", "Brake piston", "Brake pump",
    "Brake roll", "Brake roll", "Brake rotor", "Brake servo", "Brake shoe", "Brake warning light", "Caliper", "Hold-down springs", "Hose",
    "Brake booster hose", "Hydraulic booster unit", "Load-sensing valve", "Master cylinder", "Metering valve", "Park brake lever",
    "Pressure differential valve", "Proportioning valve", "Reservoir", "Shoe return spring", "Tyre", "Vacuum brake booster", "Wheel cylinder",
    "Wheel stud", "Engine", "Air duct", "Air intake housing", "Air intake manifold", "Camshaft", "Connecting rod", "Crank case", "Crank pulley",
    "Crankshaft", "Crankshaft oil seal", "Cylinder head", "Cylinder head cover", "Cylinder head gasket", "Distributor cap", "Drive belt",
    "Engine block", "Engine cradle", "Engine shake damper", "Engine valve", "Gudgeon pin", "Harmonic balancer", "Heater", "Mounting", "Piston",
    "Poppet valve", "PCV valve", "Pulley", "Rocker arm", "Starter motor", "Turbocharger", "Tappet", "Timing tape", "Timing belt", "Valve cover",
    "Water pump pulley", "Air blower", "Coolant hose", "Cooling fan", "Fan blade", "Fan clutch", "Radiator", "Water neck", "Water pipe",
    "Water tank", "Water pump", "Oil filter", "Oil pump", "Catalytic converter", "Exhaust clamp and bracket", "Exhaust flange gasket",
    "Exhaust gasket", "Exhaust manifold", "Exhaust pipe", "Muffler", "Spacer ring", "Air filter", "Carburetor", "Fuel pump", "Chocke cable",
    "EGR valve", "Fuel cooler", "Fuel filter", "Fuel injector", "Fuel pump", "Fuel pressure regulator", "Intake manifold", "Fuel rail",
    "LPG system", "Throttle body"];

app.subscribeInputsToAutocomplete = function(selector, autocompleteSource) {
  var inputNames = [],
    autocompletes = [],
    $inputs = $(selector),
    inputSelector,
    i;

  $.each($inputs, function (i, val) {
    inputNames.push($($inputs[i]).attr("name"));
  });

  for (i = 0; i < inputNames.length; i++) {
    inputSelector = 'input[name="' + inputNames[i] + '"]';
    autocompletes.push(new autoComplete({
      selector: inputSelector,
      minChars: 1,
      source: function (term, suggest) {
        term = term.toLowerCase();
        var choices = autocompleteSource,
            matches = [];
        for (var i = 0; i < choices.length; i++)
          if (~choices[i].toLowerCase().indexOf(term)) matches.push(choices[i]);
        suggest(matches);
      }
    }));
  }
}

app.validateDynamicFormInput = function(element) {
  var currForm = element.closest("form");
  currForm.removeData("validator");
  currForm.removeData("unobtrusiveValidation");
  $.validator.unobtrusive.parse(currForm);
  currForm.validate(); // This line is important and added for client side validation to trigger, without this it didn't fire client side errors.
};

app.subscribeInsertedInputToAutocomplete = function (inputSelector, autocompleteSource) {
  var autocompleteInput = new autoComplete({
    selector: inputSelector,
    minChars: 1,
    source: function (term, suggest) {
      term = term.toLowerCase();
      var choices = autocompleteSource,
          matches = [];
      for (var i = 0; i < choices.length; i++)
        if (~choices[i].toLowerCase().indexOf(term)) matches.push(choices[i]);
      suggest(matches);
    }
  })
}

app.addSparePartForm = function(event) {
  event.preventDefault();
  $.get('/Jobs/AddSparePart').done(function (html) {
    $('#spare-parts-list').append(html);

    var jobForm = $('#job-form'),
        lastSparePartForm = $('.spare-part-name-input').filter(':last'),
        inputSelector = 'input[name="' + $(lastSparePartForm).attr("name") + '"]';
    app.subscribeInsertedInputToAutocomplete(inputSelector, app.sparePartNamesAutocompleteSource);
    app.validateDynamicFormInput(jobForm);
  });
}

app.getTotal = function () {
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
    if (quantities[i] > 0 && prices[i] > 0) {
      total += quantities[i] * prices[i];
    }
  }

  $total.innerHTML = total.toFixed(2);
};

app.removeSparePartForm = function (event) {
  event.preventDefault();
  $(this).parents('.spare-part-form').remove();
  app.getTotal();
}