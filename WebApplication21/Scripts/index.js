$(() => {

    $("#button").on('click', function () {
        console.log("button clicked")
        $.post('/Home/Like', { Id: $(this).data('image-id') }, function () {
        });
    });

    setInterval(function () {
        $.get('/Home/GetLikes', { Id: $("#button").data('image-id') }, function (count) {
            $("#likeCount").text(count);
        });
    }, 1000);
});
