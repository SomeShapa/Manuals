                function handleFileSelect(evt) {
                    evt.stopPropagation();
                    evt.preventDefault();
                    var files = evt.dataTransfer.files;
                    for (var i = 0, f; f = files[i]; i++) {
                        if (!f.type.match('image.*')) { continue; }
                        var reader = new FileReader();
                        reader.onload = (function (theFile) {
                            return function (e) {
                                var fd = new FormData();
                                fd.append("image", theFile);
                                var xhr = new XMLHttpRequest();
                               xhr.open("POST", "https://api.imgur.com/3/image"); // Boooom!
                               xhr.onload = function () {
                                   evt.target.innerHTML = ['<img class="thumb" src="', JSON.parse(xhr.responseText).data.link, '" crossOrigin = "Anonymous" />'].join('');
                               }
                               xhr.setRequestHeader('Authorization', 'Client-ID 562a647a2d3b8ea');
                               xhr.send(fd);
                            };
                        })(f);
                        reader.readAsDataURL(f);
                    }
                }
function handleDragOver(evt) {
    evt.stopPropagation();
    evt.preventDefault();
    evt.dataTransfer.dropEffect = 'copy';
}
var dropZone1 = document.getElementById('Image1');
var dropZone2 = document.getElementById('Image2');
var dropZone3 = document.getElementById('Image3');

dropZone1.addEventListener('dragover', handleDragOver, false);
dropZone1.addEventListener('drop', handleFileSelect, false);
dropZone2.addEventListener('dragover', handleDragOver, false);
dropZone2.addEventListener('drop', handleFileSelect, false);
dropZone3.addEventListener('dragover', handleDragOver, false);
dropZone3.addEventListener('drop', handleFileSelect, false);
