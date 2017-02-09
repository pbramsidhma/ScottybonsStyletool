var app = angular.module('app', ['ui.grid', 'ui.grid.selection', 'ui.grid.saveState', 'ui.grid.exporter',
    'ui.grid.pagination', 'ui.grid.selection', 'ui.grid.cellNav', 'ui.grid.resizeColumns', 'ui.grid.moveColumns',
    'ui.grid.pinning', 'ui.bootstrap', 'ui.grid.autoResize', 'ui.grid.grouping']);

app.controller('MainCtrl', ['$scope', '$http', 'uiGridConstants', '$interval', '$modal', '$log', function ($scope, $http, uiGridConstants, $interval, $modal, $log, uiGridGroupingConstants) {

    $scope.myAppScopeProvider = {
        showInfo: function (row) {
            if (row.entity.OrderId != undefined) {
                var modalInstance = $modal.open({
                    controller: 'ChildCtrl',
                    templateUrl: 'ngTemplate/infoPopup.html',
                    resolve: {
                        selectedRow: function () {
                            return row.entity;
                        }
                    }
                });
            }
            //modalInstance.result.then(function (selectedItem) {
            //    $log.log('modal selected Row: ' + selectedItem);
            //}, function () {
            //    $log.info('Modal dismissed at: ' + new Date());
            //});
        },
        showSubscription: function (row) {
            //console.log(row.entity); 
            var modalInstance = $modal.open({
                controller: 'SubscriptionCtrl',
                templateUrl: 'ngTemplate/subscriptionPopup.html',
                resolve: {
                    selectedRow: function () {
                        return row.entity;
                        //return true PerodicalMonths;
                    }
                }
            });
        },
        checkSubscription: function (row) {
            //console.log(row);
            return true;
        },

        checkSubscriptionStatus: function (row) {
            if (row.treeLevel != 0) {
                var aa = row;
            }
            return false;
        }
    }


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
        treeRowHeaderAlwaysVisible: true,
        columnDefs: [
                   //{
                   //    field: 'CustomerID', width: '10%', displayName: 'Customer ID', grouping: { groupPriority: 0 },
                   //    sort: { priority: 0, direction: 'desc' },
                   //    cellTemplate: '<div><div ng-if="!col.grouping || col.grouping.groupPriority === undefined || col.grouping.groupPriority === null || ( row.groupHeader && col.grouping.groupPriority === row.treeLevel )" class="ui-grid-cell-contents" title="TOOLTIP">{{COL_FIELD CUSTOM_FILTERS}}</div></div>'
                   //},
                   { field: 'OrderId', width: '10%', displayName: 'Order Number' },                   
                   { field: 'CreatedOn', width: '10%', displayName: 'Order Date' },
                   { field: 'CustomerID', width: '10%', displayName: 'Customer ID' },
                   { field: 'FirstName', width: '15%', displayName: 'First Name' },
                   { field: 'LastName', width: '15%', displayName: 'Last Name' },
                   { field: 'ContactNumber', width: '18%', displayName: 'Phone Number' },
                   { field: 'OrderStatusName', width: '22%', displayName: 'Order Status' },
                   //{
                   //    field: 'PeriodicalScottyBoxID', width: '10%', displayName: 'Subscription',
                   //    cellTemplate: '<div ng-disabled="grid.appScope.checkSubscriptionStatus(row)" ng-show="grid.appScope.checkSubscription(row) && row.treeLevel!=0"><button class="btn btn-primary btnCustom btnSubscription" ng-click="grid.appScope.showSubscription(row)">Subscriptions</button></div>'
                   //}

        ],
        exporterCsvFilename: 'OrderList.csv',
        exporterPdfDefaultStyle: { fontSize: 9 },
        exporterPdfTableStyle: { margin: [15, 15, 15, 15] },
        exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
        exporterPdfHeader: { text: "Order List", style: 'headerStyle' },
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

    $http.get('/api/OrderAdmin/Get')
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

    $('.ui-grid-row').find('button').hide();
}]);

