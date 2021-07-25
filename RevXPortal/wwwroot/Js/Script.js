window.SetSelectOption = (elementId, index) => {
    document.getElementById(elementId).selectedIndex = index;
};

window.GetSelectOption = (elementId) => {
    return document.getElementById(elementId).selectedIndex;
};

window.GetInnerHtml = (elementId) => {
    return document.getElementById(elementId).innerHTML;
}

window.log = (val) => {
    console.log(val);
}

window.SetPath = (elementId, path) => {
    document.getElementById(elementId).setAttribute('d', path);
}