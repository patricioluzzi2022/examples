import { parseData, parseResponse, getParametersFromDatasets } from './data-exploration-module.js';
import { draftChart } from './chart-module.js';

let socket = null;
let charts = [];
let refresh_state = false;

window.addEventListener('DOMContentLoaded', function () {
    console.log('Main module loaded.');

    const connectBtn = document.getElementById('connect-socket-btn');
    const disconnectBtn = document.getElementById('disconnect-socket-btn');
    const refreshBtn = document.getElementById('refresh-socket-btn');

    connectBtn.addEventListener('click', function() {
        connect();
    });

    disconnectBtn.addEventListener('click', function() {
        disconnect();
    });

    refreshBtn.addEventListener('click', function() {
        refresh_state = !refresh_state;
    });

    this.setInterval(() => {
        if(socket && socket.connected && refresh_state ) {
            refresh();
        }
    }, 1618);

    const exampleChartBtn = document.getElementById('example-chart-btn');
    
    exampleChartBtn.addEventListener('click', async function() {
        draftChart_old();
    });
});

async function connect() {
    const ioSource = document.getElementById('data-socket-url');
    const socketUrl = ioSource.value;
    socket = io(socketUrl);

    socket.on("connected-event", async (data) => {
        console.log('Connected to socket:', data);
        charts.push({ title: data.layout.title, layout: {}, datasets: [] });

        const { date, layout, datasets } = await parseData(charts, data.layout);

        draftChart(layout, datasets, 'myChart');
        
    });

    socket.on("new-layout-callback-event", async (data) => {
        refresh_state = !refresh_state;

        const { date, layout, datasets } = await parseResponse(charts, data.response);

        for(let i = 0; datasets[0].data.length - i >= 12; i++){
            console.log(datasets[0].data.slice(i, 12 + i));

            const newDataset = [{
                backgroundColor: datasets[0].backgroundColor,
                borderColor: datasets[0].borderColor,
                data: datasets[0].data.slice(i, 12 + i),
                fill: datasets[0].fill,
                label: datasets[0].label,
                tension: datasets[0].tension
            }];

            draftChart(layout, newDataset, 'myChart');

            setTimeout(() => {}, 400);
        }

        getParametersFromDatasets(datasets[0].data);

        refresh_state = !refresh_state;
        
    });
}

async function disconnect() {
    socket.disconnect();
}

async function refresh() {
    socket.emit("get-latest-layout-event", { layout: charts[0].title });

}