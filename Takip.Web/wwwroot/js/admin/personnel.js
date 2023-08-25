function GetPersonnels() {
    Get("Personnel/PersonnelDetails", (data) => {
        var html = `<table class="table table-info table-striped table-hover " id="personnelTable">` +
            `<tr class="table-warning text-center fw-bold"><th>Personel Adı</th><th>Rol</th><th>Başlama Tarihi</th><th style="width:250px"></th></tr>`;

        var arr = data;

        for (var i = 0; i < arr.length; i++) {
            html += `<tr  class="text-center">`;
            html += `<td>${arr[i].personnelName}</td><td>${arr[i].role}</td><td>${arr[i].startDate}</td>`;
            html += `<td><button type="button" class="btn btn-warning me-3 text-dark" onclick='Detail(${arr[i].id},"${arr[i].personnelName}",${arr[i].roleId},"${arr[i].tcNumber}","${arr[i].eMail}","${arr[i].password}","${arr[i].phoneNumber}",${arr[i].maritalStatusId},${arr[i].educationId},"${arr[i].startDate}","${arr[i].quitDate}","${arr[i].salary}",${arr[i].active})'>Detay <i class="bi bi-clipboard2-heart"></i></button><button type="button" class="btn btn-danger text-dark" onclick="DeletePersonnel(${arr[i].id})">Sil <i class="bi bi-trash3"></i></button></td>`;
            html += `</tr>`
        }
        html += `</table>`;

        $("#divPersonnels").html(html);
    });
}

function GetSelectBox() {
    Get("Personnel/GetRoles", (data) => {
        var arr = data;

        $('#selectRole').empty();
        $.each(arr, function (i, item) {
            $('#selectRole').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });

    Get("Personnel/GetMaritalStatuses", (data) => {
        var arr = data;

        $('#selectMaritalStatus').empty();

        $.each(arr, function (i, item) {
            $('#selectMaritalStatus').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });

    Get("Personnel/GetEducations", (data) => {
        var arr = data;

        $('#selectEducation').empty();
        $.each(arr, function (i, item) {
            $('#selectEducation').append($('<option>', {
                value: item.id,
                text: item.name
            }));
        });
    });
}


let selectedPersonnelId = 0;

function NewPersonnel() {
    selectedPersonnelId = 0;
    $("#inputPersonnelName").val("");
    $("#inputTcNumber").val("");
    $("#inputEmail").val("");
    $("#inputPassword").val("");
    $("#inputPhoneNumber").val("");
    $("#inputStartDate").val("");
    $("#inputQuitDate").val("");
    $("#inputSalary").val("");
    $("#personnelModal").modal("show");
}

function SavePersonnel() {
    var personnel = {
        Id: selectedPersonnelId,
        Name: $("#inputPersonnelName").val(),
        RoleId: $("#selectRole").find(":selected").val(),
        TcNumber: $("#inputTcNumber").val(),
        EMail: $("#inputEmail").val(),
        Password: $("#inputPassword").val(),
        PhoneNumber: $("#inputPhoneNumber").val(),
        MaritalStatusId: $("#selectMaritalStatus").find(":selected").val(),
        EducationalBackgroundId: $("#selectEducation").find(":selected").val(),
        StartDate: null,
        QuitDate: null,
        Salary: $("#inputSalary").val(),
        Active: $("#inputActive").is(":checked")
    };
    Post("Personnel/AddPersonnel", personnel, (data) => {
        GetPersonnels();
        $("#personnelModal").modal("hide");
    })
}

function DeletePersonnel(id) {
    Delete(`Personnel/DeletePersonnel?id=${id}`, (data) => {
        GetPersonnels();
    });
}

function Detail(id, personelName, roleId, tcNumber, eMail, password, phoneNumber, maritalStatusId, educationId, startDate, quitDate, salary, active) {
    selectedPersonnelId = id;
    $("#inputPersonnelName").val(personelName);
    $("#inputTcNumber").val(tcNumber);
    $("#inputEmail").val(eMail);
    $("#inputPassword").val(password);
    $("#inputPhoneNumber").val(phoneNumber);
    document.getElementById('selectRole').value = roleId;
    document.getElementById('selectMaritalStatus').value = maritalStatusId;
    document.getElementById('selectEducation').value = educationId;
    $("#inputStartDate").val(startDate);
    $("#inputQuitDate").val(quitDate);
    $("#inputSalary").val(salary);
    if (active) {
        document.getElementById("inputActive").setAttribute("checked", "");
    }
    $("#personnelModal").modal("show");
}

$(document).ready(function () {
    GetSelectBox();
    GetPersonnels();
});
