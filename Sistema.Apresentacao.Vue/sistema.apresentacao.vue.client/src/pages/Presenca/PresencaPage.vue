<script setup>
import { ref, computed, onMounted, toRaw } from 'vue';
import { useToast } from 'primevue/usetoast';
import api from '../../services/api'
import Checkbox from 'primevue/checkbox';

const toast = useToast();
const menu = ref();
const menuModel = ref([]);

const query = ref({});

const turmas = ref([]);
const horarios = computed(() => {

    if (query.value.idTurma) {
        return (turmas.value.find(t => t.id === query.value.idTurma)?.horarios ?? []).map(item => {
            item.data = item.data.slice(0, 10)
            return item;
        });
    }

    return [];

})
const presenca = ref([]);

const computedPresenca = computed(() => presenca.value);


const setPresenca = async (item) => {
    let index = presenca.value.indexOf(item);
    let valor = !presenca.value[index].presente;
    presenca.value[index].presente = valor;
    let response;
    let msg;
    let rawItem = toRaw(item);
    if (valor === true) {
        response = await api.post('presenca/registrar-presenca', {
            idPessoa: rawItem.idPessoa,
            idTurmaHorario: rawItem.idTurmaHorario
        });

        msg = `Presença para ${rawItem.nome} atribuida com sucesso`;
    } else {
        response = await api.post('presenca/cancelar-presenca', {
            idPessoa: rawItem.idPessoa,
            idTurmaHorario: rawItem.idTurmaHorario
        });

        msg = `Presença para ${rawItem.nome} removida com sucesso`;
    }

    if (response.isSuccess) {
        toast.add({ severity: 'success', summary: 'Erro', detail: msg, life: 3000 });
    } else {
        toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao alterar presença para ' + item.nome + ' ' + + response.error, life: 3000 });
    }
}


const alterar = () => {

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

const consultar = async () => {
    const response = await api.get('presenca/obter-registros-presenca', query.value);

    if (response.isSuccess) {
        presenca.value = response.data;
    } else {
        toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao consultar turma' + response.error, life: 3000 });
    }
}

const consultarTurmas = async () => {
    const response = await api.get('turma/search', query.value);

    if (response.isSuccess) {
        turmas.value = response.data;
    } else {
        toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao consultar turma' + response.error, life: 3000 });
    }

}

const formatarData = (date) => {
    const dataString = date.replace("T", " ");
    const data = new Date(dataString);
    const dataFormatada = data.toLocaleDateString("pt-BR");

    return dataFormatada;
}

onMounted(() => {
    consultarTurmas()
})

</script>

<template>
    <div class="w-full">
        <Fieldset class="m-0 p-0">
            <template #legend>
                <span class="text-xl text-primary font-bold"><i
                        class="pi pi-search mr-2 text-primary text-xl font-bold"></i>Filtro de Presença</span>
            </template>


            <div class="p-fluid grid gap-2 m-2">


                <div class="col-2">

                    <float-label>
                        <p-select id="turma" class="w-full" v-model="query.idTurma" :options="turmas"
                            optionLabel="nomeTurma" optionValue="id" required autofocus></p-select>
                        <label>Turma</label>
                    </float-label>
                </div>

                <div class="col-2">

                    <float-label>
                        <p-select id="horario" class="w-full" v-model="query.idTurmaHorario" :options="horarios"
                            optionLabel="data" optionValue="id" required autofocus></p-select>
                        <label>Data</label>
                    </float-label>
                </div>
                <p-button label="consultar" :disabled="!query.idTurmaHorario" @click.stop="consultar"></p-button>

            </div>
        </Fieldset>



        <div class="w-full flex flex-column gap-2">
            <h2 class="mt-2">Registro de Presenças/Faltas</h2>

            <CustomDatatable :values="computedPresenca" groupByKey="data" rowSpanKey="turma">

                <template #group-header="{ groupKey, isExpanded }">
                    <td colspan="5">
                        <div style="display: flex; align-items: center; cursor: pointer;">
                            <span style="margin-right: 8px;">
                                <span v-if="isExpanded" class="pi pi-chevron-down"></span>
                                <span v-else class="pi pi-chevron-right"></span>
                            </span>
                            <strong>{{ formatarData(groupKey) }}</strong>
                        </div>
                    </td>
                </template>

                <CustomColumn header="Ações" :exportable="false" style="width: 3rem">
                    <template #body="slotProps">
                        <PButton type="button" @click="toggleMenu(slotProps.data, $event)" icon="pi pi-bars"
                            class="w-2rem h-2rem" title="Menu de Ações" />
                        <PMenu ref="menu" :model="menuModel" :popup="true" />
                    </template>
                </CustomColumn>
                <CustomColumn header="Turma" accessorKey="turma.nomeTurma" :expanded="true"></CustomColumn>
                <CustomColumn header="Sigla" accessorKey="turma.sigla" :expanded="true"></CustomColumn>
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



<style scoped>
.card {
    background: var(--surface-card);
    padding: 2rem;
    border-radius: 10px;
    margin-bottom: 1rem;
}
</style>