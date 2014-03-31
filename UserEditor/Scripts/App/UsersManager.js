
var UserManager = (function () {

    var apiPath = "api/users";

    var getUsers = function (onSuccess, onError) {
        
        $.ajax(apiPath, {
            type: "GET",
            success: onSuccess,
            error: onError
        });
    };

    var addUser = function (userDto, onSuccess, onError) {
        $.ajax(apiPath, {
            type: "POST",
            data: userDto,
            success: onSuccess,
            error: onError
        });
    };

    var updateUser = function (userDto, onSuccess, onError) {
        $.ajax(apiPath + "/" + userDto.Id, {
            type: "PUT",
            data: userDto,
            success: onSuccess,
            error: onError
        });
    };

    var removeUser = function (id, onSuccess, onError) {
        $.ajax(apiPath + "/" + id, {
            type: "DELETE",
            success: onSuccess,
            error: onError
        });
    };

    return {
        GetUsers: getUsers,
        AddUser: addUser,
        UpdateUser: updateUser,
        RemoveUser: removeUser,
    };
})();
