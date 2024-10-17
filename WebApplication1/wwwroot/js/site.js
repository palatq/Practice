// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function changeButtonColor() {
    $.ajax({
        type: "POST",
        url: '@Url.Action("ChangeButtonColor", "Home")', // Указываем URL для POST-запроса
        success: function (response) {
            $('#myButton').css('background-color', response.color); // Изменяем цвет кнопки
            $('#myButton').css('color', 'white'); // Изменяем цвет текста, если нужно }
        });
}