import { createApp } from 'vue';
import PrimeVue from 'primevue/config';
import App from './App.vue';
import router from './router.js';
import ToastService from 'primevue/toastservice'
import Aura from '@primevue/themes/aura';

// Importações dos componentes do PrimeVue
import Button from 'primevue/button';
import Menu from 'primevue/menu';
import Menubar from 'primevue/menubar';
import Calendar from 'primevue/calendar';
import PanelMenu from 'primevue/panelmenu';
import InputText from 'primevue/inputtext';
import InputMask from 'primevue/inputmask';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import Dialog from 'primevue/dialog';
import Toast from 'primevue/toast';
import Card from 'primevue/card';
import Dropdown from 'primevue/dropdown';


const app = createApp(App);


app.use(router);
app.use(ToastService);

app.use(PrimeVue, {
    theme: {
        preset: Aura,
        options: {
            prefix: 'p',
            darkModeSelector: '.dark',
            cssLayer: false
        }
    }
});

app.component('PButton', Button);
app.component('Menu', Menu);
app.component('Menubar', Menubar);
app.component('Calendar', Calendar);
app.component('PanelMenu', PanelMenu);
app.component('InputText', InputText);
app.component('InputMask', InputMask);
app.component('DataTable', DataTable);
app.component('Column', Column);
app.component('Dialog', Dialog);
app.component('Toast', Toast);
app.component('Card', Card);
app.component('Dropdown', Dropdown);


app.mount('#app');