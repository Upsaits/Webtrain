function onCustomCommandExecuted(s, e) {
    switch (e.commandName) {
        case "Save":
            alert('tests');
            richEdit.commands.fileSaveAsDialog.execute();
            break;
    }
}

function onDocumentChanged(s, e) {
    checkOnChanges();
}

function onEndCallback(s, e) {
    checkOnChanges();
}

function checkOnChanges() {
    var currectState = richEdit.commands.fileSave.getState();
    var saveButton = richEdit.GetRibbon().GetItemByName("Save");
    saveButton.SetEnabled(currectState.enabled);
}