app.controller('SubscriptionCtrl', ['$scope', '$http', 'uiGridConstants', '$interval', '$modal', '$modalInstance', '$log', 'selectedRow', function ($scope, $http, uiGridConstants, $interval, $modal, $modalInstance, $log, selectedRow) {

    $scope.selectedRows = selectedRow;

    $scope.selectedRows.subscriptionMode = ($scope.selectedRows.PeriodicalScottyBoxID == 0) ? '0' : '1';
    $scope.selectedRows.deliveryAtNeighbour = ($scope.selectedRows.deliveryAtNeighbour == 0) ? '0' : '1';

    if (selectedRow.PeriodicalScottyBoxID <= 6 || selectedRow.PeriodicalScottyBoxID == null) {
        $scope.subscriptions = [
        { name: 'One Month', value: 1 },
        { name: 'Two Months', value: 2 },
        { name: 'Three Months', value: 3 }];
    }
    else {
        $scope.subscriptions = [
        { name: 'One Month', value: 7 },
        { name: 'Two Months', value: 8 },
        { name: 'Three Months', value: 9 }];
    }
    if ($scope.selectedRows.OrderOccasion < 1002) {
        $scope.occasions = [
        { name: 'Business/Casual', value: 1 },
        { name: 'Casual/Trendy', value: 2 },
        { name: 'Other', value: 1002 }];
    } else {
        $scope.occasions = [
        { name: 'Business/Casual', value: 1003 },
        { name: 'Casual/Trendy', value: 1004 },
        { name: 'Other', value: 1005 }];
    }


    $scope.valiateDtls = function () {
        return false;
    };
    //console.log($scope.selectedRows);
    $scope.save = function () {

        $scope.newSubscription = $scope.selectedRows.PeriodicalScottyBoxID;

        $http({
            method: 'POST',
            url: '/api/SubscriptionsAdmin/',
            data: {
                OrderId: $scope.selectedRows.OrderId,
                newSubscription: $scope.selectedRows.PeriodicalScottyBoxID,
                subscriptionMode: $scope.selectedRows.subscriptionMode,
                orderOccasion: $scope.selectedRows.occasionSelected,
                orderStreet: $scope.selectedRows.OrderStreet,
                orderHouseNumber: $scope.selectedRows.OrderHouseNumber,
                orderPostalCode: $scope.selectedRows.OrderPostalCode,
                orderTown: $scope.selectedRows.OrderTown,
                deliveryNeighbour: $scope.selectedRows.deliveryAtNeighbour,
                contactNumber: $scope.selectedRows.ContactNumber
            }
        }).then(function successCallback(response) {
            //console.log(response.data);
            //console.log(response.data.result);
            $scope.selectedRow = null;
            $modalInstance.close();
            location.reload();
        }, function errorCallback(response) {
            console.log("Fail " + response);
        });
    };

    $scope.ok = function () {
        $scope.selectedRow = null;
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $scope.selectedRow = null;
        $modalInstance.dismiss('cancel');
    };
}]);

