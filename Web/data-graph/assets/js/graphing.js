import { LinealChart } from './charts/lineal-chart.js';
import { getGraphTemplate } from './environment-module.js';
import { Toolbar } from './toolbar/toolbar.js';

export class Graphing extends EventTarget {
    constructor(id = null) {
        super();

        this.id = id;
        this.canva = undefined;

        this.type = undefined;
        this.chart = undefined;
        
        this.charts = [];
        this.tootbar = new Toolbar();
  
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
            let template = getGraphTemplate(this.id);
            const newGraph = document.createElement('div');
            newGraph.innerHTML = template;

            document.body.appendChild(newGraph);

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

        this.scriptCtrl = document.querySelector(`[data-chart="${this.id}"] #nc-toolbar-control-script`);

        this.scriptCtrl.onclick = (event) => {
            this.toolbarFunction('script');
        }

        this.bashViewerCtrl = document.querySelector(`[data-chart="${this.id}"] #nc-toolbar-control-bash-viewer`);

        this.bashViewerCtrl.onclick = (event) => {
            this.toolbarFunction('bash-viewer');
        }
    }

    async connect() {
        await this.tootbar.init();

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
        
        await this.chart.connect();

        this.chart.addEventListener('in-event', (event) => {
            this.coordinateById(1);
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
                this.scriptCtrl.classList.remove('control_disabled');
                this.bashViewerCtrl.classList.remove('control_disabled');
                this.toolbarFunction('script');
                break;
            case 2:
                this.connectCtrl.classList.remove('control_disabled');
                this.desconnectCtrl.classList.add('control_disabled');
                this.clearCtrl.classList.remove('control_disabled');
                this.scriptCtrl.classList.add('control_disabled');
                this.bashViewerCtrl.classList.add('control_disabled');
                break;
            case 3:
                this.connectCtrl.classList.remove('control_disabled');
                this.desconnectCtrl.classList.add('control_disabled');
                this.clearCtrl.classList.add('control_disabled');
                this.scriptCtrl.classList.add('control_disabled');
                this.bashViewerCtrl.classList.add('control_disabled');
                this.toolbarFunction('clear');
                break;
            default:

                break;
        }
    }

    toolbarFunction(fc) {
        switch (fc) {
            case 'script':
                this.tootbar.fc_script();
                break;
            case 'bash-viewer':
                this.tootbar.fc_bash_viewer();
                break;
            case 'clear':
                this.tootbar.fc_clear();
                break;
            
            default:
                
        }
    }

}