import { draftChart } from './chart-module.js';
import { analizeFrequencies, getNotes } from './data-exploration-module.js';

export class FrequencesChart extends EventTarget{
    constructor(chartId, source, getDataEvent, callbackEvent){
        super();
        this.chartId = chartId;
        this.source = source;
        this.socket = null;
        this.getdata = getDataEvent
        this.callback = callbackEvent;
        this.chart = undefined;
        this.datasets = [];
        this.magnitudes = undefined;
        this.notes = getNotes();
    }

    createFromSocket(){
        this.socket = io(this.source);

        this.socket.on("connected-event", async (data) => {
            console.log(data);
        });

        this.socket.on("layout-callback-event", async (data) => {
            this.layout = {
                'title': data.layout.title || undefined,
                'type': data.layout.type || 'line',
                'axis': data.layout.axis || undefined,
                'plot_bg_color': data.layout.plot_bg_color || undefined,
                'paper_bg_color': data.layout.paper_bg_color || undefined,
                'intersect': data.layout.intersect || false,
                'range': data.layout.range || [0, 105]
            };

            this.datasets = data.layout.datasets;

            this.socket.emit(this.getdata, {});
        });

        this.socket.on(this.callback, async (data) => {
            this.chart = await draftChart(this.layout, this.datasets, this.chartId);

            data.analysis.top_frequencies.map(l => l[0]).forEach(f => {
                this.chart.config._config.data.datasets[0].data = this.chart.config._config.data.datasets[0].data.slice(1).concat(f);
                this.chart.update();
            });

            this.magnitudes = await analizeFrequencies(data.analysis.top_frequencies);

            this.magnitudes.forEach( (data, note) => {
                data.forEach( value => {
                    this.dispatchEvent(new CustomEvent('magnitude-per-frequency-event', { detail: { note: this.notes.filter(n => n.id === note)[0], frequency: value[0], magnitude: value[1] }} ));
                });
                
            });

            //this.socket.emit("layout-event", this.chartId);
            this.socket.emit(this.getdata, {});

        });
        
    }

    draft(){
        this.socket.emit("layout-event", this.chartId);
    }

    disconnect() {
        this.socket.disconnect();
    }

}