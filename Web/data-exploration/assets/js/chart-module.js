export async function draftChart(layout, datasets, chartId) {
    const chart = Chart.getChart(chartId); 
    
    if (chart) {
        chart.scales.y.max = layout.range[1];
        return chart;
    }

    let scales = {};

    layout.axis.forEach((ax, index) => {
        scales[ax.name] = {
            display: true,
            title: {
                display: true,
                text: ax.title || ax.name
            },
            ticks: { color: ax.lcolor || 'white' },
            grid: { color: '#444' }
        };
        if (index == 1) {
            scales[ax.name].suggestedMin = layout.range[0];
            scales[ax.name].suggestedMax = layout.range[1];
        }
    });

    const data = {
        labels: layout.axis[0].range || [],
        datasets: datasets || []
    };

    const config = {
        type: layout.type || 'line',
        data: data,
        options: {
            responsive: false,
            plugins: {
                title: {
                    display: true,
                    text: layout.title || 'Trend estimation'
                }
            },
            interaction: {
                intersect: layout.intersect || false,
            },
            scales: scales
        }
    };

    Chart.defaults.animation = false;

    const ctx = document.getElementById(chartId);
    
    return new Chart(ctx, config);
}