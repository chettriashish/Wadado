(function (wdo) {
    var AccountLoginModel = function () {
        var self = this;
        self.LoginEmail = '';
        self.Password = '';
        self.RememberMe = false;       
    }
    wdo.AccountLoginModel = AccountLoginModel;
}(window.Wadado));