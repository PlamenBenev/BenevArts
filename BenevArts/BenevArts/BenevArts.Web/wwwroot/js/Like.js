$(document).ready(function () {
    $('#likeButton').on('click', function () {
        var assetId = $(this).data('asset-id');
        var isLiked = $(this).data('liked');

        // Send AJAX request to toggle the like status
        $.ajax({
            url: '/Asset/ToggleLike',
            type: 'POST',
            data: {
                assetId: assetId,
                isLiked: isLiked
            },
            success: function (response) {
                // Handle the response and update the UI accordingly
                if (response.success) {
                    if (response.isLiked) {
                        // User liked the asset, update the button text and UI
                        $('#likeButton').text('Unlike');
                        $('#likeButton').data('liked', true);
                    } else {
                        // User unliked the asset, update the button text and UI
                        $('#likeButton').text('Like');
                        $('#likeButton').data('liked', false);
                    }
                }
                $('#likeCount').text(response.likeCount);
            },
            error: function (xhr, status, error) {
                // Handle error if any
                console.error(error);
            }
        });
    });
});
