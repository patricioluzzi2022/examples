const express = require('express');
const fs = require('fs');
const path = require('path');
const { spawn } = require('child_process');
const cors = require('cors');

const app = express();
app.use(cors());
app.use(express.json());

const MEDIA_DIR = path.join(__dirname, 'media');
const HLS_DIR = path.join(__dirname, 'public/hls');
const PLAYLIST_FILE = path.join(__dirname, 'playlist.txt');

let ffmpegProcess = null;

/**
 * Genera playlist.txt
 */
function generatePlaylist(videos) {
  const content = videos
    .map(v => `file '${path.join('media', v)}'`)
    .join('\n');

  fs.writeFileSync(PLAYLIST_FILE, content);
}

/**
 * Inicia FFmpeg
 */
function startFFmpeg(loop) {
  if (ffmpegProcess) {
    ffmpegProcess.kill('SIGKILL');
  }

  const args = [
    '-re',
    ...(loop ? ['-stream_loop', '-1'] : []),
    '-f', 'concat',
    '-safe', '0',
    '-i', 'playlist.txt',
    '-c:v', 'libx264',
    '-c:a', 'aac',
    '-preset', 'veryfast',
    '-tune', 'zerolatency',
    '-f', 'hls',
    '-hls_time', '3',
    '-hls_list_size', '6',
    '-hls_flags', 'delete_segments+append_list',
    path.join(HLS_DIR, 'live.m3u8')
  ];

  ffmpegProcess = spawn('ffmpeg', args);

  ffmpegProcess.stderr.on('data', d =>
    console.log(`[ffmpeg] ${d}`)
  );
}

/**
 * START STREAM
 */
app.get('/api/stream/start', (req, res) => {
  const videos = ["example01.mp4"];
  const loop = true;

  if (!Array.isArray(videos) || videos.length === 0) {
    return res.status(400).json({ error: 'Lista de videos requerida' });
  }

  // validar existencia
  const invalid = videos.find(v => !fs.existsSync(path.join(MEDIA_DIR, v)));
  if (invalid) {
    return res.status(400).json({ error: `No existe ${invalid}` });
  }

  generatePlaylist(videos);
  startFFmpeg(loop);

  res.json({
    status: 'stream started',
    url: 'http://localhost:3000/hls/live.m3u8'
  });
});

/**
 * STOP STREAM
 */
app.post('/api/stream/stop', (_req, res) => {
  if (ffmpegProcess) {
    ffmpegProcess.kill('SIGKILL');
    ffmpegProcess = null;
  }
  res.json({ status: 'stream stopped' });
});

/**
 * Servir HLS
 */
app.use('/hls', express.static(HLS_DIR, {
  setHeaders: res => {
    res.setHeader('Cache-Control', 'no-cache');
  }
}));

app.listen(3000, () =>
  console.log('Server running on http://localhost:3000')
);
