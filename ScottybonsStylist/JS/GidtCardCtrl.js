var app = angular.module('app', ['ui.grid', 'ui.grid.selection', 'ui.grid.saveState', 'ui.grid.exporter',
    'ui.grid.pagination', 'ui.grid.selection', 'ui.grid.cellNav', 'ui.grid.resizeColumns', 'ui.grid.moveColumns',
    'ui.grid.pinning', 'ui.bootstrap', 'ui.grid.autoResize']);

app.controller('GiftCardCtrl', ['$scope', '$http', 'uiGridConstants', '$interval', '$modal', '$log', function ($scope, $http, uiGridConstants, $interval, $modal, $log) {

    $scope.myAppScopeProvider = {
        showInfo: function (row) {
            var modalInstance = $modal.open({
                controller: 'GiftCardDetailsCtrl',
                templateUrl: 'ngTemplate/infoPopup.html',
                resolve: {
                    selectedRow: function () {
                        return row.entity;
                    }
                }
            });
        }
    }

    $scope.NewGiftCard = function () {

        var modalInstance = $modal.open({
            controller: 'GiftCardNewCtrl',
            templateUrl: 'ngTemplate/newPopup.html'
        });
        modalInstance.opened.then(function () {
            setTimeout(function () {
                $("#datepickerExp").datepicker({minDate:0});
            }, 100);
        });
    };

    $scope.gridOptions = {
        showFooter: true,
        enableSorting: true,
        multiSelect: false,
        enableFiltering: true,
        enableRowSelection: true,
        enableSelectAll: false,
        enableRowHeaderSelection: false,
        selectionRowHeaderWidth: 35,
        noUnselect: true,
        enableGridMenu: true,
        columnDefs: [
                   { field: 'Code', width: '13%', displayName: 'Gift Card Code' },
                   { field: 'OrderNumber', width: '15%', displayName: 'Order Number' },
                   { field: 'OrderDate', width: '17%', displayName: 'Order Date Time' },
                   { field: 'Email', width: '15%', displayName: 'Email Address' },
                   { field: 'ForWho', width: '13%', displayName: 'For Who' },
                   { field: 'Value', width: '8%', displayName: 'Original Value' },
                   { field: 'CurrentValue', width: '8%', displayName: 'Current Value' },
                   { field: 'Status', width: '11%', displayName: 'Order Status' }
        ],

        exporterCsvFilename: 'GiftCards.csv',
        exporterPdfDefaultStyle: { fontSize: 9 },
        exporterPdfTableStyle: { margin: [15, 15, 15, 15] },
        exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
        exporterPdfHeader: { text: "Gift Cards", style: 'headerStyle' },
        exporterPdfFooter: function (currentPage, pageCount) {
            return { text: currentPage.toString() + ' of ' + pageCount.toString(), style: 'footerStyle' };
        },
        exporterPdfCustomFormatter: function (docDefinition) {
            docDefinition.styles.headerStyle = { fontSize: 20, bold: true };
            docDefinition.styles.footerStyle = { fontSize: 10, bold: true };
            return docDefinition;
        },
        exporterPdfOrientation: 'landscape',
        exporterPdfPageSize: 'LETTER',
        exporterPdfMaxGridWidth: 500,
        exporterCsvLinkElement: angular.element(document.querySelectorAll(".custom-csv-link-location")),

        onRegisterApi: function (gridApi) {
            $scope.gridApi = gridApi;
        },
        paginationPageSize: 20,
        paginationPageSizes: [20, 40, 60, 80],
        appScopeProvider: $scope.myAppScopeProvider,
        rowTemplate: "<div ng-dblclick=\"grid.appScope.showInfo(row)\" ng-repeat=\"(colRenderIndex, col) in colContainer.renderedColumns track by col.colDef.name\" class=\"ui-grid-cell\" ng-class=\"{ 'ui-grid-row-header-cell': col.isRowHeader }\" ui-grid-cell></div>"
    };

    $scope.paginationGridOptions = {
        paginationPageSizes: [20, 40, 60, 80],
        paginationPageSize: 20,
    };
    angular.extend($scope.paginationGridOptions, $scope.gridOptions);

    $http.get('/api/GiftCardAdmin/Get')
        .success(function (data) {
            data.forEach(function (row) {
                row.registered = Date.parse(row.registered);
            });
            $scope.gridOptions.data = data;
            $scope.paginationGridOptions.data = data;
        }).error(function (reason) {
            //if (reason.status == 401) {
            //    this.$window.location.href = '/Account/Login?returnurl=/Home/index';
            //}
            $scope.title = "Oops... something went wrong";
            $scope.working = false;
        });

    angular.extend($scope.paginationGridOptions, $scope.gridOptions);
}]);

