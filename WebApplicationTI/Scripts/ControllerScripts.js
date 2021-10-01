//SCRIPT PARA ABRIR LOS MODALS USUARIO
function OpenEditModalUser(id) {
    var data = { idUser: id };
    $.ajax(
        {
            type: 'GET',
            url: '/Usuarios/Edit',
            contentType: 'application/json; charset=utf=8',
            data: data,
            success: function (result) {
                $('#modal-edit-content').html(result);
                $('#modal-edit').modal('show');
            },
            error: function (er) {
                alert(er);
            }
        });
}

function OpenDeleteModalUser(id) {
    var data = { idUser: id };
    $.ajax(
        {
            type: 'GET',
            url: '/Usuarios/Delete',
            contentType: 'application/json; charset=utf=8',
            data: data,
            success: function (result) {
                $('#modal-delete-content').html(result);
                $('#modal-delete').modal('show');
            },
            error: function (er) {
                alert(er);
            }
        });
}
function OpenAddModalUser() {
    $.ajax(
        {
            type: 'GET',
            url: '/Usuarios/Create',
            contentType: 'application/json; charset=utf=8',
            success: function (result) {
                $('#modal-add-content').html(result);
                $('#modal-add').modal('show');
            },
            error: function (er) {
                alert(er);
            }
        });
}