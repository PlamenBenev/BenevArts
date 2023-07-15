$(document).ready(function () {
    $('#favoriteButton').on('click', function () {
        var assetId = $(this).data('asset-id');
        var isFavorited = $(this).data('favorited');

        // Send AJAX request to toggle the like status
        $.ajax({
            url: '/Favorite/ToggleFavorite',
            type: 'POST',
            data: {
                assetId: assetId,
                isFavorited: isFavorited
            },
            success: function (response) {
                // Handle the response and update the UI accordingly
                if (response.success) {
                    if (response.isFavorited) {
                        // User liked the asset, update the button text and UI
                        $('#favoriteButton').text('Remove from Favorites');
                        $('#favoriteButton').data('favorited', true);
                    } else {
                        // User unliked the asset, update the button text and UI
                        $('#favoriteButton').text('Add to Favorites');
                        $('#favoriteButton').data('favorited', false);
                    }
                }
            },
            error: function (xhr, status, error) {
                // Handle error if any
                console.error(error);
            }
        });
    });
});
