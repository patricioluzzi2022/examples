const axios = require('axios');
const readline = require('readline-sync');

// Rango de IP a escanear (ajustalo a tu red)
const BASE = "192.168.100.";

async function isRoku(ip) {
  try {
    const res = await axios.get(`http://${ip}:8060/query/device-info`, { timeout: 500 });
    return res.status === 200;
  } catch {
    return false;
  }
}

async function scanNetwork() {
  console.log("Buscando dispositivos Roku en la red...\n");
  const devices = [];

  for (let i = 1; i < 255; i++) {
    const ip = BASE + i;
    if (await isRoku(ip)) {
      console.log("✔ Roku encontrado:", ip);
      devices.push(ip);
    }
  }

  if (devices.length === 0) {
    console.log("No se encontraron dispositivos Roku.");
    process.exit();
  }

  return devices;
}

async function sendCommand(ip, path) {
  try {
    await axios.post(`http://${ip}:8060${path}`);
    console.log("Comando enviado:", path);
  } catch (err) {
    console.error("Error enviando comando:", err.message);
  }
}

async function main() {
  const devices = await scanNetwork();

  console.log("\nDispositivos encontrados:");
  devices.forEach((ip, i) => console.log(`${i + 1}. ${ip}`));

  const index = readline.questionInt("\nElegí un dispositivo: ") - 1;
  const ip = devices[index];

  console.log("\nAcciones disponibles:");
  console.log("1. Abrir YouTube");
  console.log("2. Reproducir video de YouTube");
  console.log("3. Enviar tecla (Home, Up, Down, Left, Right, Select)");
  console.log("4. Reproducir media directa (URL MP4)");

  const action = readline.questionInt("\nElegí una acción: ");

  switch (action) {
    case 1:
      await sendCommand(ip, "/launch/837");
      break;

    case 2:
      const videoId = readline.question("ID de YouTube: ");
      await sendCommand(ip, `/launch/837?contentID=${videoId}&v=${videoId}`);
      break;

    case 3:
      const key = readline.question("Tecla (Home, Up, Down, Left, Right, Select): ");
      await sendCommand(ip, `/keypress/${key}`);
      break;

    case 4:
      const url = readline.question("URL del MP4: ");
      await sendCommand(ip, `/input/15985?t=v&u=${encodeURIComponent(url)}`);
      break;

    default:
      console.log("Opción inválida.");
  }
}

main();