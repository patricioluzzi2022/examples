export async function getLayoutFromDataParsed(parsedData){
    const axis = new Array();

    parsedData.axis.forEach(ax => {
        const axisObj = {
            'axis_name': ax.axis_name,
            'title': ax.title,
            'range': ax.range,
            'line_color': ax.line_color,
            'mirror': ax.mirror
        };
        axis.push(axisObj);
    });

    if(axis.length === 0){
        axis.push({
            'axis_name': 'x',
            'title': 'm/z array',
            'range': [],
            'line_color': 'rgb(33, 33, 33)',
            'mirror': true
        });
        axis.push({
            'axis_name': 'y',
            'title': 'intensity',
            'range': [],
            'line_color': 'rgb(33, 33, 33)',
            'mirror': true
        });
    }

    return {
        'title': parsedData.title || 'Data Exploration Plot',
        'axis': axis,
        'plot_bg_color': parsedData.plot_bg_color || 'white',
        'pap_bg_color': parsedData.pap_bg_color || 'white'
    };
}

export async function parseData_old(datasets, data){
    let layout = {
        'title': data.title || undefined,
        'type': data.type || 'line',
        'axis': data.axis || undefined,
        'plot_bg_color': data.plot_bg_color || undefined,
        'paper_bg_color': data.paper_bg_color || undefined,
        'intersect': data.intersect || false,
        'range': data.range || [0, 105]
    };

    if(!datasets){
        datasets = [];

        data.axis[1].values.forEach((value, index) => {
            datasets.push({
                label: value[0],
                data: [value[1]],
                borderColor: "#4caf50",
                fill: true,
                backgroundColor: "rgba(76,175,80,0.2)",
                tension: 0.388
            });
        });
    }else{
        data.axis[1].values.forEach((value, index) => {
            if(datasets.length > 0){
                const t0 = datasets.findIndex(d => (Math.abs(d.label - value[0])/d.label)*100 <= 3 );
            
                if(t0 !== -1){
                    datasets[t0].data.push(value[1]);
                }
            }else{
                //datasets.push({
                //    label: value[0],
                //    data: [value[1]],
                //    borderColor: "#4caf50", podria cambiar de color segun un criterio a definir
                //    fill: true,
                //    backgroundColor: "rgba(76,175,80,0.2)",
                //    tension: 0.388
                //});
            }
            
        });
    }

    const info = {
        date: new Date().toISOString(),
        layout: layout,
        datasets: datasets
    }
    
    return {date: info.date, layout: layout, datasets: datasets};
    
}

export async function parseData(charts, data){
    const cIndex = charts.findIndex(l => l.title === data.title);

    switch(data.title){
        case 'frequency_threshold':
            if(cIndex != -1){
                if(charts[cIndex].layout != {}){
                    let layout = {
                        'title': data.title || undefined,
                        'type': data.type || 'line',
                        'axis': data.axis || undefined,
                        'plot_bg_color': data.plot_bg_color || undefined,
                        'paper_bg_color': data.paper_bg_color || undefined,
                        'intersect': data.intersect || false,
                        'range': data.range || [0, 105]
                    };

                    charts[cIndex].layout = layout;
                }

                // actualizar los datasets existentes con la nueva informacion de los datasets
                data.datasets.forEach(nds => {
                    const dsIndex = charts[cIndex].datasets.findIndex(ds => ds.label === nds.label);

                    if(dsIndex !== -1){
                        charts[cIndex].datasets[dsIndex].data = charts[cIndex].datasets[dsIndex].data.concat(nds.data);
                    }else{
                        charts[cIndex].datasets.push(nds);
                    }
                });

            }
            break;
        default:
            // Lógica por defecto
            break;
    }

    const info = {
        date: new Date().toISOString(),
        layout: charts[cIndex].layout,
        datasets: charts[cIndex].datasets
    }
    
    return {date: info.date, layout: info.layout, datasets: info.datasets};
    
}

export async function parseResponse(charts, response){
    const cIndex = charts.findIndex(l => l.title === response.layout);

    switch(response.layout){
        case 'frequency_threshold':
            charts[cIndex].datasets[0].data = charts[cIndex].datasets[0].data.concat(response.data);

            break;
        default:
            // Lógica por defecto
            break;
    }

    const info = {
        date: new Date().toISOString(),
        layout: charts[cIndex].layout,
        datasets: charts[cIndex].datasets
    }
    
    return {date: info.date, layout: info.layout, datasets: info.datasets};
    
}

export async function getParametersFromDatasets(dataset){
    const maxFreqElem = document.getElementById('max-freq');
    const minFreqElem = document.getElementById('min-freq');

    const maxFreq = Math.max(...dataset);
    const minFreq = Math.min(...dataset);

    maxFreqElem.innerText = `Max Frequency: ${maxFreq}`;
    minFreqElem.innerText = `Min Frequency: ${minFreq}`;
}
