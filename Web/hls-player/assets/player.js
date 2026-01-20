const video = document.getElementById("video");
const streamInput = document.getElementById("streamUrl");
const loadBtn = document.getElementById("loadBtn");

// Función para cargar un stream HLS
function loadStream(url) {
    if (Hls.isSupported()) {
        const hls = new Hls();
        hls.loadSource(url);
        hls.attachMedia(video);
        hls.on(Hls.Events.MANIFEST_PARSED, () => {
            video.play();
        });
    } else if (video.canPlayType("application/vnd.apple.mpegurl")) {
        // Safari soporta HLS nativo
        video.src = url;
        video.play();
    } else {
        alert("Tu navegador no soporta HLS");
    }
}

loadBtn.addEventListener("click", () => {
    const url = streamInput.value;
    loadStream(url);
});

// Cargar stream por defecto al iniciar
loadStream(streamInput.value);
