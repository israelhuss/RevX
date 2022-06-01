window.closeErrorUi = () => {
    var el = document.getElementById('blazor-error-ui');
    if (el) {
        el.style.display = 'none';
    } else {
        console.log("Not Found");
    }
};


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

function getCoords(elem) {
    let box = elem.getBoundingClientRect();

    return {
        top: box.top + window.pageYOffset,
        right: box.right + window.pageXOffset,
        bottom: box.bottom + window.pageYOffset,
        left: box.left + window.pageXOffset
    };
}

window.setTooltipCords = (e) => {
    var mainEl = document.getElementsByClassName('content px-4')[0];
    var bottom = mainEl.scrollTop + window.innerHeight;
    var tooltip = document.getElementById(window.movingTooltipId);
    if (tooltip) {
        var currentTooltipCoords = getCoords(tooltip);
        var info = tooltip.getBoundingClientRect();
        console.log(bottom);
        console.log(currentTooltipCoords.bottom);
        console.log("would be ", e.srcElement.getBoundingClientRect().top + e.offsetY + info.height);
        console.log(bottom <= e.srcElement.getBoundingClientRect().top + e.offsetY + info.height);
        if (bottom <= e.srcElement.getBoundingClientRect().top + e.offsetY + info.height) {
            tooltip.style.top = e.offsetY - (info.height) + 'px';
        } else {
            tooltip.style.top = e.offsetY + 'px';
        }
        tooltip.style.left = e.offsetX + 'px';
        console.log(e);
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


function printElement(el) {
    var markup = document.documentElement.outerHTML;
    console.log(markup);
}

function print_this(elem) {
    document.body.classList.add('print-element');
    elem.classList.add('print-all');
    var path = getDomPath(elem);
    for (var i in path) {
        var el = path[i];
        el.classList.add('print')
    }
    window.print();
    //document.body.classList.remove('print-element');
    //for (var i in path) {
    //    var el = path[i];
    //    el.classList.remove('print');
    //}
}

function printById(id) {
    var el = document.getElementById(id);
    if (el) {
        print_this(el);
    } else {
        console.log("Can't find element", id);
    }
}

function getDomPath(el) {
    var stack = [];
    while (el.parentNode != null) {
        console.log(el.nodeName);
        var sibCount = 0;
        var sibIndex = 0;
        for (var i = 0; i < el.parentNode.childNodes.length; i++) {
            var sib = el.parentNode.childNodes[i];
            if (sib.nodeName == el.nodeName) {
                if (sib === el) {
                    sibIndex = sibCount;
                }
                sibCount++;
            }
        }
        stack.unshift(el);
        //if (el.hasAttribute('id') && el.id != '') {
        //    stack.unshift(el.nodeName.toLowerCase() + '#' + el.id);
        //} else if (sibCount > 1) {
        //    stack.unshift(el.nodeName.toLowerCase() + ':eq(' + sibIndex + ')');
        //} else {
        //    stack.unshift(el.nodeName.toLowerCase());
        //}
        el = el.parentNode;
    }
    return stack.slice(1); // removes the html element
}