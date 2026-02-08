import { draftChart } from './chart-module.js';

export class MagnitudeChart extends EventTarget{
    constructor(chartId, source, note){
        super();
        this.chartId = chartId;
        this.source = source;
        this.socket = null;
        this.chart = undefined;
        this.datasets = [];
        this.layoutName = 'MGChart';
        this.layout;
        this.note = note;
    }

    createFromSocket(){
        this.socket = io(this.source);

        this.socket.on("connected-event", async (data) => {
            console.log(data);
        });

        this.socket.on("layout-callback-event", async (data) => {
            data.layout.axis.forEach(a => {
                a.lcolor = this.note.color;
            });

            this.layout = {
                'title': `${this.note.name} - ${this.note.frequency} Hz` || data.layout.title,
                'type': data.layout.type || 'line',
                'axis': data.layout.axis || undefined,
                'plot_bg_color': data.layout.plot_bg_color || undefined,
                'paper_bg_color': data.layout.paper_bg_color || undefined,
                'intersect': data.layout.intersect || false,
                'range': data.layout.range || [0, 105]
            };

            this.datasets = data.layout.datasets;
            this.datasets[0].borderColor = this.note.color;

            this.chart = await draftChart(this.layout, this.datasets, this.chartId);
        });
        
    }

    async draft(){
        if(this.layout){
            this.chart = await draftChart(this.layout, this.datasets, this.chartId);
        }else{
            this.socket.emit("layout-event", this.layoutName);
        }
    }

    async update(m){
        this.chart.config._config.data.datasets[0].data = this.chart.config._config.data.datasets[0].data.slice(1).concat(m);
        this.chart.update();
    }

    disconnect() {
        this.socket.disconnect();
    }
    
}