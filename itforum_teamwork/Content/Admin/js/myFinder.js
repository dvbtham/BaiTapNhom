
$(document).ready(function () {
    $('li #btnKhoAnh').on('click', function (e) {
        e.preventDefault();
        var finder = new CKFinder();
        finder.show();
    });
});
