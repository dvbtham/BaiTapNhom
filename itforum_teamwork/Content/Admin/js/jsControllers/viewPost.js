var post = {
    init: function () {
        post.registerEvent();
    },
    registerEvent: function () {
        $('#viewPost').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Home/myViews",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.response
                    if (response.views) {
                        alert('View +1')
                    }
                    else
                        alert('View 0')
                }
            });
        })
    }
}
post.init();