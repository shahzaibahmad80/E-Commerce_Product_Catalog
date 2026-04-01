function loadProducts(page = 1) {

    let filter = {
        Name: $("#filterName").val(),
        Price: $("#filterPrice").val(),
        CategoryId: $("#filterCategory").val(),
        Page: page,
        PageSize: 10
    };

    $.post("/Product/LoadProducts", filter, function (html) {
        $("#productListContainer").html(html);
    });
}

loadProducts(); // auto-load on page start

// ---------------- VIEW ----------------

function viewProduct(id) {
    $.get("/Product/ViewProduct/" + id, function (html) {
        $("#modalContainer").html(html);
    });
}

// ---------------- EDIT ----------------

function editProduct(id) {
    $.get("/Product/EditProduct/" + id, function (html) {
        $("#modalContainer").html(html);
    });
}

function updateProduct() {
    let form = $("#editForm");

    $.post("/Product/UpdateProduct", form.serialize(), function (res) {
        if (res.success) {
            alert("Updated Successfully");
            closeModal();
            loadProducts();
        } else {
            alert("Error updating product");
        }
    });
}

// ---------------- DELETE ----------------

function deleteProduct(id) {
    $.get(`/Product/DeleteProduct?id=${id}`, function (html) {
        $("#modalContainer").html(html);
    });
}

function confirmDelete(id) {
    $.post('/Product/DeleteProductConfirmed', { id: id }, function (res) {
        if (res.success) {
            alert("Deleted Successfully");
            closeModal();
            loadProducts();
        } else {
            alert("Error deleting");
        }
    });
}

// ---------------- MODAL CLOSE ----------------

function closeModal() {
    $("#modalContainer").html("");
}