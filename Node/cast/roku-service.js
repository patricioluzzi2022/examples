
const axios = require('axios');
const readline = require('readline-sync');

// Rango de IP a escanear (ajustalo a tu red)
const IP = "192.168.100.55";

async function isRoku(ip) {
  try {
    const res = await axios.get(`http://${ip}:8060/query/device-info`, { timeout: 500 });
    return res.status === 200;
  } catch {
    return false;
  }
}

async function sendCommand(ip, path) {
  try {
    await axios.post(`http://${ip}:8060${path}`).then( result => {
      console.log(result);
    });
    console.log("Comando enviado:", path);
  } catch (err) {
    console.error("Error enviando comando:", err.message);
  }
}

async function hasRokuMediaPlayer(ip) {
  try {
    console.log("Media Player Info")
    const res = await axios.get(`http://${ip}:8060/query/apps`);
    return res.data.includes('id="15985"');
  } catch (err) {
    console.error("Error enviando comando:", err.message);
  }
}

async function main() {
  if(! isRoku(IP)){ console.log(`No se encontro un dicpositivo Roku en la IP: ${IP}`)}

  console.log("\nAcciones disponibles:");
  console.log("1. Reproducir video de YouTube");
  console.log("2. Reproducir media directa (URL MP4)");

  const action = readline.questionInt("\nElegí una acción: ");

  switch (action) {
    case 1:
      const videoId = readline.question("ID de YouTube: ");
      await sendCommand(IP, `/launch/837?contentID=${videoId}&v=${videoId}`);
      break;

    case 2:
      const url = readline.question("URL: ");
      await sendCommand(IP, `/launch/15985?contentID=${encodeURIComponent(url)}&mediaType=movie`);
      break;
    default:
      console.log("Opción inválida.");
  }
}

main();