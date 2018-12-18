'use restrict';

angular.module('ElevatorApp', [])
    .controller('ElevatorCtrl', function ($scope, $http) {
        $scope.floors = [];
        $scope.floorList = [];
        $scope.errorMessage = [];

        //Load Floor using floor api
        $scope.loadFloors = function () {
            var url = baseUrl + "/api/floorapi";
            $http.get(url).success(function (data) {
              $.each(data, function (ix, floor) {
               
                    $scope.floorList.push(ix - 2);
                    $scope.errorMessage.push('');
                    $scope.floors.push(floor);
                });
            });
        };

        $scope.validate = function(ix) {
            var rexp = new RegExp(/^([1-9]|[1-1][0-9]|[2-2][0-0])$/);
            $scope.errorMessage[ix] = '';
            //validate if total people are empty
            if ($scope.floors[ix].TotalPeople.length == 0) {
                $scope.errorMessage[ix] = 'Required';
            }

            //only upto 20 allowed
            if (!rexp.test($scope.floors[ix].TotalPeople)) {
                $scope.errorMessage[ix] = 'invalid number/upto 20 only';
            }
        };

        $scope.validateSameFloor = function (ix) {
            $scope.errorMessage[ix] = '';
            if ($scope.floors[ix].CurrentFloor == $scope.floors[ix].DestinationFloor) {
                $scope.errorMessage[ix] = 'Invalid Destination';
            }
        };


        //operate elevator upon request
        $scope.run = function (ix) {
            var url = baseUrl + "/api/floorapi";

            //Post current floor data
            var data = { CurrentFloor: $scope.floors[ix].CurrentFloor, DestinationFloor: $scope.floors[ix].DestinationFloor, TotalPeople: $scope.floors[ix].TotalPeople };
            $.ajax({
                type: "POST",
                url: url,
                contentType: "application/json",
                data: angular.toJson(data),
                success: function () {

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.responseText);
                }
            });
        };

    });

