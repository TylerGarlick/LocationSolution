$(function () {
    populateCountriesDropDown($("#CountryCode"),
        function () {
            populateStatesDropDown($("#CountryCode"), $("#StateId"), function () {
                populateCitiesDropDown($("#StateId"), $("#CityId"));
            });
        });
});