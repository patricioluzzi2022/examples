import socketio
import eventlet
import importlib
import random

# new Socket.IO server
sio = socketio.Server(cors_allowed_origins='*')
app = socketio.WSGIApp(sio)

# graph layout definitions
layout1 = {
    "title": "Lineal Chart",
    "type": "line",
    "intersect": False,
    "axis": [
        {
            "name": "x",
            "title": "x [ ]",
            "range": ['11', '10', '9', '8', '7', '6', '5', '4', '3', '2', '1', '0'],
            "lcolor": "rgb(200, 200, 200)",
            "mirror": False
        },
        {
            "name": "y",
            "title": "y [ ]",
            "range": [],
            "lcolor": "rgb(200, 200, 200)",
            "mirror": False
        },
    ],
    "datasets": [
        {
            "label": "y",
            "data": [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            "borderColor": "rgb(217, 141, 54)",
            "backgroundColor": "rgba(75, 192, 192, 0.2)",
            "fill": True,
            "tension": 0.7220
        }
    ],
    "range": [0, 320],
    "plot_bg_color": "white",
    "paper_bg_color": "white",
    "analisis": [],
    "ms": 500
}

layout2 = {
    "title": "Lineal Chart",
    "type": "line",
    "intersect": False,
    "axis": [
        {
            "name": "x",
            "title": "x [ ]",
            "range": ['11', '10', '9', '8', '7', '6', '5', '4', '3', '2', '1', '0'],
            "lcolor": "rgb(200, 200, 200)",
            "mirror": False
        },
        {
            "name": "y",
            "title": "y [ ]",
            "range": [],
            "lcolor": "rgb(200, 200, 200)",
            "mirror": False
        },
    ],
    "datasets": [
        {
            "label": "y",
            "data": [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            "borderColor": "rgb(217, 141, 54)",
            "backgroundColor": "rgba(75, 192, 192, 0.2)",
            "fill": True,
            "tension": 0.7220
        }
    ],
    "range": [0, 320],
    "plot_bg_color": "white",
    "paper_bg_color": "white",
    "analisis": [],
    "ms": 500
}

# endpoints definitions
@sio.event
def connect(sid, environ):
    sio.emit('connected-event', {'status': 200 ,'message': 'Connection stablished!'}, to=sid)

@sio.on('layout-event')
def get_layout(sid, type):
    match type:
        case 1:
            sio.emit('layout-callback-event', {'status': 200, 'layout': layout1 }, to=sid)
        case 2:
            sio.emit('layout-callback-event', {'status': 200, 'layout': layout2 }, to=sid)

@sio.on('datasets-event')
def get_data(sid, params):
    dataset = get_dataset(params)
    sio.emit('dataset_callback_event', {'status': 200, 'dataset': dataset}, to=sid)    

# functions
def get_dataset(params):
    """
    This fuction is responsible for returning information based on the established parameters.
    
    Args:
        params: Dictionary
    
    Returns:
        It depends on the layout and the output of the related script
    """
    
    spec = None

    match params["layout"]:
        case 1:
            spec = importlib.util.spec_from_file_location("modulo", "./assets/scripts/01-factorial.py")
        case 2:
            spec = importlib.util.spec_from_file_location("modulo", "./assets/scripts/02-combination.py")
        case 3:
            spec = importlib.util.spec_from_file_location("modulo", "./assets/scripts/03-permutation.py")
        case _:
            spec = importlib.util.spec_from_file_location("modulo", "./00.py")
    
    modulo = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(modulo)

    return modulo.get_dataset(params["params"]["x"])

if __name__ == '__main__':
    # Starting a new server on port 36388
    eventlet.wsgi.server(eventlet.listen(('', 36388)), app)
    