/// <reference path="../../Vendor/jquery-1.10.2.js" />
/// <reference path="../../Vendor/jquery-1.10.2.intellisense.js" 

$(document).ready(function () {
    
    // dynamically subscribes all remove buttons
    $(document).on('click', '.remove-item', function (event) {
        event.preventDefault()
        console.log('removed')
        $(this).parents('.spare-part-form').remove()
    })

    // Add new spare part button event
    $('#add-spare-part').on('click', function addSparePart(event) {
        event.preventDefault()
        $.get('/Jobs/AddSparePart').done(function (html) {
            $('#spare-parts-list').append(html)
        })
    })
});