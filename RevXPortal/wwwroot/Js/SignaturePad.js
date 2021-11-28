var paintCanvas;
let isMouseDown = false;

function initializeCanvas() {
    paintCanvas = document.getElementById('signature-pad');
    const context = paintCanvas.getContext('2d');
    context.lineCap = 'round';
    context.lineWidth = 2;

    let x = 0, y = 0;

    const stopDrawing = () => {
        isMouseDown = false;
        // console.log(signatureDataUrl);
    };

    const startDrawing = event => {
        isMouseDown = true;
        [x, y] = [event.offsetX, event.offsetY];
    };

    const drawLine = event => {
        if (isMouseDown) {
            const newX = event.offsetX;
            const newY = event.offsetY;
            context.beginPath();
            context.moveTo(x + .5, y);
            context.lineTo(newX + .5, newY);
            context.stroke();
            //[x, y] = [newX, newY];
            x = newX;
            y = newY;
        }
    };

    paintCanvas.addEventListener('mousedown', startDrawing);
    paintCanvas.addEventListener('mousemove', drawLine);
    paintCanvas.addEventListener('mouseup', stopDrawing);
    //paintCanvas.addEventListener('mouseout', stopDrawing);

    console.log("Canvas set up complete.");
}
function getSignatureData() {
    isMouseDown = false;
    return cropImageFromCanvas(paintCanvas.getContext("2d"));
}

function clearCanvas() {
    paintCanvas.getContext('2d').clearRect(0, 0, 300, 90)
    return paintCanvas.toDataURL();
}

function download() {
    window.location = paintCanvas.toDataURL("image/png");
}


function cropImageFromCanvas(ctx) {
    var canvas = ctx.canvas,
        w = canvas.width,
        h = canvas.height,
        pix = { x: [], y: [] },
        imageData = ctx.getImageData(0, 0, canvas.width, canvas.height),
        x,
        y,
        index;

    for (y = 0; y < h; y++) {
        for (x = 0; x < w; x++) {
            index = (y * w + x) * 4;
            if (imageData.data[index + 3] > 0) {
                pix.x.push(x);
                pix.y.push(y);
            }
        }
    }
    pix.x.sort(function (a, b) {
        return a - b;
    });
    pix.y.sort(function (a, b) {
        return a - b;
    });
    var n = pix.x.length - 1;

    w = 1 + pix.x[n] - pix.x[0];
    h = 1 + pix.y[n] - pix.y[0];
    try {
        var cut = ctx.getImageData(pix.x[0], pix.y[0], w, h);

        // Create new image
        var hiddenCanvas = document.createElement("canvas");
        hiddenCanvas.width = w;
        hiddenCanvas.height = h;
        // Copy the image contents to the canvas
        var ctx = hiddenCanvas.getContext("2d");
        ctx.putImageData(cut, 0, 0);
        var dataURL = hiddenCanvas.toDataURL("image/png");
        return dataURL.replace(/^data:image\/(png|jpg);base64,/, "");
    } catch {
        return "";
    }
}