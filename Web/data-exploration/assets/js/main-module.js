import { FrequencesChart } from './frequencies-chart.js';
import { MagnitudeChart } from './magnitud-chart.js';
import { getNotes } from './data-exploration-module.js';

let charts = [];

window.addEventListener('DOMContentLoaded', function () {
    console.log('Main module loaded.');

    const connectBtn = document.getElementById('connect-socket-btn');
    const disconnectBtn = document.getElementById('disconnect-socket-btn');

    connectBtn.addEventListener('click', function() {
        connect();
    });

    disconnectBtn.addEventListener('click', function() {
        disconnect();
    });

});

async function connect() {
    const ioSource = document.getElementById('data-socket-url');
    const socketUrl = ioSource.value;

    let gChart = new FrequencesChart('GChart', socketUrl, 'frequency-analisis-event', 'frequency-analisis-callback-event');

    gChart.createFromSocket();
    gChart.draft();

    charts.push(gChart);

    [1,2,3,4,5,6].forEach(n => {
        const chart = new MagnitudeChart(`chart-freq-${n}`, socketUrl, getNotes()[n-1]);
        chart.createFromSocket();
        chart.draft();
        charts.push(chart);
    }),

    gChart.addEventListener('magnitude-per-frequency-event', (event) => {
        let chart = charts.find(c => c.chartId == `chart-freq-${event.detail.note.id}`);
        chart.update(event.detail.magnitude);
    });
      
}

async function disconnect() {
    charts.forEach(chart => {
        chart.disconnect();
    });
    charts = [];
}