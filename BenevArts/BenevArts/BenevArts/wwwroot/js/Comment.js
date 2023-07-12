$(function () {
	$('#submitComment').click(function () {
		var commentContent = $('#commentText').val();

		// Create an object to send in the AJAX request
		var comment = {
			Content: commentContent
		};

		// Send the AJAX request to the controller action
		$.ajax({
			url: '@Url.Action("PostComment", "Comment")',
			type: 'POST',
			dataType: 'json',
			data: comment,
			success: function (response) {
				// Handle the success response, e.g., update the comments section
				// You can append the new comment to the comments list or reload the page
				// to fetch the updated comments from the server
			},
			error: function (xhr) {
				// Handle the error response
			}
		});
	});
});