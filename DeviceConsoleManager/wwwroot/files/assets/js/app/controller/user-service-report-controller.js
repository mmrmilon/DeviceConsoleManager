app.controller('ReportController', function ($scope, $http, $filter, BaseApiService, userServices, toaster) {
    $scope.search = {
        customerId: 0,
        deviceId: 0,
        startDate: '',
        endDate: ''
    };
    $scope.UserServiceList = [];
    $scope.DeviceStatus = [];

    $scope.steppedLineData = {
        labels: ['Day 1', 'Day 2', 'Day 3', 'Day 4', 'Day 5', 'Day 6', 'Day 7'],
        datasets: [{
            label: 'Device 01',
            steppedLine: true,
            data: [-28, -21, 37, -51, 22, -89, -3],
            borderColor: 'rgb(255, 0, 0)',
            fill: false
        },
        {
            label: 'Device 02',
            steppedLine: true,
            data: [98, -88, 0, 89, -75, 42, 81],
            borderColor: 'rgb(0, 181, 204)',
            fill: false
        },
        {
            label: 'Device 03',
            steppedLine: true,
            data: [78, -48, 20, 69, -55, 22, 81],
            borderColor: 'rgb(51, 110, 123)',
            fill: false
        }]
    };

    $scope.GetAll = function (search) {
        //console.log(JSON.stringify(search));
        search.customerId = search.customerId === null ? 0 : search.customerId;
        search.deviceId = search.deviceId === null ? 0 : search.deviceId;
        search.startDate = (search.startDate === null || search.startDate === '') ? '' : $filter('date')(new Date(search.startDate), 'yyyy-MM-dd');
        search.endDate = (search.endDate === null || search.endDate === null) ? '' : $filter('date')(new Date(search.endDate), 'yyyy-MM-dd');
        //console.log('Start Date: ' + search.startDate + ', End Date: ' + search.endDate);
        $scope.UserServiceGraphData = [];
        $scope.DeviceServiceGraphData = [];
        var userServicObj = {};
        var deviceServicObj = {};

        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/UserServiceReportApi/GetUserServiceDetailsBy?customerId=' + search.customerId + '&deviceId=' + search.deviceId + '&startDate=' + search.startDate + '&endDate' + search.endDate
        }).then(function successCallback(response) {
            $scope.UserServiceList = response.data.data;

            $scope.prevDeviceId = 0;
            $scope.prevUserId = 0;
            angular.forEach($scope.UserServiceList, function (item, index) {
                if (index === 0) {
                    userServicObj = {
                        UserId: item.User.Id,
                        UserName: item.User.UserName,
                        Amount: 0
                    };

                    deviceServicObj = {
                        DeviceId: item.DeviceInformation.Id,
                        DeviceName: item.DeviceInformation.DeviceName,
                        Amount: 0
                    };
                }

                //Device Information
                if (item.DeviceInformation.Id !== $scope.prevDeviceId && index > 0) {
                    $scope.DeviceServiceGraphData.push(deviceServicObj);
                    deviceServicObj = {};
                    deviceServicObj = {
                        DeviceId: item.DeviceInformation.Id,
                        DeviceName: item.DeviceInformation.DeviceName,
                        Amount: 0
                    };
                }

                //User Information
                if (item.User.Id !== $scope.prevUserId && index > 0) {
                    $scope.UserServiceGraphData.push(userServicObj);
                    userServicObj = {};
                    userServicObj = {
                        UserId: item.User.Id,
                        UserName: item.User.UserName,
                        Amount: 0
                    };
                }

                userServicObj.Amount += item.Transiction.TransictionAmount;
                deviceServicObj.Amount += item.Transiction.TransictionAmount;

                $scope.prevUserId = item.User.Id;
                $scope.prevDeviceId = item.DeviceInformation.Id;
                //console.log('prevItem: ' + $scope.prevItem + ', currItem: ' + item.DeviceInformation.Id);
            });

            //Last Object
            $scope.UserServiceGraphData.push(userServicObj);
            $scope.DeviceServiceGraphData.push(deviceServicObj);

            //console.log(JSON.stringify($scope.UserServiceGraphData));
            //console.log(JSON.stringify($scope.DeviceServiceGraphData));

        }, function errorCallback(response) {
            }).finally(function () {
                LoadUserChartUI($scope.UserServiceGraphData);
                LoadDeviceChartUI($scope.DeviceServiceGraphData);
        });

        //$scope.GetAllUserService(search.deviceId, search.customerId, search.startDate, search.endDate);
        //$scope.GetAllDevicerService(search.deviceId, search.customerId, search.startDate, search.endDate);
    };

    $scope.GetDeviceStaus = function () {
        $scope.DeviceStatus = [];
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/UserServiceReportApi/GetDeviceServiceStatus'
        }).then(function successCallback(response) {
            //console.log(response.data.data);
            angular.forEach(response.data.data, function (item, index) {                
                var obj = {
                    UserName: item.User === null? '': item.User.UserName,
                    MobileNumber: item.User === null ? '' : item.User.MobileNumber,
                    DeviceName: item.DeviceInformation.DeviceName,
                    LocationName: item.DeviceLocation.LocationName,
                    LocationLevel: item.DeviceLocation.LocationLevel,
                    StartTime: item.DeviceStatus === null ? '' : item.DeviceStatus.CreatedOn,
                    IsRunning: item.DeviceStatus === null ? '' : item.DeviceStatus.IsActive
                };

                $scope.DeviceStatus.push(obj);
            });
            
        }, function errorCallback(response) {
        });
    };

    $scope.GetAllUsers = function () {
        userServices.GetCustomerList().then(function (res) {
            $scope.Users = res.data;
        });
    };

    $scope.GetDeviceInfoBy = function (userId) {
        if (userId > 0) {
            userServices.GetDeviceInfoBy(userId).then(function (res) {
                $scope.DeviceList = res.data;
            });
        }
        else {
            $scope.GetDeviceInfo();
        }
    };

    $scope.GetDeviceInfo = function () {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/DeviceApi/GetAll'
        }).then(function successCallback(response) {
            $scope.DeviceList = response.data.data;
        }, function errorCallback(response) {

        });
    };

    $scope.ExportUserServiceInfo = function () {
        $scope.search.startDate = ($scope.search.startDate === null || $scope.search.startDate === '') ? '' : $filter('date')($scope.search.startDate, 'yyyy-MM-dd');
        $scope.search.endDate = ($scope.search.endDate === null || $scope.search.endDate === null) ? '' : $filter('date')($scope.search.endDate, 'yyyy-MM-dd');

        $http({
            method: 'GET',
            url: BaseApiService.baseUrl() + 'api/UserServiceReportApi/ExportUserServiceDetailsBy?customerId=' + $scope.search.customerId + '&deviceId=' + $scope.search.deviceId + '&startDate=' + $scope.search.startDate + '&endDate' + $scope.search.endDate
        }).then(function successCallback(response) {
            GenarateExportToExcelForAllStaff(response.data.data);
        });
    };

    function GenarateExportToExcelForAllStaff(data) {

        var rows = [];
        var cellsobject = {};
        var cells = [];
        cells = [
            { value: "Customer Name", background: "#aabbcc" },
            { value: "Mobile Number", background: "#aabbcc" },
            { value: "Transiction Date", background: "#aabbcc" },
            { value: "Transiction Ref", background: "#aabbcc" },
            { value: "Start Time", background: "#aabbcc" },
            { value: "End Time", background: "#aabbcc" },
            { value: "Transiction Amount", background: "#aabbcc" },
            { value: "Device ID", background: "#aabbcc" },
            { value: "Device Name", background: "#aabbcc" },
            { value: "Location", background: "#aabbcc" },
            { value: "Location Level", background: "#aabbcc" }
        ];
        cellsobject = {
            cells: cells
        };
        rows.push(cellsobject);


        $.each(data, function (index, value) {
            cells = [
                { value: value.User.UserName },
                { value: value.User.MobileNumber },
                { value: value.Transiction.TransictionDate },
                { value: value.Transiction.Guid },
                { value: value.UserService.StartTime },
                { value: value.UserService.EndTime },
                { value: value.Transiction.TransictionAmount.toFixed(2) },
                { value: value.DeviceInformation.DeviceCode },
                { value: value.DeviceInformation.DeviceName },
                { value: value.DeviceLocation.LocationName },
                { value: value.DeviceLocation.LocationLevel }
            ];
            cellsobject = {
                cells: cells
            }
            rows.push(cellsobject);
        });


        var workbook = new kendo.ooxml.Workbook({
            sheets: [
                {
                    columns: [
                        { autoWidth: false, width: 100 },
                        { autoWidth: false, width: 100 },
                        { autoWidth: false, width: 100 },
                        { autoWidth: false, width: 100 },
                        { autoWidth: false, width: 100 },
                        { autoWidth: false, width: 100 },
                        { autoWidth: false, width: 100 },
                        { autoWidth: false, width: 100 },
                        { autoWidth: false, width: 100 },
                        { autoWidth: false, width: 100 },
                        { autoWidth: false, width: 100 }
                    ],
                    title: "Customer Service Infomation",
                    rows: rows
                }
            ]
        });
        kendo.saveAs({
            dataURI: workbook.toDataURL(),
            fileName: "Customer Service Infomation" + ".xlsx"
        });
    }

    $scope.GetTotalMinutes = function (startTime, endTime) {
        startTime = new Date(startTime);
        endTime = new Date(endTime);

        var difference = endTime.getTime() - startTime.getTime(); // This will give difference in milliseconds
        var resultInMinutes = Math.round(difference / 60000);

        return resultInMinutes; // return minutes
    };

    $scope.GetAllUserService = function (deviceId, customerId, startDate, endDate) {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/UserServiceReportApi/GetUserServiceDetailsByUserId?customerId=' + customerId + '&deviceId=' + deviceId + '&startDate=' + startDate + '&endDate' + endDate
        }).then(function successCallback(response) {
            //console.log(response);
            LoadUserChartUI(response.data.data);
        }, function errorCallback(response) {
            //console.log(response);
        });
    };

    $scope.GetAllDevicerService = function (deviceId, customerId, startDate, endDate) {
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/UserServiceReportApi/GetDeviceServiceDetailsByDeviceId?customerId=' + customerId + '&deviceId=' + deviceId + '&startDate=' + startDate + '&endDate' + endDate
        }).then(function successCallback(response) {
            LoadDeviceChartUI(response.data.data);
        }, function errorCallback(response) {

        });

    };

    $scope.GetDeviceServiceRecords = function () {
        $scope.deviceArray = [];
        $http({
            method: 'Get',
            url: BaseApiService.baseUrl() + 'api/UserServiceReportApi/GetDeviceServiceRecords'
        }).then(function successCallback(response) {
            //console.log('GetDeviceServiceRecords................');
            //console.log(JSON.stringify(response.data.data));

            $scope.DeviceServiceRecords = response.data.data;
            $scope.prevDeviceId = 0;
            $scope.prevUserId = 0;
            //find unique device list
            angular.forEach($scope.DeviceServiceRecords, function (item, index) {
                if (item.DeviceInformation.Id !== $scope.prevDeviceId) {
                    var deviceObj = {
                        DeviceId: item.DeviceInformation.Id,
                        DeviceName: item.DeviceInformation.DeviceName,
                        data: []
                    };
                    $scope.deviceArray.push(deviceObj);
                }
                $scope.prevDeviceId = item.DeviceInformation.Id;
            });
            //console.log($scope.deviceArray);

            //find device wise last 7 days data
            angular.forEach($scope.deviceArray, function (item, index) {
                $scope.innerArray = $scope.DeviceServiceRecords.filter(x => x.DeviceInformation.Id === item.DeviceId);
                var preStartDate = '';
                var totalTime = 0;
                angular.forEach($scope.innerArray, function (item, index1) {
                    if (preStartDate === '') {
                        totalTime = 0;
                    }
                    else if ($filter('date')(new Date(preStartDate), 'yyyyMMdd') !== $filter('date')(new Date(item.UserService.StartTime), 'yyyyMMdd') && index1 > 0) {
                        $scope.deviceArray[index].data.push(totalTime);
                        totalTime = 0;
                    }

                    totalTime += $scope.GetTotalMinutes(item.UserService.StartTime, item.UserService.EndTime);
                    preStartDate = item.UserService.StartTime;
                });
                $scope.deviceArray[index].data.push(totalTime);
                //console.log($scope.deviceArray);
            });
            $scope.LoadMultiAxisChart($scope.deviceArray);
        }, function errorCallback(response) {
            //console.log(response);
        });
    };

    function LoadUserChartUI(users) {
        var user_chart_context = $('#userChart');
        var userChart = new Chart(user_chart_context, {
            // The type of chart we want to create
            type: 'bar',
            // The data for our dataset
            data: {
                labels: [],
                datasets: [{
                    label: 'Total Cost',
                    data: [],
                    backgroundColor: [],
                    borderWidth: 1,
                    borderColor: 'rgba(0, 0, 132, 1)',
                    hoverBorderWidth: 2
                }]
            },

            // Configuration options go here
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: "User Wise Costing"
                },
                legend: {
                    display: false
                }
            }
        });

        //here you can call your ajax to load dynamic data to the chart
        for (var i = 0; i < users.length; i++) {
            var code = 99 + i * 15;
            userChart.data.labels.push(users[i].UserName);
            userChart.data.datasets[0].data.push(users[i].Amount.toFixed(2));
            userChart.data.datasets[0].backgroundColor.push('rgba(255,' + code + ', 132, 0.7)');
        }

        userChart.update();

        $('#userChartContainer').fadeIn();
    }

    function LoadDeviceChartUI(devices) {

        var device_chart_context = $('#deviceChart');
        var deviceChart = new Chart(device_chart_context, {
            // The type of chart we want to create
            type: 'bar',
            // The data for our dataset
            data: {
                labels: [],
                datasets: [{
                    label: 'Total Cost',
                    data: [],
                    backgroundColor: 'rgb(255, 99, 132)',
                    borderColor: 'rgb(255, 99, 132)'
                }]
            },

            // Configuration options go here
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: "Device Wise Costing"
                },
                legend: {
                    display: false
                }
            }
        });

        //here you can call your ajax to load dynamic data to the chart
        for (var i = 0; i < devices.length; i++) {
            deviceChart.data.labels.push(devices[i].DeviceName);
            //console.log(devices[i].DeviceName);
            deviceChart.data.datasets[0].data.push(devices[i].Amount.toFixed(2));
        }

        // re-render the chart
        deviceChart.update();

        $('#deviceChartContainer').fadeIn();
    }

    $scope.LoadMultiAxisChart = function (records) {
        var multiaxis_chart_context = $('#multiAxisChart');
        var multiAxisChart = new Chart.Line(multiaxis_chart_context, {
            data: {
                labels: [$scope.getNewDate(6), $scope.getNewDate(5), $scope.getNewDate(4), $scope.getNewDate(3), $scope.getNewDate(2), $scope.getNewDate(1), $scope.getNewDate(0)],
                datasets: []
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: 'Device Utilization (Last 7 Days)'
                },
                tooltips: {
                    mode: 'index',
                    intersect: false
                },
                hover: {
                    mode: 'nearest',
                    intersect: false
                },
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Days'
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: 'Minuts'
                        }
                    }]
                }
            }
        });

        //here you can call your ajax to load dynamic data to the chart
        for (var i = 0; i < records.length; i++) {
            var r = getRandomInt(0, 255);
            var g = getRandomInt(0, 255);
            var b = getRandomInt(0, 255);
            var dataObj = {
                label: records[i].DeviceName,
                backgroundColor: 'rgba(' + r + ',' + g + ', ' + b + ', 0.7)',
                borderColor: 'rgba(' + r + ',' + g + ', ' + b + ', 0.7)',
                data: records[i].data,
                fill: false
            };
            multiAxisChart.data.datasets.push(dataObj);
        }
        // re-render the chart
        multiAxisChart.update();

        $('#multiAxisChartContainer').fadeIn();
    };

    $scope.LoadSteppedLineChart = function () {
        var stepped_line_chart_context = $('#steppedLineChart');
        var steppedLineChart = new Chart.Line(stepped_line_chart_context, {
            type: 'line',
            data: $scope.steppedLineData,
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: 'Step Before Interpolation'
                }
            }
        });

        // re-render the chart
        steppedLineChart.update();

        $('#steppedLineChartContainer').fadeIn();
    };

    //it will return month and day like [Sep 16]
    $scope.getNewDate = function (days) {
        var dateOut = new Date();
        dateOut.setDate(dateOut.getDate() - days);
        return $filter('date')(new Date(dateOut), 'MMM d');
    };

    function getRandomInt(min, max) {
        return Math.floor(Math.random() * (max - min + 1)) + min;
    }
});