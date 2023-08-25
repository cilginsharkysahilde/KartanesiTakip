//Müşterileri Getir
function GetAdminCustomers() {
    Get("Customer/AdminCustomers", (data) => {
        var html = `<table class="table table-info table-striped table-hover font-size-small" id="customerTable">` +
            `<tr class="table-warning text-center fw-bold"><th>Müşteri Adı</th><th>Telefon</th><th>Ev Telefon</th><th style="width:100px">Ürün Adı</th><th style="width:150px">Satış Personeli</th><th>Adres</th><th>Servis</th><th style="width:100px">Kayıt Tarih</th><th style="width:100px">Satış Tarihi</th><th style="width:250px"></th></tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr  class="text-center">`;
            html += `<td>${arr[i].musteriAdi}</td><td>${arr[i].telefon}</td><td>${arr[i].evTelefon}</td><td>${arr[i].urunAdi}</td><td>${arr[i].satisPersoneli}</td><td><button type="button" class="btn btn-secondary font-size-small text-center text-light" onclick="GetAddressesByUserId(${arr[i].musteriId})"">Adresler <i class="bi bi-house-heart"></i></button></td><td><button type="button" class="btn btn-success font-size-small text-center text-light" onclick='GetServicesByUserId(${arr[i].musteriId},"${arr[i].musteriAdi}")'><i class="bi bi-wrench-adjustable me-2"></i>Servis</button></td><td>${arr[i].kayitTarih}</td><td>${arr[i].satisTarih}</td>`;
            html += `<td><button type="button" class="btn btn-warning font-size-small me-3 text-dark" onclick='EditCustomer(${arr[i].musteriId},"${arr[i].musteriAdi}","${arr[i].telefon}","${arr[i].evTelefon}",${arr[i].salespersonId},${arr[i].productTypeId})'>Düzenle <i class="bi bi-pencil"></i></button><button type="button" class="btn btn-danger font-size-small text-dark" onclick="DeleteCustomer(${arr[i].musteriId})">Sil <i class="bi bi-trash3"></i></button></td>`;
            html += `</tr>`
        }
        html += `</table>`;
        $("#divCustomers").html(html);
    });
}

let selectedCustomerId = 0;
let selectedAddressId = 0;
let selectedAddressUserId;
let selectedServiceId = 0;
let selectedServiceUserId;
let selectedServiceUserName;

//Servisleri getir
function GetServicesByUserId(id,customerName) {
    Get(`Service/GetAdminServices?id=${id}`, (data) => {
        var html = `<table class="table table-striped table-hover font-size-small ">` +
            `<tr class="table-warning text-center fw-bold"><th>İşlem Türü</th><th>Bakım Yapıldı mı?</th><th>Ürün Adı</th><th>Parça Açıklaması</th><th>Teslim Tarihi</th><th>Müşteri Şikayeti</th><th>Su Değerleri</th><th>Servis Personeli</th><th>Özel Not</th><th style="width:250px"></th></tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr  class="text-center">`;
            html += `<td>${arr[i].processType}</td><td>${arr[i].isMaintenanceDone}</td><td>${arr[i].productName}</td><td>${arr[i].itemDescription}</td><td>${arr[i].dueDate}</td><td>${arr[i].customerComplaint}</td><td>${arr[i].waterValues}</td><td>${arr[i].servicePersonnelName}</td><td>${arr[i].specialNote}</td>`;
            html += `<td><button type="button" class="btn btn-success me-3 text-light" onclick='EditService(${arr[i].serviceId},${arr[i].customerId},${arr[i].processTypeId},${arr[i].productId},${arr[i].dueDate},"${arr[i].itemDescription}","${arr[i].customerComplaint}","${arr[i].waterValues}",${arr[i].servicePersonnelId},${arr[i].isMaintenanceDone},${arr[i].maintenanceMonthId},${arr[i].maintenancePeriodId},${arr[i].maintenanceYearId},"${arr[i].specialNote}","${arr[i].customerName}")'>Düzenle <i class="bi bi-pencil"></i></button><button type="button" class="btn btn-danger text-dark" onclick="DeleteService(${arr[i].serviceId})">Sil <i class="bi bi-trash3"></i></button></td>`;
            html += `</tr>`
        }
        html += `</table>`;

        selectedServiceUserId = id;
        selectedServiceUserName = customerName;
        $("#divServices").html(html);
        $("#servicesModal").modal("show");
    })
}

//Adresleri Getir

function GetAddressesByUserId(id) {
    Get(`Address/AdminAddressesByUserId?id=${id}`, (data) => {
        var html = `<table class="table table-striped table-hover ">` +
            `<tr class="table-warning text-center fw-bold"><th>Adres Adı</th><th>Adres Açıklaması</th><th>Şehir Adı</th><th>İlçe Adı</th><th>Ülke Adı</th><th>Posta Kodu</th><th style="width:250px"></th></tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr  class="text-center">`;
            html += `<td>${arr[i].adresAdi}</td><td>${arr[i].adresAciklamasi}</td><td>${arr[i].sehirAdi}</td><td>${arr[i].ilceAdi}</td><td>${arr[i].ulkeAdi}</td><td>${arr[i].postaKodu}</td>`;
            html += `<td><button type="button" class="btn btn-warning me-3 text-dark" onclick='EditAddress(${arr[i].id},${arr[i].customerId},"${arr[i].adresAdi}","${arr[i].adresAciklamasi}",${arr[i].sehirId},${arr[i].ilceId},${arr[i].ulkeId},"${arr[i].postaKodu}")'>Düzenle <i class="bi bi-pencil"></i></button><button type="button" class="btn btn-danger text-dark" onclick="DeleteAddress(${arr[i].id})">Sil <i class="bi bi-trash3"></i></button></td>`;
            html += `</tr>`
        }
        html += `</table>`;

        selectedAddressUserId = id;
        $("#divAddresses").html(html);
        $("#addressesModal").modal("show");
    })
}

