<script setup>

import Checkbox from 'primevue/checkbox';
import { ref, computed, onMounted } from 'vue';
import { useToast } from 'primevue/usetoast';
import api from '../../services/api'
import Column from 'primevue/column';
import DataTable from 'primevue/datatable';

const props = defineProps({
    pessoa: {
        type: Object,
        default: null
    },
    visible: {
        type: Boolean,
        required: true,
    },
})

const emit = defineEmits(["update:visible"]);
const cVisible = computed({
    get() {
        return props.visible
    },
    set(value) {
        emit('update:visible', value)
    }
})

const toast = useToast();
const data = ref([]);
const selected = ref([]);
const filtro = ref('');
const submit = ref(false);


const cturmas = computed(() => {
    let filteredData = data.value;

    if (filtro.value?.length > 0){
        let exp = new RegExp(filtro.value, 'i');
        filteredData = filteredData?.filter(item => exp.test(item.sigla) || exp.test(item.nomeTurma))
    }


    return filteredData;
});

const setChecked = (item) => {
    
    
    let index = data.value.indexOf(item);
    let checkvalue = !data.value[index].checked;
    data.value[index].checked = checkvalue;

    if (checkvalue){
        selected.value.push(item);
    } else {
        selected.value = selected.value.filter(t => t.id !== item.id);
    }

}


const getTurmas = async() => {
    selected.value = [];
    const response = await api.get('turma/search', {}); 

    if (response.isSuccess) {
        data.value = response.data;
    }
    else {
        toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao buscar turmas', life: 3000 });
    }
}

const vincularTurmas = async() => {
    submit.value = true;

    for (const turma of selected.value){
        let response = await api.post(`turma/adicionar-aluno`, {
            idTurma: turma.id,
            pessoas: [ props.pessoa.id ]
        });

        if (response.isSuccess){
            toast.add({ severity: 'success', summary: 'Sucesso', detail: `Turma ${turma.nomeTurma} vinculada com sucesso`, life: 3000 });
            cVisible.value = false;
            getTurmas();

        } else {
            toast.add({ severity: 'error', summary: 'Erro',  detail: `Erro ao vincular Turma ${turma.nomeTurma} : ${response.error}`, life: 3000 });
            if (response.validationErrors){
                for (const error in response.validationErrors){
                    toast.add({ severity: 'error', summary: 'Erro',  detail: `${error}`, life: 3000 });
                }
            }
        }
    }

    submit.value = false;

    }

onMounted(() => {
    getTurmas()
})

</script>

<template>

    <PDialog v-model:visible="cVisible" modal :closeOnEscape="true" maximizable :style="{ width: '60vw' }"
        :breakpoints="{ '1199px': '75vw', '575px': '90vw' }" :pt="{
            mask: {
                style: 'backdrop-filter: blur(2px)'
            }
        }">
        <template #header>
            <div class="w-full ">
                <div class="row">
                    <div class="inline-flex align-items-center justify-content-center gap-2 text-xl text-primary">
                        <span class="font-semibold white-space-nowrap">Vincular Turmas de: {{ pessoa.nome }}</span>
                    </div>

                </div>
            </div>
        </template>


        <div class="w-full p-fluid grid mt-1">
            <div class="field w-full">
                
                <label>Pesquise por turma ou sigla para filtrar</label>
                    <InputText class="w-full" placeholder="Ex: Algoritmos" v-model="filtro" />
                
                
            </div>
        </div>

        <div class="w-full flex flex-column gap-4">
            <p class="text-xl font-bold">Selecione as turmas para vincular</p>
            <DataTable :value="cturmas">
                <Column :exportable="false" style="width: 3rem">
                    <template #body="slotProps">
                        <Checkbox :modelValue="slotProps.data.checked" @click="setChecked(slotProps.data)" binary></Checkbox>
                    </template>
                </Column>
                <Column header="Nome" field="nomeTurma"></Column>
                <Column header="Sigla" field="sigla"></Column>
                <Column header="Professor" field="professor.nome"></Column>

            </DataTable>
        </div>

        <div class="w-full flex flex-column gap-4 mt-4">
            <p class="text-xl font-bold">Turmas Selecionadas</p>
            <DataTable :value="selected">
                <Column header="Nome" field="nomeTurma"></Column>
                <Column header="Sigla" field="sigla"></Column>
                <Column header="Professor" field="professor.nome"></Column>
                
            </DataTable>
        </div>

        <template #footer>
            <div class="w-full flex justify-end">
                <p-button @click="vincularTurmas" :disabled="selected.length == 0 || submit" label="Vincular"></p-button>
                <progress-spinner v-if="submit"></progress-spinner>
            </div>
        </template>
    </PDialog>
</template>