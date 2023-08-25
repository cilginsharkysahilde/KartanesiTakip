function GetAdminCategories() {
    Get("Category/AdminCategories", (data) => {
        var html = `<table class="table table-info table-striped table-hover " id="categoryTable">` +
            `<tr class="table-warning text-center fw-bold"><th style="width:250px">Kategori Adı</th><th style="width:250px">Üst Kategori Adı</th><th style="width:50px">Aktif</th><th style="width:150px"></th></tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr  class="text-center">`;
            html += `<td>${arr[i].kategoriAdi}</td><td>${arr[i].ustKategoriAdi}</td><td>${arr[i].aktif}</td>`;
            html += `<td><button type="button" class="btn btn-warning me-3 text-dark" onclick='EditCategory(${arr[i].id},"${arr[i].kategoriAdi}",${arr[i].ustKategoriId},${arr[i].aktif})'>Düzenle <i class="bi bi-pencil"></i></button><button type="button" class="btn btn-danger text-dark" onclick="DeleteCategory(${arr[i].id})">Sil <i class="bi bi-trash3"></i></button></td>`;
            html += `</tr>`
        }
        html += `</table>`;

        $("#divCategories").html(html);
    });
}

function GetCategories() {
    Get("Category/AllCategories", (data) => {
        var arr = data;

        $('#selectTopCategories').empty();
        $.each(arr, function (i, item) {
            $('#selectTopCategories').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });
}

let selectedCategoryId = 0;

function NewCategory() {
    selectedCategoryId = 0;
    $("#inputCategoryName").val("");
    $("#categoryModal").modal("show");
}

function SaveCategory() {
    var category = {
        Id: selectedCategoryId,
        Name: $("#inputCategoryName").val(),
        TopCategoryId: $("#selectTopCategories").find(":selected").val(),
        Active: $("#inputActive").is(":checked"),
        Photo: ""
};
Post("Category/AddCategory", category, (data) => {
    GetAdminCategories();
    $("#categoryModal").modal("hide");
    GetCategories();
})
}

function DeleteCategory(id) {
    Delete(`Category/DeleteCategory?id=${id}`, (data) => {
        GetCategories();
        GetAdminCategories();
    });
}

function EditCategory(id, name, topCategoryId, active) {
    selectedCategoryId = id;
    $("#inputCategoryName").val(name);
    document.getElementById('selectTopCategories').value = topCategoryId;
    if (active) {
        document.getElementById("inputActive").setAttribute("checked", "");
    }
    $("#categoryModal").modal("show");
}

$(document).ready(function () {
    GetCategories();
    GetAdminCategories();
});
