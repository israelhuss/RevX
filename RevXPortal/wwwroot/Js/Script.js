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




window.setTooltipCords = (e) => {
    if (typeof (e.target.className) == 'object' && e.target.className.animVal == 'path') {
        var tooltip = document.getElementById('m-tooltip');
        tooltip.style.left = e.pageX + 15 + 'px';
        tooltip.style.top = e.pageY + 15 + 'px';
    }
}

window.addEventListener('mousemove', setTooltipCords, false);
