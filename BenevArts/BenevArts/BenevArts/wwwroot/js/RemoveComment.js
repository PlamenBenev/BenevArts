// RemoveComment.js

function removeComment(commentId) {
    // Send AJAX request to remove the comment
    $.ajax({
        url: "/Comment/RemoveComment",
        type: "POST",
        data: {
            commentId: commentId
        },
        success: function (response) {
            // Remove the comment element from the DOM
            var commentElement = $('[data-comment-id="' + commentId + '"]');
            commentElement.remove();
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}
