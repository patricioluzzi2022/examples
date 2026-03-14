import { LinealChart } from './lineal-chart.js';

let charts = [];

window.onload = () => {
    console.log('Main module loaded.');

    const connectBtn = document.getElementById('connect-socket-btn');
    const disconnectBtn = document.getElementById('disconnect-socket-btn');

    connectBtn.onclick = () => {
        connect();
    }

    disconnectBtn.onclick = () => {
        disconnect();
    }

}

async function connect() {
    const ioSource = document.getElementById('data-socket-url');
    const socketUrl = ioSource.value;

    let chart01 = new LinealChart(1, socketUrl, 'datasets-event', 'dataset_callback_event');

    chart01.createFromSocket();
    chart01.draft();

    charts.push(chart01);

    chart01.addEventListener('dataset_callback_event', (event) => {
        let chart = charts.find(c => c.chartId == `chart-${event.detail.note.id}`);
        chart.update(event.detail.dataset);
    });
      
}

async function disconnect() {
    charts.forEach(chart => {
        chart.disconnect();
    });
    charts = [];
}