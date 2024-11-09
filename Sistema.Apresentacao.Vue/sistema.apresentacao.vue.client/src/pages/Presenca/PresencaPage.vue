<template>
    <div class="w-full">
        <Fieldset class="m-0 p-0">
            <template #legend>
                <span class="text-xl text-primary font-bold"><i
                        class="pi pi-search mr-2 text-primary text-xl font-bold"></i>Filtro de Presença</span>
            </template>


            <div class="p-fluid grid gap-2 m-2">

                <float-label>
                    <InputText id="turma" v-model="query.turma" required autofocus />
                    <label>Turma</label>
                </float-label>


                <float-label>
                    <PCalendar></PCalendar>
                    <label>Data Inicio</label>
                </float-label>

                <float-label>
                    <PCalendar></PCalendar>
                    <label>Data Final</label>
                    </ float-label>

                    <p-button label="consultar"></p-button>

            </div>
        </Fieldset>



        <div class="w-full flex flex-column gap-2">
            <h2 class="mt-2">Registro de Presenças/Faltas</h2>

            <CustomDatatable :values="computedPresenca" groupByKey="data" rowSpanKey="turma">


                <CustomColumn header="Ações" :exportable="false" style="width: 3rem">
                    <template #body="slotProps">
                        <PButton type="button" @click="toggleMenu(slotProps.data, $event)" icon="pi pi-bars"
                            class="w-2rem h-2rem" title="Menu de Ações" />
                        <PMenu ref="menu" :model="menuModel" :popup="true" />
                    </template>
                </CustomColumn>
                <CustomColumn header="Turma" accessorKey="turma" :expanded="true"></CustomColumn>
                <CustomColumn header="Nome" accessorKey="nome"></CustomColumn>
                <CustomColumn header="Presente" accessorKey="presente">
                    <template #body="{ item }">
                        <Checkbox :modelValue="item.presente" @click="setPresenca(item)" binary></Checkbox>
                    </template>
                </CustomColumn>
            </CustomDatatable>
        </div>
    </div>
</template>

<script setup>
import { ref, computed } from 'vue';
import { useToast } from 'primevue/usetoast';
import api from '../../services/api'
import Checkbox from 'primevue/checkbox';

const toast = useToast();
const menu = ref();
const menuModel = ref([]);

const query = ref({});


const presenca = ref([
    {
        id: 1,
        nome: "Lucas Silva",
        email: "lucassmsilvadev@gmail.com",
        telefone: "65984699962",
        data: "11/10/2024",
        turma: "VC01",
        presente: true,
    },
    {
        id: 2,
        nome: "Isabel Camila",
        email: "teste@gmail.com",
        telefone: "65999999999",
        data: "11/10/2024",
        turma: "VC01",
        presente: true,

    },
    {
        id: 1,
        nome: "Lucas Silva",
        email: "lucassmsilvadev@gmail.com",
        telefone: "65984699962",
        data: "12/10/2024",
        turma: "VC01",
        presente: true,
    },
    {
        id: 2,
        nome: "Isabel Camila",
        email: "teste@gmail.com",
        telefone: "65999999999",
        data: "12/10/2024",
        turma: "VC01",
        presente: true,

    },
]);

const computedPresenca = computed(() => presenca.value);


const setPresenca = (item) => {
    let index = presenca.value.indexOf(item);
    presenca.value[index].presente = !presenca.value[index].presente;
}


const toggleMenu = (model, event) => {
    menuModel.value = [];

    let arrayMenu = [
        {
            label: "Justificar",
            icon: "pi pi-pencil",
            command: () => alterar(model),
            rf: "RF003",
        },
    ];

    menuModel.value = arrayMenu;

    menu.value.toggle(event);
};

</script>

<style scoped>
.card {
    background: var(--surface-card);
    padding: 2rem;
    border-radius: 10px;
    margin-bottom: 1rem;
}
</style>