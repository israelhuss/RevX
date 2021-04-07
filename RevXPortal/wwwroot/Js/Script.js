var focused;
window.isFocused = (element) => {
    focused = element;
};

window.ContainerClicked = (element) => {
    console.log(focused, element.children, document.activeElement.classList);
    if (focused != undefined && focused.classList.contains("time-input")) {
        return true;
    } else {
        return false;
    }

    //element.focus();
};

window.SelectOption = (elementId, index) => {
    document.getElementById(elementId).selectedIndex = index;
    
};