import { createApp } from 'vue';
import PrimeVue from 'primevue/config';
import App from './App.vue';
import router from './router.js';
import ToastService from 'primevue/toastservice'
import Aura from '@primevue/themes/aura';
import 'primeicons/primeicons.css'
import 'primeflex/primeflex.css';

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
import FloatLabel from 'primevue/floatlabel';
import Fieldset from 'primevue/fieldset';
import CustomDatatable from './components/data/CustomDatatable.vue';
import CustomColumn from './components/data/CustomColumn.vue';


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
app.component('PMenu', Menu);
app.component('PMenubar', Menubar);
app.component('PCalendar', Calendar);
app.component('PanelMenu', PanelMenu);
app.component('InputText', InputText);
app.component('InputMask', InputMask);
app.component('DataTable', DataTable);
app.component('FloatLabel', FloatLabel);
app.component('Fieldset', Fieldset);
app.component('PColumn', Column);
app.component('PDialog', Dialog);
app.component('PToast', Toast);
app.component('PCard', Card);
app.component('CustomDatatable', CustomDatatable);
app.component('CustomColumn', CustomColumn);


app.mount('#app');