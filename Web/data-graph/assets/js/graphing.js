import { LinealChart } from './charts/lineal-chart.js';

export class Graphing extends EventTarget {
    constructor(id = null) {
        super();

        this.id = id;
        this.canva = undefined;

        this.type = undefined;
        this.chart = undefined;
        
        this.charts = [];

    }

    init(type = null) {
        this.type = type;
        this.findOrCreateNewCanvas();
        this.assignControlsEvents();
    }

    findOrCreateNewCanvas() {
        if(this.id == 1){
            this.canvas = document.querySelector(`[data-chart="${this.id}"]`);
        }else{
            //let reference = document.querySelector(`[data-chart="1"]`);

            //const newCanvas = document.createElement('div');
            //newCanvas.classList.add('wizard-soperpower-9');
            //newCanvas.attributes.add('data-chart', this.id);
            
            
        }
    }

    assignControlsEvents() {
        this.connectCtrl = document.querySelector(`[data-chart="${this.id}"] #connect-socket-control`);

        this.connectCtrl.onclick = (event) => {
            this.connect();
        }

        this.desconnectCtrl = document.querySelector(`[data-chart="${this.id}"] #disconnect-socket-control`);

        this.desconnectCtrl.onclick = (event) => {
            this.disconnect();
        }

        this.addNewChartCtrl = document.querySelector(`[data-chart="${this.id}"] #add-new-graph-control`);

        this.addNewChartCtrl.onclick = (event) => {
            this.addNewGraph();
        }

        this.clearCtrl = document.querySelector(`[data-chart="${this.id}"] #clear-graph-control`);

        this.clearCtrl.onclick = (event) => {
            this.clear();
        }
    }

    async connect() {
        this.coordinateById(1);
 
        const source = document.querySelector(`[data-chart="${this.id}"] #data-socket-url`);
        const socketUrli = source.value;
        
        switch(this.type){
            case 'lineal':
                this.chart = new LinealChart(this.id, socketUrli, undefined);
                break;
            case 'radar':

                break;
            default:
                break;
        }

        await this.chart.init();
        
        this.chart.connect();

        this.chart.addEventListener('in-event', (event) => {
            let chartLabel = document.querySelector(`[data-chart="${this.id}"] .graph-col-1`);
            chartLabel.innerHTML = `Script: ${event.target.in.id} - File: ${event.target.in.file}`;
        });

        //this.dispatchEvent(evento);


        
        
        //this.chart.events[''].then((event) => {
//
        //});
//
        //let index = this.charts.findIndex(c => c.id == this.id);
        //
        //if(index != -1){
        //    this.charts[index] = this.chart;
        //}
//
        //this.charts.addEventListener('dataset_callback_event', (event) => {
        //    let chart = charts.find(c => c.chartId == `chart-${event.detail.note.id}`);
        //    chart.update(event.detail.dataset);
        //});
        
    }

    disconnect() {
        this.coordinateById(2);
        this.chart.disconnect();
    }

    addNewGraph() {
        const newGraphEvent = new CustomEvent('new-graph-event', {} );
        this.dispatchEvent(newGraphEvent);
    }

    clear() {
        this.coordinateById(3);
        this.chart.clear();
    }

    coordinateById(magicId) {
        switch(magicId){
            case 1:
                this.connectCtrl.classList.add('control_disabled');
                this.desconnectCtrl.classList.remove('control_disabled');
                this.clearCtrl.classList.add('control_disabled');
                break;
            case 2:
                this.connectCtrl.classList.remove('control_disabled');
                this.desconnectCtrl.classList.add('control_disabled');
                this.clearCtrl.classList.remove('control_disabled');
                break;
            case 3:
                this.connectCtrl.classList.remove('control_disabled');
                this.desconnectCtrl.classList.add('control_disabled');
                this.clearCtrl.classList.add('control_disabled');
                break;
            default:

                break;
        }
    }

}