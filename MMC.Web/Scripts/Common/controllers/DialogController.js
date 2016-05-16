(function () {
    var app = angular.module("appMain");
    var dialogController = function ($scope) {
        $scope.dialog = {};
        $scope.$on("SHOWDIALOG", function (event, args) {
            $scope.showDialog = true;
            $scope.dialog.header = args.header;
            $scope.dialog.body = args.body;
            $scope.dialog.showButtons = args.showButtons;
            $scope.dialog.isUserAction = args.isUserAction;
        });
        $scope.$on("HIDEDIALOG", function (event, args) {
            $scope.showDialog = false;
            $scope.$apply();
        });        
        /************IMAGE DIALOG START*************/       
        $scope.$on("SHOWIMAGEDIALOG", function (event, args) {
            $scope.image = { result: "", name: "" };
            $scope.showImageDialog = true;
        });

        $scope.fileChanged = function (e) {
            var files = e.target.files;
            var fileReader = new FileReader();
            fileReader.readAsDataURL(files[0]);

            fileReader.onload = function (e) {
                $scope.imgSrc = this.result;
                $scope.$apply();
            };
        }
        $scope.clear = function () {
            $scope.imageCropStep = 1;
            delete $scope.imgSrc;
            delete this.result;
            delete this.resultBlob;
        };

        $scope.save = function () {
            $scope.image.result = this.result;
            $scope.showImageDialog = false;
            $scope.imageCropStep = 1;
            delete $scope.imgSrc;
            delete this.result;
            delete this.resultBlob;
            $scope.$emit("IMAGE_CROPPED", $scope.image);
        }

        $scope.cancel = function () {
            $scope.showImageDialog = false;
            $scope.imageCropStep = 1;
            delete $scope.imgSrc;
            delete this.result;
            delete this.resultBlob;            
        }
        /************IMAGE DIALOG END*************/
    }
    app.controller("DialogController", dialogController);
}());