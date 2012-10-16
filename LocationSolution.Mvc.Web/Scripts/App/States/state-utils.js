function populateStatesDropDown(countryElement, element, onCompleted) {
    setupStateDropDown(countryElement, element, onCompleted);

    $(countryElement).on("change", function () {
        setupStateDropDown(countryElement, element, onCompleted);
    });
}

function setupStateDropDown(countryElement, element, onCompleted) {
    if ($(countryElement).val().length > 0) {
        var url = element.data("url").replace("{countryCode}", $(countryElement).val());
        $.getJSON(url, function (results) {
            setStateDefaultOption(element);
            $(element).prop("disabled", false);
            $.each(results, function (index, value) {
                $(element).append($("<option></option>")
                    .attr("value", value.StateId)
                    .text(value.StateName));
            });

            // Select the default when finished populating
            $(element).val($(element).data("default-value"));
            
            if (onCompleted != null) {
                onCompleted();
            }
        });
    } else {
        disableStateElementAndSetDefault(element);
        
        if (onCompleted != null) {
            onCompleted();
        }
    }
}

function disableStateElementAndSetDefault(element) {
    $(element).prop("disabled", true);
    setStateDefaultOption(element);
}
function setStateDefaultOption(element) {
    $(element).find("option").remove().end().append($("<option></option>")
        .attr("value", "")
        .text("Select a State"));
}