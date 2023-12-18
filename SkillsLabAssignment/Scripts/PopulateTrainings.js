$(document).ready(function () {
    var originalTrainingData;
    var filteredTrainingData;

    function fetchTrainingData() {
        $.ajax({
            url: '/Training/GetAllTrainingJson',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                originalTrainingData = data.trainings;
                filteredTrainingData = originalTrainingData;
                displayTrainingData(filteredTrainingData);             
            },
            error: function (xhr, status, error) {
                console.error('Error fetching training data:', error);
                console.log(xhr.responseText);
            }
        });
    }
    function displayTrainingData(data) {
        var trainingTable = $('#trainingTable');
        trainingTable.empty();

        var tableHeader = `
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Description</th>
                    <th>Actions</th>
                </tr>
            </thead>
        `;
        trainingTable.append(tableHeader);

        var tableBody = $('<tbody></tbody>');

        data.forEach(function (training) {
            var row = $('<tr></tr>');
            row.append(`<td>${training["Title"]}</td>`);
            row.append(`<td>${training["Description"]}</td>`);
            var actionsHtml = `
                <td>
                    <button class="btn btn-primary" onclick="applyForTraining(${training["TrainingId"]})">Apply</button>
                    <button class="btn btn-info" onclick="openTrainingDetailsModal(${training["TrainingId"]})">View</button>
                </td>
            `;
            row.append(actionsHtml);
            tableBody.append(row);
        });
        trainingTable.append(tableBody);
    }

    function searchTraining() {
        var searchTerm = $('#searchInput').val().toLowerCase();

        if (searchTerm === "") {
            filteredTrainingData = originalTrainingData;
        } else {
            filteredTrainingData = originalTrainingData.filter(function (training) {
                return training['Title'].toLowerCase().includes(searchTerm);
            });
        }

        displayTrainingData(filteredTrainingData);
        }

        $('#searchInput').on('input', searchTraining);
        fetchTrainingData();
    });

 

function openTrainingDetailsModal(trainingId) {
    $.ajax({
        url: '/Training/GetTrainingDetails',
        type: 'GET',
        dataType: 'json',
        data: { trainingId: trainingId },
        success: function (data) {
            $('#trainingDetailsTitle').html( data.Title);
            $('#trainingDetailsDescription').html('<b>Description:</b> ' + data.Description);
            $('#trainingDetailsPreRequisite').html('<b>Prerequisites:</b> ' + data.PreRequisites);
            var deadlineMilliseconds = parseInt(data.Deadline.substr(6));
            var formattedDeadline = new Date(deadlineMilliseconds).toLocaleDateString();
            $('#trainingDetailsDeadline').html('<b>Deadline:</b> ' + formattedDeadline);

            $('#trainingDetailsSeat').html('<b>Seat Threshold:</b> ' + data.SeatThreshold);

            var trainingDateMilliseconds = parseInt(data.TrainingDate.substr(6));
            var formattedTrainingDate = new Date(trainingDateMilliseconds).toLocaleDateString();
            $('#trainingDetailsDate').html('<b>Training Date:</b> ' + formattedTrainingDate);

            $('#trainingDetailsModal').modal('show');
        },


        error: function (xhr, status, error) {
            console.error('Error fetching training details:', error);
            console.log(xhr.responseText);
        }
    });
}

function applyForTraining(trainingId) {
    $.ajax({
        url: '/Training/GetTrainingDetails',
        type: 'GET',
        dataType: 'json',
        data: { trainingId: trainingId },
        success: function (data) {
            $('#applyTrainingTitle').html('You are applying for: ' + data.Title);

            $('#applyModal').data('trainingId', trainingId);

            $('#applyModal').modal('show');
        },
        error: function (xhr, status, error) {
            console.error('Error fetching training details:', error);
            console.log(xhr.responseText);
        }
    });
}
function submitApplication() {
    var trainingId = $('#applyModal').data('trainingId');

    var fileInput = document.getElementById('fileInput');
    var file = fileInput.files[0];

    var formData = new FormData();
    formData.append('file', file);

    $.ajax({
        url: '/TrainingApplication/ApplyForTraining?trainingId=' + trainingId,
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            if (data.success) {
                alert(data.message);
                $('#applyModal').modal('hide');
            } else {
                alert(data.message);
            }
        },
        error: function (xhr, status, error) {
            console.error('Error applying for training:', error);
            console.log(xhr.responseText);
        }
    });
}