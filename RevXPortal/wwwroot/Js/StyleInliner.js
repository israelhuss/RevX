//var rootVariables = {};

//function ensureValue(val) {
//    if (!rootVariables) {
//        return val;
//    }

//    var reg = /var\((--[^\)]*)\)/g;
//    var matches = val.matchAll(reg);
//    for (var variable of matches) {
//        var variableName = variable[1];
//        if (variableName && rootVariables[variableName]) {
//            var newVal = rootVariables[variableName].replace(/\s/g, "");
//            if (/var\((--[^\)]*)\)/g.exec(newVal)) {
//                newVal = ensureValue(newVal);
//            }
//            var replaced = val.replace(variable[0], newVal);
//            if (variableName == "--chart-row-height") {
//                console.log("variable", variableName);
//                console.log("newVal", newVal);
//                console.log("replaced", replaced);
//            }
//            val = replaced;
//            console.log("variable", variableName);
//            console.log("val", val);
//        }
//    }
//    return val;
//}

//function applyInline(element, recursive = true) {
//    if (!element) {
//        throw new Error("No element specified.");
//    }
//    console.log(element);

//    const matches = matchRules(element);

//    // we need to preserve any pre-existing inline styles.
//    var srcRules = document.createElement(element.tagName).style;
//    srcRules.cssText = ensureValue(element.style.cssText);
//    console.log("srcRules", srcRules.cssText);
//    element.style.cssText = srcRules.cssText;

//    matches.forEach((rule) => {
//        for (var prop of rule.style) {
//            let val =
//                srcRules.getPropertyValue(prop) || rule.style.getPropertyValue(prop);
//            val = ensureValue(val);
//            let priority = rule.style.getPropertyPriority(prop);
//            element.style.setProperty(prop, val, priority);
//            console.log("prop", prop);
//            console.log("val", val);
//        }
//    });

//    if (recursive) {
//        for (var i = 0; i < element.children.length; i++) {
//            applyInline(element.children[i], recursive);
//        }
//    }
//    return element;
//}

//function matchRules(el, sheets) {
//    sheets = sheets || document.styleSheets;
//    console.log(sheets);
//    var ret = [];
//    if (el.tagName == 'style') {
//        return [];
//    }

//    for (var i in sheets) {
//        if (sheets.hasOwnProperty(i)) {
//            try {
//                var rules = sheets[i].rules || sheets[i].cssRules;
//            } catch (e) {
//                continue;
//            }
//            for (var r in rules) {
//                if (rules[r].selectorText == ":root") {
//                    var style = rules[r].style;
//                    console.log(style);
//                    for (var i = 0; i < rules[r].styleMap.size; i++) {
//                        var prop = style[i];
//                        var val = style.getPropertyValue(prop).trim();
//                        rootVariables[prop] = val;
//                        lo
//                    }
//                }
//                if (el.matches(rules[r].selectorText)) {
//                    ret.push(rules[r]);
//                }
//            }
//        }
//    }
//    //   Iterate rootVariables to resolve internal variable referencing
//    for (var s in rootVariables) {
//        rootVariables[s] = ensureValue(rootVariables[s]);
//    }
//    console.log("rootVariables", rootVariables);
//    return ret;
//}

//function normalizeElement(el) {
//    //var el = document.getElementById(id);
//    //if (!el) {
//    //    return;
//    //}
//    console.log(el);
//    var inlined = applyInline(el, false);
//    console.log(inlined);
//}


//function inlineStyles(id) {
//    var el = document.getElementById(id);
//    if (!el) {
//        return;
//    }
//    //var copyEl = document.createElement("div");
//    //copyEl.innerHTML = el.outerHTML.replace(/<\s?!\s?--\s?!\s?--\s?>/g, "");
//    var copyEl = el;
//    console.log(copyEl);
//    var inlined = applyInline(copyEl);
//    //if (inlined.tagName != "html" && inlined.tagName != "body") {
//    //    var html = document.createElement("html");
//    //    var body = document.createElement("body");
//    //    html = applyInline(html);
//    //    body = applyInline(body);
//    //    html.appendChild(body);
//    //    body.appendChild(inlined);
//    //    body.style.cssText +=
//    //        "display:flex; flex-direction:column; justify-content:center; min-height:100vh;";
//    //    var str = html.outerHTML;
//    //} else {
//        var str = inlined.outerHTML;
//    //}
//    console.log(str);
//    return str;
//}




