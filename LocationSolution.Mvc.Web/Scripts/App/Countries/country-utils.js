function populateCountriesDropDown(element, onCompleted) {
    $.getJSON(element.data("url"), function (results) {

        $.each(results, function (index, value) {
            $(element).append($("<option></option>")
                .attr("value", value.CountryCode)
                .text(value.CountryName));
        });

        // Select the default when finished populating
        $(element).val($(element).data("default-value"));
        if (onCompleted != null) {
            onCompleted();
        }
    });
}