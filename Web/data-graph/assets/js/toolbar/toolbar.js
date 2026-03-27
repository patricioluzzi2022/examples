import { environment } from '../environment-module.js';

export class Toolbar {
    constructor(id = undefined) {
        this.id = id | 1;
        this.container = undefined;
    }

    async init() {
        this.container = document.querySelector(`[data-chart="${this.id}"] .mc-container`);
    }

    async fc_script() {
        this.container.classList.add('mc-editing');
        this.container.classList.remove('mc-bash');

        let path = await this.getPath();

        let fileString = await this.loadFile(path);

        this.container.classList.add('python-code');
        this.container.textContent = fileString;

    }

    fc_bash_viewer() {
        console.log('fc_bash_viewer');
        this.container.classList.remove('mc-editing');
        this.container.classList.add('mc-bash');
    }

    getPath() {
        let fileReference = document.querySelector(`[data-chart="${this.id}"] .graph-col-1`).innerHTML.trim();
        const index = fileReference.indexOf('scripts/');
        let file_name = fileReference.slice(index).replace('scripts/', '');
        return `${environment.file_static_url}/${file_name}`; //`${environment.main_path}scripts/${file_name}`;
    }

    async loadFile(path) {
        try {
            const response = await fetch(path);

            if (!response.ok) {
                console.info(response);
            }

            const texto = await response.text();
            return texto;

        } catch (error) {
            console.info(error);
            return "Error al cargar el archivo.";
        }
    }

    fc_clear() {
        this.container.textContent = '';
        let chartLabel = document.querySelector(`[data-chart="${this.id}"] .graph-col-1`);
        chartLabel.innerHTML = '';
    }
}