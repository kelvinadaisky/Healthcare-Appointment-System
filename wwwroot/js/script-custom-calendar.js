var routeURL = location.protocol + "//" + location.host;

$(document).ready(function () {
    $("#appointmentDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });

    InitializeCalendar();
});

var calendar;
function InitializeCalendar() {
    try {
        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                },
                eventDisplay: 'block',
                events: function (fetchInfo, successCallback, failureCallback) {
                    $.ajax({
                        url: routeURL + '/api/Appointment/GetCalendarData?doctorId=' + $("#doctorId").val(),
                        type: 'GET',
                        dataType: 'JSON',
                        success: function (response) {
                            if (response.status === 1) {
                                var events = response.dataenum.map(data => ({
                                    title: data.title,
                                    description: data.description,
                                    start: data.startDate,
                                    end: data.endDate,
                                    backgroundColor: data.isDoctorApproved ? "#28a745" : "#dc3545",
                                    borderColor: "#162466",
                                    textColor: "white",
                                    id: data.id
                                }));
                                successCallback(events);
                            } else {
                                $.notify(response.message, "error");
                            }
                        },
                        error: function () {
                            $.notify("Error loading calendar data", "error");
                            failureCallback();
                        }
                    });
                },
                eventClick: function (info) {
                    console.log("Event clicked:", info.event);
                    if (info.event) {
                        getEventDetailsByEventId(info.event);
                    } else {
                        console.error("Event information is missing.");
                    }
                }
            });
            calendar.render();
        }
    } catch (e) {
        alert("An error occurred while initializing the calendar: " + e.message);
    }
}

function onShowModal(obj, isEventDetail) {
    if (isEventDetail != null) {
        $("#title").val(obj.title); 
        $("#description").val(obj.description);
        $("#appointmentDate").val(obj.startDate);
        $("#duration").val(obj.duration);
        $("#doctorId").val(obj.doctorId);
        $("#patientId").val(obj.patientId);
        $("#id").val(obj.id);
        $("#lblPatientName").html(obj.patientName);
        $("#lblDoctorName").html(obj.doctorName);
        $("#lblStatus").html(obj.isDoctorApproved ? 'Approved' : 'Pending');
        $("#btnConfirm").toggleClass("d-none", obj.isDoctorApproved);
        $("#btnSubmit").removeClass("d-none");
        $("#btnDelete").removeClass("d-none");
    } else {
        $("#appointmentDate").val(obj.startStr + " " + moment().format("hh:mm A"));
        $("#id").val(0);
        $("#btnDelete").addClass("d-none");
        $("#btnSubmit").removeClass("d-none");
    }
    $("#appointmentInput").modal("show");
}

function onSubmitForm() {
    if (checkValidation()) {
        var requestData = {
            Id: parseInt($("#id").val()),
            Title: $("#title").val(),
            Description: $("#description").val(),
            StartDate: $("#appointmentDate").val(),
            Duration: $("#duration").val(),
            DoctorId: $("#doctorId").val(),
            PatientId: $("#patientId").val()
        };

        $.ajax({
            url: routeURL + '/api/Appointment/SaveCalendarData',
            type: 'POST',
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (response) {
                if (response.status === 1 || response.status === 2) {
                    calendar.refetchEvents();
                    $.notify(response.message, "success");
                    onCloseModal();
                } else {
                    $.notify(response.message, "error");
                }
            },
            error: function (xhr) {
                var errorMsg = "Error saving appointment";
                if (xhr.responseJSON && xhr.responseJSON.message) {
                    errorMsg = xhr.responseJSON.message;
                }
                $.notify(errorMsg, "error");
            }
        });
    }
}

function checkValidation() {
    var isValid = true;

    // Validate Title
    if ($("#title").val().trim() === "") {
        isValid = false;
        $("#title").addClass('error');
    } else {
        $("#title").removeClass('error');
    }

    // Validate Appointment Date
    if ($("#appointmentDate").val().trim() === "") {
        isValid = false;
        $("#appointmentDate").addClass('error');
    } else {
        $("#appointmentDate").removeClass('error');
    }

    return isValid;
}

function onCloseModal() {
    $("#appointmentInput").modal("hide");
    clearForm();
}

function clearForm() {
    $("#title").val('');
    $("#description").val('');
    $("#appointmentDate").val('');
    $("#duration").val('');
    $("#doctorId").val('');
    $("#patientId").val('');
    $("#id").val('');
    $("#lblPatientName").html('');
    $("#lblDoctorName").html('');
    $("#lblStatus").html('');
    $("#btnConfirm").addClass("d-none");
    $("#btnDelete").addClass("d-none");
}
 
function getEventDetailsByEventId(info) {
    $.ajax({
        url: routeURL + '/api/Appointment/GetCalendarDataById/' + info.id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {

            if (response.status === 1 && response.dataenum !== undefined) {
                onShowModal(response.dataenum, true);
            }
            successCallback(events);
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
}