var rootVariables = {};

function ensureValue(val) {
    if (!rootVariables) {
        return val;
    }

    var reg = /var\((--[^\)]*)\)/g;
    var matches = val.matchAll(reg);
    for (var variable of matches) {
        console.log(variable);
        var variableName = variable[1];
        if (variableName && rootVariables[variableName]) {
            var newVal = rootVariables[variableName].replace(/\s/g, "");
            if (/var\((--[^\)]*)\)/g.exec(newVal)) {
                newVal = ensureValue(newVal);
            }
            var replaced = val.replace(variable[0], newVal);
            console.log("variable", variableName);
            console.log("newVal", newVal);
            console.log("replaced", replaced);
            val = replaced;
            console.log("variable", variableName);
            console.log("val", val);
        }
    }
    return val;
}

function applyInline(element, recursive = true) {
    if (!element) {
        throw new Error("No element specified.");
    }

    const matches = matchRules(element);

    // we need to preserve any pre-existing inline styles.
    var srcRules = document.createElement(element.tagName).style;
    srcRules.cssText = ensureValue(element.style.cssText);
    console.log("srcRules", srcRules.cssText);
    element.style.cssText = srcRules.cssText;

    matches.forEach((rule) => {
        for (var prop of rule.style) {
            let val =
                srcRules.getPropertyValue(prop) || rule.style.getPropertyValue(prop);
            val = ensureValue(val);
            let priority = rule.style.getPropertyPriority(prop);
            element.style.setProperty(prop, val, priority);
            console.log("prop", prop);
            console.log("val", val);
        }
    });

    if (recursive) {
        for (var i = 0; i < element.children.length; i++) {
            applyInline(element.children[i], recursive);
        }
    }
    return element;
}

function matchRules(el, sheets) {
    sheets = sheets || document.styleSheets;
    console.log("sheets", sheets);
    var ret = [];

    for (var i in sheets) {
        if (sheets.hasOwnProperty(i)) {
            try {
                var rules = sheets[i].rules || sheets[i].cssRules;
            } catch (e) {
                continue;
            }
            for (var r in rules) {
                if (rules[r].selectorText == ":root") {
                    var style = rules[r].style;
                    for (var i = 0; i < rules[r].styleMap.size; i++) {
                        var prop = style[i];
                        var val = style.getPropertyValue(prop).trim();
                        rootVariables[prop] = val;
                    }
                }
                if (el.matches(rules[r].selectorText)) {
                    ret.push(rules[r]);
                }
            }
        }
    }
    //   Iterate rootVariables to resolve internal variable referencing
    //for (var s in rootVariables) {
    //    rootVariables[s] = ensureValue(rootVariables[s]);
    //}
    console.log("rootVariables", rootVariables);
    return ret;
}

function inlineStyles(id) {
    var el = document.getElementById(id);
    if (!el) {
        return;
    }
    var copyEl = el.cloneNode(true);
    var inlined = applyInline(copyEl);
    if (inlined.tagName != "html" && inlined.tagName != "body") {
        var html = document.createElement("html");
        var body = document.createElement("body");
        html = applyInline(html);
        body = applyInline(body);
        html.appendChild(body);
        body.appendChild(inlined);
        body.style.cssText +=
            "display:flex; flex-direction:column; justify-content:center; min-height:100vh;";
        var str = html.outerHTML;
    } else {
        var str = inlined.outerHTML;
    }
    str = str.replace(/<\s?!\s?--\s?!\s?--\s?>/g, "");
    return str;
}