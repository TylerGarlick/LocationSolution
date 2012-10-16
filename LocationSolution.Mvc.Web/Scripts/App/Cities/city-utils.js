function populateCitiesDropDown(stateElement, element) {
    setupCityDropDown(stateElement, element);

    $(stateElement).on("change", function () {
        setupCityDropDown(stateElement, element);
    });
}

function setupCityDropDown(stateElement, element) {
    if ($(stateElement).val().length > 0) {
        var url = element.data("url").replace("{stateId}", $(stateElement).val());
        $.getJSON(url, function (results) {
            setCityDefaultOption(element);
            $(element).prop("disabled", false);
            $.each(results, function (index, value) {
                $(element).append($("<option></option>")
                    .attr("value", value.CityId)
                    .text(value.CityName));
            });

            // Select the default when finished populating
            $(element).val($(element).data("default-value"));
        });
    } else {
        disableCityElementAndSetDefault(element);
    }
}

function disableCityElementAndSetDefault(element) {
    $(element).prop("disabled", true);
    setCityDefaultOption(element);
}
function setCityDefaultOption(element) {
    $(element).find("option").remove().end().append($("<option></option>")
        .attr("value", "")
        .text("Select a City"));
}