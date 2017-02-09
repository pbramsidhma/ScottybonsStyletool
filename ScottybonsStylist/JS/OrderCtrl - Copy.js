var app = angular.module('app', ['ui.grid', 'ui.grid.selection', 'ui.grid.saveState', 'ui.grid.exporter',
    'ui.grid.pagination', 'ui.grid.selection', 'ui.grid.cellNav', 'ui.grid.resizeColumns', 'ui.grid.moveColumns',
    'ui.grid.pinning', 'ui.bootstrap', 'ui.grid.autoResize', 'ui.grid.grouping']);

app.controller('MainCtrl', ['$scope', '$http', 'uiGridConstants', '$interval', '$modal', '$log', function ($scope, $http, uiGridConstants, $interval, $modal, $log) {

    $scope.myAppScopeProvider = {
        showInfo: function (row) {
            var modalInstance = $modal.open({
                controller: 'ChildCtrl',
                templateUrl: 'ngTemplate/infoPopup.html',
                resolve: {
                    selectedRow: function () {
                        return row.entity;
                    }
                }
            });

            //modalInstance.result.then(function (selectedItem) {
            //    $log.log('modal selected Row: ' + selectedItem);
            //}, function () {
            //    $log.info('Modal dismissed at: ' + new Date());
            //});
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

        columnDefs: [
                   { field: 'OrderId', width: '15%', displayName: 'Order Number' },
                   { field: 'CreatedOn', width: '10%', displayName: 'Order Date' },
                   { field: 'CustomerID', width: '10%', displayName: 'Customer ID' },
                   { field: 'FirstName', width: '15%', displayName: 'First Name' },
                   { field: 'LastName', width: '15%', displayName: 'Last Name' },
                   { field: 'ContactNumber', width: '15%', displayName: 'Phone Number' },
                   { field: 'OrderStatusName', width: '20%', displayName: 'Order Status' }
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
}]);

app.controller('ChildCtrl', ['$scope', '$http', 'uiGridConstants', '$interval', '$modal', '$modalInstance', '$log', 'selectedRow', function ($scope, $http, uiGridConstants, $interval, $modal, $modalInstance, $log, selectedRow) {

    $scope.loader = {
        loading: false,
    };

    $scope.selectedRow = selectedRow;

    var orderId = $scope.selectedRow.OrderId;
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
        }).error(function (error,reason) {
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