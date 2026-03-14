import { draftChart } from './chart-module.js';

export class LinealChart extends EventTarget{
    constructor(chartId, source, getDataEvent, callbackEvent, ms = null){
        super();
        this.chartId = chartId;
        this.source = source;
        this.socket = null;
        this.getdata = getDataEvent
        this.callback = callbackEvent;
        this.chart = undefined;
        this.datasets = [];
        this.ms = ms;
    }

    createFromSocket(){
        this.socket = io(this.source);

        this.socket.on("connected-event", async (result) => {
            console.log(result);
        });

        this.socket.on("layout-callback-event", async (result) => {
            this.layout = {
                'title': result.layout.title || undefined,
                'type': result.layout.type || 'line',
                'axis': result.layout.axis || undefined,
                'plot_bg_color': result.layout.plot_bg_color || undefined,
                'paper_bg_color': result.layout.paper_bg_color || undefined,
                'intersect': result.layout.intersect || false,
                'range': result.layout.range || [0, 105]
            };

            this.datasets = result.layout.datasets;

            if(!this.ms){
                this.ms = result.layout.ms;
            }

            this.socket.emit(this.getdata, { 'layout': this.chartId, 'params': { "y": "f(x)", "x": 0 } });
        });

        this.socket.on(this.callback, async (result) => {
            this.chart = await draftChart(this.layout, this.datasets, this.chartId);

            let next = await this.update(result.dataset);

            this.socket.emit(this.getdata, { 'layout': this.chartId, 'params': { "y": "f(x)", "x": next } });

        });
        
    }

    draft(){
        this.socket.emit("layout-event", this.chartId);
    }

    disconnect() {
        this.socket.disconnect();
    }

    async update(dataset){
        let idle = (ms) => new Promise(resolve => setTimeout(resolve, ms));

        const data = dataset.map((f, key) => f[Object.keys(f)[1]])

        for (let p of data) { // fk1_data.length in [1,12]
            this.chart.config._config.data.datasets[0].data = this.chart.config._config.data.datasets[0].data.slice(1).concat(p);
            this.chart.update();
            await idle(this.ms);
        }

        let last = dataset[dataset.length-1];
        let keys = Object.keys(last);
        let value = last[keys[0]];
        let increment = last[keys[2]];
        let next = value + increment;

        return next;
    }

}