import { environment } from '../environment-module.js';
 
export class LinealChart extends EventTarget {
    constructor(id, source, ms = undefined){
        super();

        this.id = id;

        this.source = source;
        this.socket = undefined;
        this.ms = ms;

        this.chart = undefined;
        this.datasets = [];
        this.scales = {};

        this.in = undefined;
    }

    async init() {
        this.socket = io(this.source);

        this.socket.on(environment.connection_response_event, async (result) => {
            this.socket.emit(environment.request_layout_event, this.id);  
        });

        this.socket.on(environment.response_layout_event, async (result) => {
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

            this.socket.emit(environment.request_data_event, { 'layout': this.id, 'params': { "y": "f(x)", "x": 0 } });
        });

        this.socket.on(environment.response_data_event, async (result) => {
            this.in = result.dataset[0].script;
            const inEvent = new CustomEvent('in-event', { message: { id: result.dataset[0].script.id, file: result.dataset[0].script.file } } );
            this.dispatchEvent(inEvent);

            this.chart = await this.drawChart();

            let next = await this.updateChart(result.dataset);

            this.socket.emit(environment.request_data_event, { 'layout': this.id, 'params': { "y": "f(x)", "x": next } });
        });
    }

    connect(){
        this.socket.connect();
        this.socket.emit(environment.connection_request_event, this.id);
    }

    disconnect() {
        this.socket.disconnect();
    }

    async drawChart() {
        //this.chart = Chart.getChart(`chart-${this.id}`); 
        
        if(this.chart) {
            this.chart.scales.y.max = this.layout.range[1];
            return this.chart;
        }

        this.layout.axis.forEach((ax, index) => {
            this.scales[ax.name] = {
                display: true,
                title: {
                    display: true,
                    text: ax.title || ax.name
                },
                ticks: { color: ax.lcolor || 'white' },
                grid: { color: '#444' }
            };
            if (index == 1) {
                this.scales[ax.name].suggestedMin = this.layout.range[0];
                this.scales[ax.name].suggestedMax = this.layout.range[1];
            }
        });

        let data = {
            labels: this.layout.axis[0].range || [],
            datasets: this.datasets || []
        };

        let config = {
            type: this.layout.type || 'line',
            data: data,
            options: {
                responsive: false,
                plugins: {
                    title: {
                        display: true,
                        text: this.layout.title || 'Trend estimation'
                    }
                },
                interaction: {
                    intersect: this.layout.intersect || false,
                },
                scales: this.scales
            }
        };

        Chart.defaults.animation = false;

        const ctx = document.getElementById(`chart-${this.id}`);
        
        return new Chart(ctx, config);
    }

    async updateChart(dataset) {
        let idle = (ms) => new Promise(resolve => setTimeout(resolve, ms));

        let data = dataset.map((f, key) => f[Object.keys(f)[1]]);

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

    clear() {
        this.chart.destroy();
    }

}