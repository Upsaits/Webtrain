$(document).ready(function () {
    LeavePageQuestion();
});

function LeavePageQuestion() {
    window.addEventListener("beforeunload", function (e) {
        var confirmationMessage = "Do you want to leave this page?";
        e.returnValue = confirmationMessage;     // Gecko and Trident
        return confirmationMessage;              // Gecko and WebKit

    });

    window.addEventListener('unload', function (e) {
        alert('ende');
        App.AppExit(e.currentTarget.Wisej.Core.session.id);
    });
}