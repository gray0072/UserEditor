var UserModel = function(data) {
    var self = this;

    this.Id = ko.observable(data.Id);
    this.Firstname = ko.observable(data.Firstname);
    this.Lastname = ko.observable(data.Lastname);
    this.Status = ko.observable(data.Status);
    this.Pages = ko.observableArray(data.Pages);
    this.IsAdmin = ko.observable(data.IsAdmin);
};

var UserEditModel = function (id, fistname, lastname, status, pages, isAdmin) {
    var self = this;

    this.Id = ko.observable(id);
    this.Firstname = ko.observable(fistname);
    this.Lastname = ko.observable(lastname);
    this.Status = ko.observable(status);
    this.Pages = ko.observableArray(pages);
    this.IsAdmin = ko.observable(isAdmin);

    this.AvailablePages = ko.computed(function () {
        var result = ko.utils.makeArray(mockModels.userPages);
        for (var i = 0; i < self.Pages().length; i++) {
            ko.utils.arrayRemoveItem(result, self.Pages()[i]);
        }
        return result;
    });

};

var ViewModel = function (data) {
    var self = this;

    var users = ko.observableArray();

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

    this.Users = ko.computed(function () {
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

    this.EditUserModel = ko.observable();


    this.AddUser = function () {
        self.EditUserModel(new UserEditModel(0, "", "", "", [], false));
        self.InitForm();
    };

    this.EditUser = function (user) {
        self.EditUserModel(new UserEditModel(user.Id(), user.Firstname(), user.Lastname(), user.Status(), user.Pages(), user.IsAdmin()));
        self.InitForm();
    };

    this.InitForm = function () {
        $(".js-page").draggable({ containment: ".js-page-input"/*helper: "clone"*/ });
        $(".js-user").droppable({
            drop: function (event, ui) {
                var page = $(ui.draggable).attr("data-page");
                self.EditUserModel().Pages.push(page);
            }
        });
        $(".js-available").droppable({
            drop: function (event, ui) {
                var page = $(ui.draggable).attr("data-page");
                var index = self.EditUserModel().Pages.indexOf(page);
                self.EditUserModel().Pages.splice(index, 1);
            }
        });
    };

    this.Save = function () {
        var userNew = self.EditUserModel();
        var id = self.EditUserModel().Id();
        if (id != 0) {
            // update user
            var user = self.GetById(id);
            user.Firstname(userNew.Firstname());
            user.Lastname(userNew.Lastname());
            user.Status(userNew.Status());
            user.Pages(userNew.Pages());
            user.IsAdmin(userNew.IsAdmin());
        }
        else {
            users.push(new UserModel({
                Id: self.GetNextId(),
                Firstname: userNew.Firstname(),
                Lastname: userNew.Lastname(),
                Status: userNew.Status(),
                Pages: userNew.Pages(),
                IsAdmin: userNew.IsAdmin()
            }));
        }
        self.EditUserModel(null);
    };

    this.Cancel = function () {
        self.EditUserModel(null);
    };

    this.Delete = function () {
        self.RemoveById(self.EditUserModel().Id());
        self.EditUserModel(null);
    };

    // Help functions

    this.GetById = function (id) {
        var result = null;
        ko.utils.arrayForEach(users(), function (user) {
            if (user.Id() == id) result = user;
        });
        return result;
    };

    this.RemoveById = function (id) {
        var user = self.GetById(id);
        users.remove(user);
    };

    var nextId = -1;
    this.GetNextId = function () {
        var result = nextId;
        nextId--;
        return result;
    };

    // Init
    (function () {
        ko.utils.arrayForEach(data, function (user) {
            users.push(new UserModel(user));
        });
    })();

};

var mockModels = {
    userStatuses: ["Single", "Married", "Divorced"],
    userModels: [{ Id: 1, Firstname: "aaa", Lastname: "111", Status: "Single", Pages: ["Page1", "Page2"], IsAdmin: true },
        { Id: 2, Firstname: "bbb", Lastname: "222", Status: "Married", Pages: ["Page1", "Page3"], IsAdmin: false}],
    userPages: ["Page1", "Page2", "Page3"]
};


$(function() {
    var viewModel = new ViewModel(mockModels.userModels);

    ko.applyBindings(viewModel);
});