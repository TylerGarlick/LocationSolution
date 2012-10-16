$(function () {
    populateCountriesDropDown($("#CountryCode"),
        function () {
            populateStatesDropDown($("#CountryCode"), $("#StateId"));
        });
});