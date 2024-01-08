
function formDataToJson(formData) {
    let jsonObject = {};
    for (const [key, value] of formData.entries()) {
        jsonObject[key] = value;
    }
    return JSON.stringify(jsonObject);
}
function Action(action, type, jsonData) {
    $.ajax({
        type: type,
        url: action,
        data: jsonData,
        contentType: 'application/json',
        success: function (response) {
                alert (response.message);
        },
        error: function (xhr, status, error) {
            var response = JSON.parse(xhr.responseText);
            alert(response.message);
        }
    });
}