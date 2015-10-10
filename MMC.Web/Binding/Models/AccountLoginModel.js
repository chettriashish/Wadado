(function (mmc) {
    var AccountLoginModel = function () {

        var self = this;
        self.LoginEmail = '';
        self.Password = '';
        self.RememberMe = false;
    }
    mmc.AccountLoginModel = AccountLoginModel;
}(window.MMC));