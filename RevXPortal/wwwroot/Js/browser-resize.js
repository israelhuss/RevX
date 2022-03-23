var resizeTimer;
window.browserResize = {
    getInnerHeight: function () {
        return window.innerHeight;
    },
    getInnerWidth: function () {
        return window.innerWidth;
    },
    registerResizeCallback: function () {
        window.addEventListener("resize", browserResize.resized);
    },
    resized: function () {
        clearTimeout(resizeTimer);
        resizeTimer = setTimeout(() => {
            DotNet.invokeMethodAsync("RevXPortal", 'OnBrowserResize').then(data => data);
        }, 150);
    }
};