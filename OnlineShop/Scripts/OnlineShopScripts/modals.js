$(document).ready(function () {

    $('.openDialog-Add').click(function (event) {
        event.preventDefault();
        $.get(this.href, function (response) {
            $('.divForAdd').html(response);
          
        });
        $('#addmodel').modal({
            backdrop: 'static',
        
        }, 'show');
    });
    $('.openDialog-Edit').click(function (event) {
        event.preventDefault();
        $.get(this.href, function (response) {
            $('.divForEdit').html(response);
        });
        $('#editmodel').modal({
            backdrop: 'static',
           
        }, 'show');
    });
    $('.openDialog-Detail').click(function (event) {
        event.preventDefault()
        $.get(this.href, function (response) {
            $('.divForDetail').html(response);
        });
        $('#detailmodel').modal({
            backdrop: 'static',
        }, 'show');
    });
    $('.openDialog-Delete').click(function (event) {
        event.preventDefault();
        $.get(this.href, function (response) {
            $('.divForDelete').html(response);
        });
        $('#deletemodel').modal({
            backdrop: 'static',
        }, 'show');
    });

    $('.openDialog-Cart').click(function (event) {
        event.preventDefault();
        $.get(this.href, function (response) {
            $('.divForCart').html(response);
        });
        $('#cartmodel').modal({
            backdrop: 'static',
        }, 'show');
    });

    $('.openDialog-RemoveFromCart').click(function (event) {
        event.preventDefault();
        $.get(this.href, function (response) {
            $('.divForRemoveFromCart').html(response);
        });
        $('#removeFromCartModel').modal({
            backdrop: 'static',
        }, 'show');
    });
});
