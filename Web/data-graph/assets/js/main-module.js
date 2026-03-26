import { Graphing } from './graphing.js';

window.onload = function() {
    let graphing = new Graphing(1);
    graphing.init('lineal');

    graphing.addEventListener('new-graph-event', (event) => {
        let layout = this.prompt('Script #:');

        let graphing = new Graphing(parseInt(layout));
        graphing.init('lineal');
    });
};