/// <reference path="userController.js" />
var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();

            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/User/ChangeStatusAjax",
                data: { id: id },
                dataType: "json",
                type:"POST",
                success: function (response) {
                    console.response
                    if (response.status) {
                        btn.text("Khóa")
                    }
                    else
                        btn.text("Mở khóa")
                }
            });
        });
    }
}
user.init();
