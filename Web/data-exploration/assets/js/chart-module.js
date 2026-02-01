export async function draftChart(layout, datasets, chartId) {
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

    const chart = Chart.getChart(chartId); 
    
    if (chart) {
        chart.destroy();
    }

    const ctx = document.getElementById(chartId);
    
    new Chart(ctx, config);
}

export async function draftChart_reference() {
    const ctx = document.getElementById('myChart');

    const labels = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    const ask = [20, 30, 40, 80];
    const prapare = [NaN, NaN, NaN, 80, 135, 180, 120];
    const process = [NaN, NaN, NaN, NaN, NaN, NaN, 120, 105, 110, 165];
    const datapoints2 = [NaN, NaN, NaN, NaN, NaN, NaN, NaN, NaN, NaN, 165, 180, 180];

    const linearArray = [20.0, 34.55, 49.09, 63.64, 78.18, 92.73, 107.27, 121.82, 136.36, 150.91, 165.45, 180.0];

    const data = {
        labels: labels,
        datasets: [
            {
                label: 'First trimester',
                data: ask,
                borderColor: "#4caf50",
                fill: true,
                backgroundColor: "rgba(76,175,80,0.2)",
                tension: 0.388
            },
            {
                label: 'Second quarter',
                data: prapare,
                borderColor: "#2196f3",
                fill: true,
                backgroundColor: "rgba(33,150,243,0.2)",
                tension: 0.388
            },
            {
                label: 'Third trimester',
                data: process,
                borderColor: "#ff9800",
                fill: true,
                backgroundColor: "rgba(255,152,0,0.2)",
                tension: 0.388
            },
            {
                label: 'The last walk',
                data: datapoints2,
                borderColor: "#e91e63",
                fill: true,
                backgroundColor: "rgba(233,30,99,0.2)",
                tension: 0.388
            },
            {
                label: 'Linear estimation',
                data: linearArray,
                borderColor: "#00ffff",
                fill: true,
                backgroundColor: "rgba(255,255,255,0.1)",
            }
        ]
    };
    
    const config = {
        type: 'line',
        data: data,
        options: {
            responsive: false,
            plugins: {
                title: {
                    display: true,
                    text: 'Trend estimation'
                }
            },
            interaction: {
                intersect: false,
            },
            scales: {
                x: {
                    display: true,
                    title: {
                        display: true,
                        text: 'Months'
                    },
                    ticks: { color: 'white' },
                    grid: { color: '#444' }
                },
                y: {
                    display: true,
                    title: {
                        display: true,
                        text: 'Value'
                    },
                    suggestedMin: -10,
                    suggestedMax: 200,
                    ticks: { color: 'white' },
                    grid: { color: '#444' }
                }
                
            }
        }
    };

    console.log(config);

    new Chart(ctx, config);
}
