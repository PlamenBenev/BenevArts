$(document).ready(function () {
    // Handle the click event for the add comment button
    $("#addCommentButton").on("click", function () {
        // Get the comment content from the input field
        var commentContent = $("#commentContent").val();
        var assetId = $(this).data('asset-id');
        var writeComment = $("#writeComment");

        // Send AJAX request to add the comment
        $.ajax({
            url: "/Comment/PostComment",
            type: "POST",
            data: {
                content: commentContent,
                assetId: assetId
            },
            success: function (data) {
                // Clear the comment input field
                $("#commentContent").val("");

                if ($("#commentsContainer").has("p").length) {

                    writeComment.remove();
                }

                // Append the new comment to the comments container
                $("#commentsContainer").append(data);
            },
            error: function (error) {
                console.log(error);
            },
            dataType: "html" // Set the data type as HTML
        });
    });
});