app.controller('ChildCtrl', ['$scope', '$http', 'uiGridConstants', '$interval', '$modal', '$modalInstance', '$log', 'selectedRow', function ($scope, $http, uiGridConstants, $interval, $modal, $modalInstance, $log, selectedRow) {

    $scope.loader = {
        loading: false,
    };

    $scope.selectedRow = selectedRow;

    var orderId = $scope.selectedRow.OrderId;

    if (orderId != undefined) {

        var excelName = "OrderDetailsOf" + orderId + ".csv";

        $scope.loader.loading = true;

        $scope.gridOptions = {
            showFooter: false,
            enableSorting: false,
            multiSelect: false,
            enableFiltering: false,
            enableRowSelection: false,
            enableSelectAll: false,
            enableRowHeaderSelection: false,
            exporterMenuCsv: true,
            enableGridMenu: true,
            noUnselect: true,


            headerTemplate: '<div class="ui-grid-top-panel" style="text-align: center">Order Details for : ' + orderId + '</div>',
            columnDefs: [
                {
                    field: 'FirstColumnValue', displayName: 'Name', width: '40%',

                    cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                        if ((grid.getCellValue(row, col) === 'Order Information') || ((grid.getCellValue(row, col) === 'Customer - Style Intake'))) {
                            return 'blue';
                        }
                    }
                },
            {
                field: 'SecondColumnValue',
                displayName: 'Value',
                width: '60%',
                hight: 'auto'
                //}, {
                //    field: 'AnswerImage',
                //    displayName: 'Image',
                //    hight: '100%',
                //    width: '50%',
                //    cellTemplate: '<div class="ui-grid-cell-contents">' +
                //                    '<img ng-if="row.entity.SecondColumnValue == \'img\'"  ng-src="data:image/PNG;base64,  {{row.entity.AnswerImage}}" height="200" width="200" />' +
                //                    '</div>'
            }

            ],


            exporterSuppressColumns: ['AnswerImage'],
            exporterCsvFilename: excelName,

            exporterPdfDefaultStyle: { fontSize: 9 },
            exporterPdfTableStyle: { margin: [30, 15, 15, 15] },
            exporterPdfTableHeaderStyle: { fontSize: 10, bold: true, italics: true, color: 'blue' },
            exporterPdfHeader: { text: "Customer Order Details ", style: 'headerStyle' },
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
        };

        $http.get('/api/OrderAdmin/Get/' + orderId)
            .success(function (data) {
                data.forEach(function (row) {
                    row.registered = Date.parse(row.registered);
                });
                $scope.gridOptions.data = data;
                $scope.loader.loading = false;
            }).error(function (error, reason) {
                $scope.title = "Oops... something went wrong, please try again";
                $scope.gridOptions.data = $scope.title;
                $scope.working = false;
            });

        $scope.gridImages = {};
        $http.get('/api/CustomerAdmin/Get/' + orderId)
       .success(function (data) {
           data.forEach(function (row) {
               row.registered = Date.parse(row.registered);
           });
           $scope.gridImages.data = data;
           $scope.loader.loading = false;
       }).error(function (reason) {
           $scope.title = "Oops... something went wrong, please try again";
           $scope.gridImages.data = $scope.title;
           $scope.working = false;
       });

        $scope.gridImages = {
            showFooter: false,
            enableSorting: false,
            multiSelect: false,
            enableFiltering: false,
            enableRowSelection: false,
            enableSelectAll: false,
            enableRowHeaderSelection: false,



            headerTemplate: '<div class="ui-grid-top-panel" style="text-align: center">Images</div>',
            columnDefs: [
                {
                    field: 'FirstColumnValue', displayName: 'Name', width: '20%',

                    cellClass: function (grid, row, col, rowRenderIndex, colRenderIndex) {
                        if ((grid.getCellValue(row, col) === 'Order Information') || ((grid.getCellValue(row, col) === 'Customer - Style Intake'))) {
                            return 'blue';
                        }
                    }
                },
            {
                field: 'SecondColumnValue',
                displayName: 'Value',
                width: '20%',
                hight: 'auto'
            }, {
                field: 'AnswerImage',
                displayName: 'Image',
                hight: '100%',
                width: '60%',
                cellTemplate: '<div class="ui-grid-cell-contents">' +
                                '<img ng-if="row.entity.SecondColumnValue == \'img\'"  ng-src="data:image/PNG;base64,  {{row.entity.AnswerImage}}" height="200" width="200" />' +
                                '</div>'
            }

            ],

            onRegisterApi: function (gridApi) {
                $scope.gridApi = gridApi;
            },


        };

        $scope.toggleFlat = function () {
            $scope.gridOptions.flatEntityAccess = !$scope.gridOptions.flatEntityAccess;
        }

        $scope.ok = function () {
            $scope.selectedRow = null;
            $modalInstance.close();
        };

        $scope.cancel = function () {
            $scope.selectedRow = null;
            $modalInstance.dismiss('cancel');
        };
    } else {
        $modalInstance.close();
    }

}]);



//app.controller('InfoController', ['$scope', '$modal', '$modalInstance', '$filter', '$interval', 'selectedRow', function ($scope, $modal, $modalInstance, $filter, $interval, selectedRow) {

//    $scope.selectedRow = selectedRow;
//    $scope.ok = function () {
//        $scope.selectedRow = null;
//        $modalInstance.close();
//    };
//    $scope.cancel = function () {
//        $scope.selectedRow = null;
//        $modalInstance.dismiss('cancel');
//    };
//}
//]);