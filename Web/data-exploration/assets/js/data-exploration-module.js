export async function analizeFrequencies(data){
    const maxFreqElem = document.getElementById('max-freq');
    const minFreqElem = document.getElementById('min-freq');

    const maxFreq = Math.max(...data.map(f => f[0]));
    const minFreq = Math.min(...data.map(f => f[0]));

    maxFreqElem.innerText = `Max Frequency: ${maxFreq}`;
    minFreqElem.innerText = `Min Frequency: ${minFreq}`;

    return await analizeData(data);
     
}

async function analizeData(data){
    const filterRange = (data, min, max) => data.filter(n => n[0] >= min && n[0] <= max);

    const properties = await getNotes();

    const results = [];

    properties.forEach( p => {
        const result = filterRange(data, p.min, p.max);

        const chartInfo = document.getElementById(`chart-info-freq-${p.id}`);

        chartInfo.innerText = `[ ${p.name} - ${p.frequency} Hz ]:  ${result.length}`;

        results[p.id] = result;
    });

    return results;

}

export function getNotes(tolerance=0.3){
    return [
        {
            "id": 6,
            "frequency": 82.41,
            "tolerance": tolerance,
            "min": 82.41 * (1-tolerance),
            "max": 82.41 * (1+tolerance),
            "name": "Mi/E2",
            "color": "rgb(200, 124, 10)"
        },
        {
            "id": 5,
            "frequency": 110,
            "tolerance": tolerance,
            "min": 110 * (1-tolerance),
            "max": 110 * (1+tolerance),
            "name": "La/A2",
            "color": "rgb(187, 200, 10)"
        },
        {
            "id": 4,
            "frequency": 146.83,
            "tolerance": tolerance,
            "min": 146.83 * (1-tolerance),
            "max": 146.83 * (1+tolerance),
            "name": "Re/D3",
            "color": "rgb(10, 200, 122)"
        },
        {
            "id": 3,
            "frequency": 196,
            "tolerance": 0.3,
            "min": 196 * (1-tolerance),
            "max": 196 * (1+tolerance),
            "name": "Sol/G3",
            "color": "rgb(67, 180, 243)"
        },
        {
            "id": 2,
            "frequency": 246.94,
            "tolerance": 0.3,
            "min": 246.94 * (1-tolerance),
            "max": 246.94 * (1+tolerance),
            "name": "Si/B3",
            "color": "rgb(192, 67, 165)"
        },
        {
            "id": 1,
            "frequency": 329.63,
            "tolerance": 0.3,
            "min": 329.63 * (1-tolerance),
            "max": 329.63 * (1+tolerance),
            "name": "Mi/E4",
            "color": "rgb(255, 255, 255)"
        }
    ];
}