//SelectBoxları Getir

function GetSelectBox(){
    Get("Customer/GetPersonnels", (data) => {
        var arr = data;

        $('#selectSalesperson').empty();
        $('#selectServicePersonnel').empty();
        $.each(arr, function (i, item) {
            $('#selectSalesperson').append($('<option>', {
                value: item.id,
                text: item.name
            }));
            $('#selectServicePersonnel').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });

    Get("Customer/GetProducts", (data) => {
        var arr = data;

        $('#selectProduct').empty();
        $('#selectServiceProduct').empty();

        $.each(arr, function (i, item) {
            $('#selectProduct').append($('<option>', {
                value: item.id,
                text: item.name
            }));
            $('#selectServiceProduct').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });

    Get("Address/GetCities", (data) => {
        var arr = data;

        $('#selectCity').empty();
        $.each(arr, function (i, item) {
            $('#selectCity').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });

    Get("Address/GetDistricts", (data) => {
        var arr = data;

        $('#selectDistrict').empty();
        $.each(arr, function (i, item) {
            $('#selectDistrict').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });

    Get("Address/GetCountries", (data) => {
        var arr = data;

        $('#selectCountry').empty();
        $.each(arr, function (i, item) {
            $('#selectCountry').append($('<option>', {
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

//Customer

function NewCustomer() {
    selectedCustomerId = 0;
    $("#inputCustomerName").val("");
    $("#inputPhone").val("");
    $("#inputHomePhone").val("");
    $("#customerModal").modal("show");
}

function SaveCustomer() {
    var customer = {
        Id: selectedCustomerId,
        Name: $("#inputCustomerName").val(),
        PhoneNumber: $("#inputPhone").val(),
        HomePhoneNumber: $("#inputHomePhone").val(),
        ProductTypeId: $("#selectProduct").find(":selected").val(),
        SalespersonId: $("#selectSalesperson").find(":selected").val()
    };

    Post("Customer/AddCustomer", customer, (data) => {
        GetSelectBox();
        $("#customerModal").modal("hide");
        GetAdminCustomers();
    })
}

function EditCustomer(musteriId, musteriAdi, telefon, evTelefon, salespersonId, productTypeId) {
    selectedCustomerId = musteriId;
    $("#inputCustomerName").val(musteriAdi);
    $("#inputPhone").val(telefon);
    $("#inputHomePhone").val(evTelefon);
    document.getElementById('selectSalesperson').value = salespersonId;
    document.getElementById('selectProduct').value = productTypeId;
    $("#customerModal").modal("show");

}

function DeleteCustomer(id) {
    Delete(`Customer/DeleteCustomer?id=${id}`, (data) => {
        GetAdminCustomers();
        GetSelectBox();
    });
}

//Address

function NewAddress() {
    selectedAddressId = 0;
    $("#inputAddressName").val("");
    $("#inputDescription").val("");
    $("#inputZipCode").val("");
    $("#addressEditModal").modal("show");
}

function SaveAddress() {
    var address = {
        Id: selectedAddressId,
        CustomerId: selectedAddressUserId,
        Name: $("#inputAddressName").val(),
        Description: $("#inputDescription").val(),
        CountryId: $("#selectCountry").find(":selected").val(),
        CityId: $("#selectCity").find(":selected").val(),
        DistrictId: $("#selectDistrict").find(":selected").val(),
        ZipCode: $("#inputZipCode").val()
    };

    Post("Address/AddAddress", address, (data) => {
        GetSelectBox();
        $("#addressEditModal").modal("hide");
        GetAddressesByUserId(selectedAddressUserId);
    })
}

function EditAddress(id, userId, adresAdi, adresAciklamasi, sehirId, ilceId, ulkeId, postaKodu) {
    selectedAddressId = id;
    selectedAddressUserId = userId;
    $("#inputAddressName").val(adresAdi);
    $("#inputDescription").val(adresAciklamasi);
    document.getElementById('selectCity').value = sehirId;
    document.getElementById('selectDistrict').value = ilceId;
    document.getElementById('selectCountry').value = ulkeId;
    $("#inputZipCode").val(postaKodu);
    $("#addressEditModal").modal("show");

}

function DeleteAddress(id) {
    Delete(`Address/DeleteAddress?id=${id}`, (data) => {
        GetAddressesByUserId(selectedAddressUserId);
        GetSelectBox();
    });
}

//Service

function NewService() {
    selectedServiceId = 0;
    document.getElementById("inputServiceCustomerName").value = selectedServiceUserName;
    $("#inputItemDescription").val("");
    $("#inputCustomerComplaint").val("");
    $("#inputWaterValues").val("");
    document.getElementById("inputSpecialNote2").value = "";
    $("#serviceEditModal").modal("show");
}


function SaveService() {
    var service = {
        Id: selectedServiceId,
        CustomerId: selectedServiceUserId,
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
        GetServicesByUserId(selectedServiceUserId);
    })
}


function EditService(id, customerId, processTypeId, productId, dueDate, itemDescription, customerComplaint, waterValues, servicePersonnelId, isMaintenanceDone, maintenanceMonthId, maintenancePeriodId, maintenanceYearId, specialNote, customerName) {
    selectedServiceId = id;
    selectedServiceUserId = customerId;
    document.getElementById("inputServiceCustomerName").value = selectedServiceUserName;
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
        GetServicesByUserId(selectedServiceUserId);
        GetSelectBox();
    });
}


$(document).ready(function () {
    GetSelectBox();
    GetAdminCustomers();
});