app.controller('GiftCardNewCtrl', ['$scope', '$http', 'uiGridConstants', '$interval', '$modal', '$modalInstance', '$log', function ($scope, $http, uiGridConstants, $interval, $modal, $modalInstance, $log) {
   
    $scope.modal = {};
    $scope.modal.country = [];
    $scope.modal.emailerror = false;
    
    $http.get('/api/GiftCardAdmin/GetCountries')
        .success(function (result) {
            angular.forEach(result, function (item) {
                $scope.modal.country.push(item);
                $scope.modal.ShipmentCountry = $scope.modal.country[0].CountryId;
            });
        }).error(function (reason) {
            $scope.title = "Oops... something went wrong";
            $scope.working = false;
        });

    $http.get('/api/GiftCardAdmin/GenerateOrderNumber')
        .success(function (result) {
            $scope.modal.OrderNumber = result;
        }).error(function (reason) {
            $scope.title = "Oops... something went wrong";
            $scope.working = false;
        });

    $scope.Cancel = function () {
        $modalInstance.close();
    };  

    $scope.Save = function () {
        $('.btn-gc-save').prop("disabled", true);
       /* var expDate = ($scope.modal.ExpirationDateCtrl.getMonth() + 1) + "/" + $scope.modal.ExpirationDateCtrl.getDate() + "/" + $scope.modal.ExpirationDateCtrl.getFullYear();
        $scope.modal.ExpirationDate = expDate;*/
        //$scope.modal.ExpirationDateCtrl = new Date($scope.modal.ExpirationDateCtrl);
        $http({
            method: 'POST',
            url: '/api/GiftCardAdmin/CheckOrderNumberStatus?orderNumber=' + $scope.modal.OrderNumber,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            // handle success things
            if (data.Status) {
                $scope.ConfirmSave();
            } else {
                alert('OrderNumber ' + $scope.modal.OrderNumber + ' has been issued to other order, your new order number is ' + data.OrderNumber);
                $scope.modal.OrderNumber = data.OrderNumber;
                $scope.ConfirmSave();
            }
        }).error(function (data) {
            // handle error things
            alert(data.Message);
        });
    };

    $scope.ConfirmSave = function () {
        $http({
            method: 'POST',
            url: '/api/GiftCardAdmin/HandleGiftCard',
            data: $.param($scope.modal),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            // handle success things
            location.reload();
        }).error(function (data) {
            // handle error things
            alert(data.Message);
        });
    }

    $scope.CheckEmailValidation = function () {
        $scope.modal.emailerror = $scope.email.hasClass('ng-invalid-email');
    }

}]);

app.controller('GiftCardDetailsCtrl', ['$scope', '$http', 'uiGridConstants', '$interval', '$modal', '$modalInstance', '$log', 'selectedRow', function ($scope, $http, uiGridConstants, $interval, $modal, $modalInstance, $log, selectedRow) {

    $scope.loader = {
        loading: false,
    };

    $scope.GiftCardInfo = {};

    $scope.selectedRow = selectedRow;
    var orderId = $scope.selectedRow.GiftCardId;
    $scope.loader.loading = true;

    $http.get('/api/GiftCardAdmin/Get/' + orderId)
        .success(function (data) {
            $scope.loader.loading = false;
            $scope.GiftCardInfo = data;
        }).error(function (reason) {
            $scope.title = "Oops... something went wrong, please try again";
            $scope.working = false;
        });

    $scope.ok = function () {
        $scope.selectedRow = null;
        $modalInstance.close();
    };

}]);