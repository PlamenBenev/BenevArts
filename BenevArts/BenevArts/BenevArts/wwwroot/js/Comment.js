$(document).ready(function () {
    // Handle the click event for the add comment button
    $("#addCommentButton").on("click", function () {
        // Get the comment content from the input field
        var commentContent = $("#commentContent").val();
        var assetId = $(this).data('asset-id');

        // Send AJAX request to add the comment
        $.ajax({
            url: "/Comment/PostComment",
            type: "POST",
            data: {
                content: commentContent,
                assetId: assetId
            },
            success: function (data) {
                // Update the comment list on the page
                $("#commentsList").append(`<li>${data.content}</li>`);
            },
            error: function (error) {
                console.log(error);
            },
        });
    });
});
