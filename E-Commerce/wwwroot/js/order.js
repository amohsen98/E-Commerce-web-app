var dtble;

$(document).ready(function () {
    loaddata();
});

function loaddata() {
    dtble = $("#mytable").DataTable({
        "ajax": {
            "url": "/Admin/Order/GetData", 
            "dataSrc": "data"  // Ensure this is correct, or leave out if default
        },
        "columns": [
            { "data": "id" },
            { "data": "name" },
            { "data": "phoneNumber" },
            { "data": "applicationUser.email" },
            { "data": "orderStatus" },
            { "data": "totalPrice" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Admin/Order/Details?orderid= ${data}" class="btn btn-warning">Details</a>
                    `;
                }
            }
        ]
    });
}

// Move DeleteItem function outside of loaddata to ensure it's accessible
function DeleteItem(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {

            // Corrected the AJAX call
            $.ajax({
                url: url,
                type: "Delete",  // Added comma to separate properties
                success: function (data) {
                    if (data.success) {
                        dtble.ajax.reload();
                        toastr.success(data.message);  // Ensure toastr is defined
                    } else {
                        toastr.error(data.message);  // Ensure toastr is defined
                    }
                }
            });
            Swal.fire({
                title: "Deleted!",
                text: "Your file has been deleted.",
                icon: "success"
            });
        }
    });
}
