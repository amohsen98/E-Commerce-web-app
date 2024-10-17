var dtble;

$(document).ready(function () {
    loaddata();
});

function loaddata() {
    dtble = $("#mytable").DataTable({
        "ajax": {
            "url": "/Admin/Product/GetData",
            "dataSrc": "data"  // Ensure this is correct, or leave out if default
        },
        "columns": [
            { "data": "name" },
            { "data": "description" },
            { "data": "price" },
            { "data": "category.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <a href="/Admin/Product/Edit/${data}" class="btn btn-success">Edit</a>
                        <a onclick="DeleteItem('/Admin/Product/Delete/${data}')" class="btn btn-danger">Delete</a>
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
