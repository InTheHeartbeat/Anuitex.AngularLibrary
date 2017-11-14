libraryModule.controller('navController', function ($scope, sharedService) {
    $scope.LibSections = ["Books", "Journals", "Newspapers"];
    $scope.FuncSections = ["ExportBooks", "ExportJournals", "ExportNewspapers", "ImportResult"];
    $scope.CurrentNav = $scope.LibSections[0];
    $scope.applyNav = function (nav) {
        $scope.CurrentNav = nav;
    }

    sharedService.Nav = $scope;
});