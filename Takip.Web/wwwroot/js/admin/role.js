function GetRoles() {
    Get("Role/AllRoles", (data) => {
        var html = `<table class="table table-info table-striped table-hover" id="roleTable">` +
                    `<tr class="table-warning text-center"><th style="width:50px">Id</th><th style="width:250px">Rol Adı</th><th style="width:150px"></th></tr>`;

                var arr = data;

                for (var i = 0; i < arr.length; i++) {
                    html += `<tr class="text-center">`;
                    html += `<td>${arr[i].id}</td><td>${arr[i].name}</td>`;
                    html += `<td><button type="button" class="btn btn-warning me-3 text-dark" onclick='EditRole(${arr[i].id},"${arr[i].name}")'>Düzenle <i class="bi bi-pencil"></i></button><button type="button" class="btn btn-danger text-dark" onclick="DeleteRole(${arr[i].id})">Sil <i class="bi bi-trash3"></i></button></td>`;
                    html += `</tr>`
                }
                html += `</table>`;

                $("#divRoles").html(html);
    });
}

let selectedRoleId = 0;

function NewRole() {
    selectedRoleId = 0;
    $("#inputRoleName").val("");
    $("#roleModal").modal("show");

}

function SaveRole() {
    var role = {
        Id:selectedRoleId,
        Name:$("#inputRoleName").val()
    };

    Post("Role/AddRole", role, (data) => {
        GetRoles();
        $("#roleModal").modal("hide");

    });
    
}



function DeleteRole(id) {
    Delete(`Role/DeleteRole?id=${id}`, (data) => {
        GetRoles();
    });
}

function EditRole(id, name) {
    selectedRoleId = id;
    $( "#inputRoleName" ).val(name);
    $("#roleModal").modal("show");
}

$(document).ready(function () {
    GetRoles();
});
