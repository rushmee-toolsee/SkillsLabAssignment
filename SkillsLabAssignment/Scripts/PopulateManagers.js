
function getManagers() {
    var departmentName = document.getElementById("departmentDropdown").value;
    console.log();
    const data = {
        departmentName: departmentName
    };

    $.ajax({
        url: '/Register/GetManagersByDepartment',
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        success: function (data) {
            console.log("AJAX Response:", data);
            if (data.success) {
                var managerDropdown = document.getElementById("managerDropdown");
                managerDropdown.innerHTML = "";

                data.managers.forEach(function (manager) {
                    var option = document.createElement("option");
                    option.text = manager;
                    managerDropdown.add(option);
                });
            } else {
                console.error("Error: " + data.error);
            }
        },
        error: function () {
            console.error("Failed to fetch managers");
        }
    });
}
