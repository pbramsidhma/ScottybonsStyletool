var app = angular.module('app', ['ui.grid', 'ui.grid.selection', 'ui.grid.saveState', 'ui.grid.exporter',
    'ui.grid.pagination', 'ui.grid.selection', 'ui.grid.cellNav', 'ui.grid.resizeColumns', 'ui.grid.moveColumns',
    'ui.grid.pinning', 'ui.bootstrap', 'ui.grid.autoResize']);

app.controller('GiftCardRedemptionCtrl', ['$scope', '$http', 'uiGridConstants', '$interval', '$modal', '$log', function ($scope, $http, uiGridConstants, $interval, $modal, $log) {

    $scope.NewGiftCardRedemption = function () {

        var modalInstance = $modal.open({
            controller: 'GiftCardNewRedemptionCtrl',
            templateUrl: 'ngTemplate/newPopup.html'
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
                   { field: 'GiftCardRedemptionId', width: '13%', displayName: 'Redemption ID' },
                   { field: 'OrderNumber', width: '15%', displayName: 'Order Number' },
                   { field: 'GiftCardCode', width: '17%' },
                   { field: 'AmounOfRedemption', width: '10%', displayName: 'Amount' },
                   { field: 'CustomerId', width: '12%', displayName: 'Customer ID' },
                   { field: 'RedemptionDateTime', width: '21%', displayName: 'Redemption Date Time' },
                   { field: 'Status', width: '12%', displayName: 'Status' }
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

    $http.get('/api/GiftCardAdmin/GetRedemptions')
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

app.controller('GiftCardNewRedemptionCtrl', ['$scope', '$http', 'uiGridConstants', '$interval', '$modal', '$modalInstance', '$log', function ($scope, $http, uiGridConstants, $interval, $modal, $modalInstance, $log) {

    $("input[type='number']").prop('min', 1);

    $scope.modal = {};
    $scope.ValidOrderNumberGiftCardCode = true;
    $scope.ShowGiftCardCodeCurrentBalance = false;
    $scope.ValidOrderNumber = true;
    $scope.ValidGiftCardCode = true;
    $scope.ValidCustomerId = true;

    $scope.CheckGiftCardBalance = function (ctrl) {
        $scope.ValidGiftCardCode = true;

        if (ctrl.target.value.trim() != '') {
            $http({
                method: 'POST',
                url: '/api/GiftCardAdmin/CheckGiftCardBalance?giftCardCode=' + ctrl.target.value.trim(),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {
                // handle success things
                if (data.Exists) {
                    $scope.ValidGiftCardCode = true;
                    $scope.ShowGiftCardCodeCurrentBalance = true;
                    $scope.modal.CurrentBalance = data.CurrentBalance;
                    $('.gc-Amount-value').prop('max', data.CurrentBalance);

                    //if ($scope.modal.OrderNumber != "" && $scope.modal.OrderNumber != undefined && $scope.ValidGiftCardCode) {
                    //    $scope.CheckOrderNumber(null,false);
                    //}
                } else {
                    $scope.ValidGiftCardCode = false;
                    $scope.ShowGiftCardCodeCurrentBalance = false;
                    $scope.modal.CurrentBalance = "";
                }
            }).error(function (data) {
                // handle error things
                alert(data.Message);
            });
        }

    }

    $scope.ValidateCustomerId = function (ctrl) {
        $scope.ValidCustomerId = true;
        if (ctrl.target.value.trim() != "") {
            $http({
                method: 'POST',
                url: '/api/GiftCardAdmin/ValidCustomerId?customerId=' + parseInt(ctrl.target.value.trim()),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {
                // handle success things
                if (!data) {
                    $scope.ValidCustomerId = false;
                }
            }).error(function (data) {
                // handle error things
                alert("Error");
            });
        }
    }

    //$scope.CheckOrderNumber = function (ctrl, selfAction) {
    //    $scope.ValidOrderNumber = true;
    //    $scope.ValidOrderNumberGiftCardCode = true;
    //    var eletext = selfAction ? ctrl.target.value.trim() : $scope.modal.OrderNumber;

    //    if (eletext != '' && $scope.ValidGiftCardCode && $scope.modal.GiftCardCode != undefined && $scope.modal.GiftCardCode != '') {
    //        $http({
    //            method: 'POST',
    //            url: '/api/GiftCardAdmin/CheckOrderNumberExistsGiftCardValid?orderNumber=' + eletext + '&giftCardCode=' + $scope.modal.GiftCardCode,
    //            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
    //        }).success(function (data) {
    //            // handle success things
    //            if (!data.Valid) {
    //                $scope.ValidOrderNumber = false;
    //            } else if (!data.GiftCardExists) {
    //                $scope.ValidOrderNumberGiftCardCode = false;
    //            }
    //        }).error(function (data) {
    //            // handle error things
    //            alert(data.Message);
    //        });
    //    }

    //}

    $scope.CheckOrderNumber = function (ctrl, selfAction) {
        $scope.ValidOrderNumber = true;
        $scope.ValidOrderNumberGiftCardCode = true;
        var eletext = selfAction ? ctrl.target.value.trim() : $scope.modal.OrderNumber;

        if (eletext != '' && $scope.ValidGiftCardCode && $scope.modal.GiftCardCode != undefined && $scope.modal.GiftCardCode != '') {
            $http({
                method: 'POST',
                url: '/api/GiftCardAdmin/CheckOrderNumberExists?orderNumber=' + parseInt(eletext) + '&giftCardCode=' + $scope.modal.GiftCardCode,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).success(function (data) {
                // handle success things
                if (!data.Valid) {
                    $scope.ValidOrderNumber = false;
                } else if (!data.GiftCardExists) {
                    $scope.ValidOrderNumberGiftCardCode = false;
                }
            }).error(function (data) {
                // handle error things
                alert(data.Message);
            });
        }

    }

    $scope.Save = function () {
        $http({
            method: 'POST',
            url: '/api/GiftCardAdmin/HandleGiftCardRedemption',
            data: $.param($scope.modal),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            // handle success things
            location.reload();
        }).error(function (data) {
            // handle error things
            alert(data.Message);
        });
    };

    $scope.Cancel = function () {
        $modalInstance.close();
    };
}]);

