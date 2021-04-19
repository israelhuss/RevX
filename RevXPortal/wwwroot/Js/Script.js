
window.SetSelectOption = (elementId, index) => {
    document.getElementById(elementId).selectedIndex = index;
};

window.GetSelectOption = (elementId) => {
    return document.getElementById(elementId).selectedIndex;
};

window.GetInnerHtml = (elementId) => {
    return document.getElementById(elementId).innerHTML;
}