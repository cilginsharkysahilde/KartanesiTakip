function GetProducts() {
    Get("Product/AllProducts", (data) => {
        var html = `<table class="table table-info table-striped table-hover" id="productTable">` +
            `<tr class="table-warning text-center"><th>Ürün Adı</th><th>Stok Kodu</th><th>Kritik Sayısı</th><th>Aktif</th><th style="width:250px"></th></tr>`;

        var arr = data;


        for (var i = 0; i < arr.length; i++) {
            html += `<tr  class="text-center">`;
            html += `<td>${arr[i].name}</td><td>${arr[i].stockCode}</td><td>${arr[i].criticalNumber}</td><td>${arr[i].active}</td>`;
            html += `<td><button type="button" class="btn btn-warning font-size-small me-3 text-dark" onclick='EditProduct(${arr[i].id},"${arr[i].name}","${arr[i].stockCode}","${arr[i].criticalNumber}",${arr[i].active})'>Düzenle <i class="bi bi-pencil"></i></button><button type="button" class="btn btn-danger font-size-small text-dark" onclick="DeleteProduct(${arr[i].id})">Sil <i class="bi bi-trash3"></i></button></td>`;
            html += `</tr>`
        }
        html += `</table>`;
        $("#divProducts").html(html);
    });
}

let selectedProductId = 0;

function DeleteProduct(id) {
    Delete(`Product/DeleteProduct?id=${id}`, (data) => {
        GetProducts();
    });
}


function NewProduct() {
    selectedProductId = 0;
    $("#inputProductName").val("");
    $("#inputStockCode").val("");
    $("#inputCriticalNumber").val("");
    $("#productModal").modal("show");

}

function SaveProduct() {
    var product = {
        Id: selectedProductId,
        Name: $("#inputProductName").val(),
        StockCode: $("#inputStockCode").val(),
        CriticalNumber: $("#inputCriticalNumber").val(),
        Active: $("#inputActive").is(":checked")
    };

    Post("Product/AddProduct", product, (data) => {
        GetProducts();
        $("#productModal").modal("hide");

    });

}

function EditProduct(id, name, stockCode, criticalNumber, active) {
    selectedProductId = id;
    $("#inputProductName").val(name);
    $("#inputStockCode").val(stockCode);
    $("#inputCriticalNumber").val(criticalNumber);
    if (active) {
        document.getElementById("inputActive").setAttribute("checked", "");
    }
    $("#productModal").modal("show");
}

$(document).ready(function () {
    GetProducts();
});
