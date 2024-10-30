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
        },
    });
}
$(document).ready(function () {
    $('#createForm').on('submit', function (e) {
        e.preventDefault(); // Отменяем стандартное поведение формы

        var formData = $(this).serialize(); // Сериализуем данные формы console.log(formData); // Выводим данные в консоль для отладки $.ajax({
        type; 'POST',
            url; '@Url.Action("Create", "Adjustments")',
            data; formData,
                success; function (result) {
                        window.location.href = '@Url.Action("Index", "Adjustments")'; // Перенаправление на страницу Index },
                        error: function (xhr, status, error) {
                            console.error(error); // Выводим ошибку в консоль
                            alert('Произошла ошибка при создании заявки.');
                        }
        });
    $(document).ready(function () {
        $('#editForm').on('submit', function (e) {
            e.preventDefault(); // Отменяем стандартное поведение формы

            var formData = $(this).serialize(); // Сериализуем данные формы console.log(formData); // Выводим данные в консоль для отладки $.ajax({
            type; 'POST',
                url; '@Url.Action("Edit", "Adjustments")',
                data; formData,
                    success; function (result) {
                            window.location.href = '@Url.Action("Index", "Adjustments")'; // Перенаправление на страницу Index },
                        error; function (xhr, status, error) {
                                console.error(error); // Выводим ошибку в консоль
                                alert('Произошла ошибка при создании заявки.');
                            }
                        });
});
});

