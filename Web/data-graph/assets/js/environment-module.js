export const environment = {
    "connection_request_event"  : "connection_request_event",
    "connection_response_event": "connection_response_event",

    "request_layout_event"  : "request_layout_event",
    "response_layout_event": "response_layout_event",

    "request_data_event"  : "request_data_event",
    "response_data_event": "response_data_event"
}

export function getGraphTemplate(layout) {
    return `
    <div class="wizard-soperpower-9 wizard-soperpower-7" data-chart="${layout}">
        <div class="graph-row-1">
            <div class="graph-col-0">
                Socket url: <input class="form-control-url" type="text" id="data-socket-url" value="http://localhost:36388" />
            </div>

            <div class="graph-col-1">
                
            </div>

            <div class="graph-col-2">
                Idle: <input class="form-control-idle-time" type="text" id="data-socket-idle-time" value="" placeholder="1500" /> [ms]
            </div>

            <div class="graph-col-3">
                <button id="connect-socket-control">Connect</button>
            </div>

            <div class="graph-col-4">
                <button id="disconnect-socket-control" class="control_disabled">Pause</button>
            </div>

            <div class="graph-col-5">
                <button id="clear-graph-control" class="control_disabled">Clear</button>
            </div>
            
        </div>

        <div class="graph-row-2">
            <canvas id="chart-${layout}" width="720" height="440" data-chart="${layout}"></canvas>
        </div>
    </div>`;
}  