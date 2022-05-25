window.closeErrorUi = () => {
    var el = document.getElementById('blazor-error-ui');
    if (el) {
         el.style.display = 'none';
    } else {
        console.log("Not Found");
    }
}


window.SetSelectOption = (elementId, index) => {
    var el = document.getElementById(elementId);
    if (el) {
        el.selectedIndex = index;
    } else {
        console.log(`Didn't find ${elementId}`);
    }
};

window.GetSelectOption = (elementId) => {
    return document.getElementById(elementId).selectedIndex;
};

window.GetInnerHtml = (elementId) => {
    return document.getElementById(elementId).innerHTML;
};

window.log = (val) => {
    console.log(val);
};

window.SetPath = (elementId, path) => {
    document.getElementById(elementId).setAttribute('d', path);
};

window.setTooltipCords = (e) => {
    var tooltip = document.getElementById(window.movingTooltipId);
    if (tooltip) {
        tooltip.style.left = e.offsetX + 5 + 'px';
        tooltip.style.top = e.offsetY + 5 + 'px';
    }
};

window.movingTooltipId;

setUpMovingTooltip = (id) => {
    window.movingTooltipId = id;
    window.addEventListener('mousemove', setTooltipCords, false);
};

removeMovingTooltip = (id) => {
    window.movingTooltipId = null;
    window.removeEventListener('mousemove', setTooltipCords, false);
};


window.getActualValue = (id, prop) => {
    var element = document.getElementById(id);
    if (element) {
        var t = element[prop];
        return t;
    }
    else {
        return 0;
    }
};

window.documentClick = {
    registerClickCallback: function () {
        window.addEventListener("click", documentClicked);
    },
    removeClickListener: function () {
        window.removeEventListener("click", documentClicked);
    }
};

function documentClicked(e) {
    console.log(e);
    DotNet.invokeMethodAsync("RevXPortal", 'OnDocumentClicked', { targetClassName: e.target.className, parentClassNames: e.path.map(p => p.className) });
}

function checkOverflow(el) {
    var curOverflow = el.style.overflow;

    if (!curOverflow || curOverflow === "visible")
        el.style.overflow = "hidden";

    var isOverflowing = el.clientWidth < el.scrollWidth
        || el.clientHeight < el.scrollHeight;

    el.style.overflow = curOverflow;

    return isOverflowing;
}