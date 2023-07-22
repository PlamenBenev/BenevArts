function loadApplicationsByStateFilter() {
    var selectedState = $('#applicationStateFilter').val();
    var actionUrl = '/Seller/GetApplicationsByState'; // Update the URL according to your controller and action
    $.ajax({
        url: actionUrl,
        type: 'GET',
        data: { state: selectedState },
        success: function (data) {
            $('#applicationsList').html(data);
        },
        error: function () {
            $('#applicationsList').html('<p>Error loading applications.</p>');
        }
    });
}

$(function () {
    // Load all applications initially
    loadApplicationsByStateFilter();

    // Handle the selection change event of the state filter
    $('#applicationStateFilter').on('change', function () {
        loadApplicationsByStateFilter();
    });
});
