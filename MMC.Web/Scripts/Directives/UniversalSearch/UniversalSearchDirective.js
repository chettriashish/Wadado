/// outer container that displays the selected Item
/// a place holder that shows initial message and displays the selected Item
/// a text field that users can type into in order to filter the list
/// a list that displays the list of available items
/// an option to navigate using the up/down arrow keys
/// an option where users can select an item by clicking the "Enter key"
/// and option where users can select an item on the mouse down 
/// disabling right click
/// highlight selected item in list/highlight item mouse over
/// option to wait for a customizable number of seconds/key strokes before making call to server to filter list further
/// option to make it a type ahead/ autocomplete dropdown list
/// Author: Ashish Chettri

(function () {
    "use strict";
    var uniSearch = angular.module("uni-search", []);
    var universalSearch = function () {
        return {
            templateUrl: "../../Templates/_universalSearch.html",
            restrict: 'E',
            scope: {
                searchResults: "=",
                callback: "&ndsOnItemSelected",
                itemSelected: "=",
                minWordLengthForFilter: "@",
                callFilter: "&ndsBeginFilter",
                searchByTags: "@",
                getResults: "&ndsGetFilteredResults"
            },
            link: function (scope, el, attrs) {
                scope.$watch("searchResults", function (newValue, oldValue) {
                    if (oldValue != newValue && newValue.length > 0) {
                        scope.displaySearch.css = "search-results-show";
                    }
                    else if (newValue.length > 0 && scope.displaySearch.css != "search-results-show") {
                        scope.displaySearch.css = "search-results-show";
                    }
                    else if (newValue.length == 0) {
                        if (scope.displaySearch.css != "search-results-hide") {
                            scope.displaySearch.css = "search-results-hide";
                        }
                    }
                })
            },
            controller: function ($scope) {
                var vm = this;
                $scope.oldValue = null;
                var setInitValues = function () {
                    $scope.term = {
                        selected: ""
                    };
                    $scope.searchResults = {};
                    $scope.initWordLengthSet = false;
                    $scope.displaySearch = { css: "search-results-hide" };                    
                }

                var checkStringLength = function () {
                    if ($scope.term.selected.length > parseInt($scope.minWordLengthForFilter)) {
                        $scope.initWordLengthSet = true;                        
                        if ($scope.oldValue == null || $scope.oldValue != $scope.term.selected) {
                            $scope.oldValue = $scope.term.selected;
                            $scope.getResults({
                                filter: $scope.term
                            });
                        }
                        
                    }
                    else if ($scope.initWordLengthSet) {
                        if ($scope.term.selected.length == 0) {
                            setInitValues();
                        }
                        else {
                            $scope.getResults({
                                filter: $scope.term
                            });
                        }
                    }
                }
                $scope.onKeyUp = function (event) {
                    event.preventDefault();
                    checkStringLength();
                }

                vm.setSelectedValue = function (val) {
                    setInitValues();
                    //resetting old search term
                    $scope.oldValue = null;
                    $scope.term.selected = val;
                    if (val) {
                        $scope.callback({
                            filter: $scope.term.selected
                        });
                    }
                }

                vm.clickOutside = function () {
                    $scope.searchResults = {};
                    $scope.displaySearch = { css: "search-results-hide" };
                }
                setInitValues();
            }
        }
    }

    var ndsSetFocus = function () {
        return {
            require: "^ndsUniSearch",
            link: function (scope, elem, attrs, ndsSearchController) {
                var selector = attrs.ndsFocus;
                var setSelectedValue = function (filter) {
                    scope.$apply(ndsSearchController.setSelectedValue(filter.value));
                    //var content = filter.textContent == "" ? filter.value : filter.textContent;
                    //scope.$apply(searchController.setSelectedValue(content));
                }
                elem.on('keyup', selector, function (e) {
                    var atoms = elem.find(selector),
                        toAtom = null;

                    for (var i = atoms.length - 1; i >= 0; i--) {
                        if (atoms[i] === e.target) {
                            if (e.keyCode === 38) {
                                toAtom = atoms[i - 1];
                            }
                            else if (e.keyCode === 40) {
                                toAtom = atoms[i + 1];
                            }
                            else if (e.keyCode === 13) {
                                toAtom = atoms[i];
                                setSelectedValue(toAtom);
                                atoms[0].focus();
                            }
                            break;
                        }
                    }
                    if (toAtom) toAtom.focus();
                });
                elem.on("contextmenu", function (e) {
                    e.preventDefault();
                });
                elem.on('mouseup', selector, function (e) {
                    var atoms = elem.find(selector),
                        toAtom = null;
                    for (var i = atoms.length - 1; i >= 0; i--) {
                        if (atoms[i] === e.target) {
                            toAtom = atoms[i];
                            setSelectedValue(toAtom);
                            atoms[0].focus(); break;
                        }
                    }
                });
                elem.on('keydown', selector, function (e) {
                    if (e.keyCode === 38 || e.keyCode === 40)
                        e.preventDefault();
                });
            }
        }
    }

    var clickOutside = function () {
        return {
            restrict: 'A',
            require: '^ndsUniSearch',
            link: function (scope, el, attrs, ndsSearchController) {
                $(document).on('click', function (e) {
                    if (el !== e.target && !el[0].contains(e.target)) {
                        scope.$apply(function () {
                            ndsSearchController.clickOutside();
                        });
                    }
                });
            }
        }
    }

    uniSearch.directive('ndsFocus', [ndsSetFocus]);
    uniSearch.directive("ndsUniSearch", [universalSearch]);
    uniSearch.directive('ndsClickOutside', [clickOutside]);
}());