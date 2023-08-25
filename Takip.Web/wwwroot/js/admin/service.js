function GetServices() {
    Get("Service/GetAllAdminServices", (data) => {
        var html = `<table class="table table-info table-striped table-hover font-size-small" id="serviceTable">` +
            `<tr class="table-warning text-center fw-bold"><th>Müşteri Adı</th><th>İşlem Türü</th><th>Bakım Yapıldı mı?</th><th>Ürün Adı</th><th>Parça Açıklaması</th><th>Teslim Tarihi</th><th>Müşteri Şikayeti</th><th>Su Değerleri</th><th>Servis Personeli</th><th>Özel Not</th><th style="width:250px"></th></tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr class="text-center">`;
            html += `<td>${arr[i].customerName}</td><td>${arr[i].processType}</td><td>${arr[i].isMaintenanceDone}</td><td>${arr[i].productName}</td><td>${arr[i].itemDescription}</td><td>${arr[i].dueDate}</td><td>${arr[i].customerComplaint}</td><td>${arr[i].waterValues}</td><td>${arr[i].servicePersonnelName}</td><td>${arr[i].specialNote}</td>`;
            html += `<td><button type="button" class="btn btn-warning me-3 text-dark" onclick='EditService(${arr[i].serviceId},${arr[i].customerId},"${arr[i].customerName}",${arr[i].processTypeId},${arr[i].productId},${arr[i].dueDate},"${arr[i].itemDescription}","${arr[i].customerComplaint}","${arr[i].waterValues}",${arr[i].servicePersonnelId},${arr[i].isMaintenanceDone},${arr[i].maintenanceMonthId},${arr[i].maintenancePeriodId},${arr[i].maintenanceYearId},"${arr[i].specialNote}","${arr[i].customerName}")'>Düzenle <i class="bi bi-pencil"></i></button><button type="button" class="btn btn-danger text-dark" onclick="DeleteService(${arr[i].serviceId})">Sil <i class="bi bi-trash3"></i></button></td>`;
            html += `</tr>`
        }
        html += `</table>`;
        $("#divServices").html(html);
        $("#servicesModal").modal("show");
    })
}

function GetSelectBox() {
    Get("Customer/GetPersonnels", (data) => {
        var arr = data;

        $('#selectServicePersonnel').empty();
        $.each(arr, function (i, item) {
            $('#selectServicePersonnel').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });

    Get("Customer/GetCustomers", (data) => {
        var arr = data;

        $('#inputServiceCustomerName').empty();
        $.each(arr, function (i, item) {
            $('#inputServiceCustomerName').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });

    Get("Customer/GetProducts", (data) => {
        var arr = data;

        $('#selectServiceProduct').empty();

        $.each(arr, function (i, item) {
            $('#selectServiceProduct').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });

    Get("Service/GetProcessTypes", (data) => {
        var arr = data;

        $('#selectProcessType').empty();
        $.each(arr, function (i, item) {
            $('#selectProcessType').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });
}

let selectedServiceId = 0;
let selectedServiceUserId;

function NewService() {
    selectedServiceId = 0;
    document.getElementById("inputServiceCustomerName").removeAttribute("disabled", "readonly");
    document.getElementById("inputServiceCustomerName").value = "";
    $("#inputItemDescription").val("");
    $("#inputCustomerComplaint").val("");
    $("#inputWaterValues").val("");
    document.getElementById("inputSpecialNote2").value = "";
    $("#serviceEditModal").modal("show");
}


function SaveService() {
    var service = {
        Id: selectedServiceId,
        CustomerId: $("#inputServiceCustomerName").find(":selected").val(),
        ProcessTypeId: $("#selectProcessType").find(":selected").val(),
        ProductId: $("#selectServiceProduct").find(":selected").val(),
        ItemDescription: $("#inputItemDescription").val(),
        CustomerComplaint: $("#inputCustomerComplaint").val(),
        WaterValues: $("#inputWaterValues").val(),
        ServicePersonnelId: $("#selectServicePersonnel").find(":selected").val(),
        IsMaintenanceDone: $("#inputIsMaintenanceDone").is(":checked"),
        MaintenancePeriodId: null,
        MaintenanceMonthId: null,
        MaintenanceYearId: null,
        SpecialNote: document.getElementById("inputSpecialNote2").value
    };

    Post("Service/AddService", service, (data) => {
        GetSelectBox();
        $("#serviceEditModal").modal("hide");
        GetServices();
    })
}


function EditService(id, customerId, customerName, processTypeId, productId, dueDate, itemDescription, customerComplaint, waterValues, servicePersonnelId, isMaintenanceDone, maintenanceMonthId, maintenancePeriodId, maintenanceYearId, specialNote, customerName) {
    selectedServiceId = id;
    selectedServiceUserId = customerId;
    document.getElementById("inputServiceCustomerName").setAttribute("disabled", "readonly");
    document.getElementById("inputServiceCustomerName").value = customerId;
    document.getElementById('selectProcessType').value = processTypeId;
    document.getElementById('selectServiceProduct').value = productId;
    $("#inputItemDescription").val(itemDescription);
    $("#inputCustomerComplaint").val(customerComplaint);
    $("#inputWaterValues").val(waterValues);
    document.getElementById('selectServicePersonnel').value = servicePersonnelId;
    document.getElementById("inputSpecialNote2").value = specialNote;
    if (isMaintenanceDone) {
        document.getElementById("inputIsMaintenanceDone").setAttribute("checked", "");
    }
    $("#serviceEditModal").modal("show");

}

function DeleteService(id) {
    Delete(`Service/DeleteService?id=${id}`, (data) => {
        GetServices();
        GetSelectBox();
    });
}

$(document).ready(function () {
    GetSelectBox();
    GetServices();
});
