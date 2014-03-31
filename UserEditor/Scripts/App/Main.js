var UserModel = function(data) {
    var self = this;

    this.Id = ko.observable(data.Id);
    this.Firstname = ko.observable(data.Firstname);
    this.Lastname = ko.observable(data.Lastname);
    this.Status = ko.observable(data.Status);
    this.Pages = ko.observableArray(data.Pages || []);
    this.IsAdmin = ko.observable(data.IsAdmin);

    this.PagesSorted = ko.computed(function() {
        var result = self.Pages().slice(0);
        result.sort();
        return result.join("/");
    });
};

var UserEditModel = function (data) {
    var self = this;

    this.Id = ko.observable(data.Id);
    this.Firstname = ko.observable(data.Firstname).extend({ required: "Firstname is required" });
    this.Lastname = ko.observable(data.Lastname).extend({ required: "Lastname is required" });
    this.Status = ko.observable(data.Status);
    this.Pages = ko.observableArray(data.Pages || []);
    this.IsAdmin = ko.observable(data.IsAdmin);

    this.PagesSorted = ko.computed(function () {
        var result = self.Pages().slice(0);
        result.sort();
        return result;
    });

    this.AvailablePages = ko.computed(function () {
        var result = mockModels.userPages.slice(0);
        for (var i = 0; i < self.Pages().length; i++) {
            ko.utils.arrayRemoveItem(result, self.Pages()[i]);
        }
        return result;
    });

    this.hasError = ko.computed(function() {
        return self.Firstname.hasError() || self.Lastname.hasError();
    });
};

var ViewModel = function () {
    var self = this;

    var users = ko.observableArray();

    // Sorting
    this.SortField = ko.observable("Firstname");
    this.IsSortOrderAsc = ko.observable(true);

    this.CalculateSortOrderClass = ko.computed(function () {
        return function(fieldName) {
            var isCurrent = self.SortField() == fieldName;
            var result = (!isCurrent || self.IsSortOrderAsc()) ? "asc" : "desc";
            if (isCurrent) result += " current-sort";
            return result;
        };
    });

    this.ChangeSort = function (sortField) {
        if (sortField == self.SortField()) {
            self.IsSortOrderAsc(!self.IsSortOrderAsc());
        }
        else {
            self.SortField(sortField);
            self.IsSortOrderAsc(true);
        }
    };

    this.UsersSorted = ko.computed(function () {
        var result = users();
        var sortField = self.SortField();
        var isSortOrderAsc = self.IsSortOrderAsc();
        result.sort(function (a, b) {
            if ((a[sortField]() < b[sortField]()) ^ !isSortOrderAsc) return -1;
            if ((a[sortField]() > b[sortField]()) ^ !isSortOrderAsc) return 1;
            return 0;
        });
        return result;
    });


    // Add/edit form
    this.EditUserModel = ko.observable();

    this.AddUser = function () {
        self.EditUserModel(new UserEditModel({ Id: 0 }));
        self.InitForm();
    };

    this.EditUser = function (user) {
        self.EditUserModel(new UserEditModel(ko.toJS(user)));
        self.InitForm();
    };

    this.InitForm = function () {
        $(".js-page").draggable({ revert: true });
        $(".js-user").droppable({
            drop: function (event, ui) {
                var page = $(ui.draggable).attr("data-page");
                var alredyExists = self.EditUserModel().Pages.indexOf(page) >= 0;
                if (!alredyExists) {
                    self.EditUserModel().Pages.push(page);
                    $(".js-page").draggable({ revert: true });
                }
            }
        });
        $(".js-available").droppable({
            drop: function (event, ui) {
                var page = $(ui.draggable).attr("data-page");
                var index = self.EditUserModel().Pages.indexOf(page);
                if (index >= 0) {
                    self.EditUserModel().Pages.splice(index, 1);
                    $(".js-page").draggable({ revert: true });
                }
            }
        });
    };

    this.Save = function() {
        var userNew = ko.toJS(self.EditUserModel());
        if (userNew.Id == 0) {
            // add user
            UserManager.AddUser(userNew,
                function (idFromDb) {
                    userNew.Id = idFromDb;
                    users.push(new UserModel(userNew));
                    self.EditUserModel(null);
                },
                function() {
                    self.LastError("Save to DB failed!");
                });
        } else {
            // update user
            UserManager.UpdateUser(userNew,
                function() {
                    var user = self.GetById(userNew.Id);
                    for (prop in userNew) {
                        if (user[prop] && user[prop].name == "observable")
                            user[prop](userNew[prop]);
                    }
                    self.EditUserModel(null);
                },
                function() {
                    self.LastError("Save to DB failed!");
                });
        }
    };

    this.Cancel = function () {
        self.EditUserModel(null);
    };

    this.Delete = function () {
        if (!window.confirm("Are you sure deleting user?")) return;

        var id = self.EditUserModel().Id();
        UserManager.RemoveUser(id,
            function() {
                self.RemoveById(self.EditUserModel().Id());
                self.EditUserModel(null);
            },
            function() {
                self.LastError("Deleting user failed!");
            });
    };

    // Help functions

    this.GetById = function(id) {
        var result = null;
        ko.utils.arrayForEach(users(), function(user) {
            if (user.Id() == id) result = user;
        });
        return result;
    };

    this.RemoveById = function(id) {
        var user = self.GetById(id);
        users.remove(user);
    };
    
    this.LastError = ko.observable();

    // Init
    this.Init = function(data) {
        ko.utils.arrayForEach(data, function(user) {
            users.push(new UserModel(user));
        });
    };

};

var mockModels = {
    userStatuses: ["Single", "Married", "Divorced"],
    userPages: ["Page1", "Page2", "Page3"],

    userModels: [{ Id: 1, Firstname: "aaa", Lastname: "111", Status: "Single", Pages: ["Page1", "Page2"], IsAdmin: true },
        { Id: 2, Firstname: "bbb", Lastname: "222", Status: "Married", Pages: ["Page1", "Page3"], IsAdmin: false }]
};

$(function() {
    var viewModel = new ViewModel();
    
    //viewModel.Init(mockModels.userModels);

    UserManager.GetUsers(
                function (data) {
                    viewModel.Init(data);
                },
                function () {
                    viewModel.LastError("Load from BD failed!");
                });

    ko.applyBindings(viewModel);

});