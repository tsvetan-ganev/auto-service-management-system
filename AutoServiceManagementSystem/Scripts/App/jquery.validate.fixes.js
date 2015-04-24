/// <reference path="../Vendor/jquery-1.10.2.js" />
$.validator.methods.range = function ( value, element, param )
{
  var globalizedValue = value.replace( ",", "." )
  return this.optional( element ) || ( globalizedValue >= param[0] && globalizedValue <= param[1] )
}

$.validator.methods.number = function ( value, element )
{
  return this.optional( element ) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test( value )
}

console.log( $( 'input' ).length )

$.validator.methods.subscribeUIToValidation = function subscribeUIToValidation()
{
  $( 'input[type="text"], input[type="number"]' ).on( 'keyup keypress blur click change', function toggleErrorClass()
  {

    var errors = $( '.input-validation-error' ).offsetParent()
                .removeClass( 'has-success' )
                .addClass( 'has-error' )

    var valid = $( '.valid' ).offsetParent()
                .removeClass( 'has-error' )
                .addClass( 'has-success' )

    console.log('validation event occured')
  } )
}

$.validator.methods.subscribeUIToValidation();

