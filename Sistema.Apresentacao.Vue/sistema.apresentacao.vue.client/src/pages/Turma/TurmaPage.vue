<script setup>
import { ref, computed, toRaw, onMounted } from 'vue';
import { useToast } from 'primevue/usetoast';
import api from '../../services/api'
import Column from 'primevue/column';
import DataTable from 'primevue/datatable';
import Toolbar from 'primevue/toolbar';
import ConfirmDialog from 'primevue/confirmdialog';
import { useConfirm } from "primevue/useconfirm";

const menu = ref();
const menuModel = ref([]);
const confirm = useConfirm();

const turmas = ref([]);
const cturmas = computed(() => turmas.value);

const query = ref({});

const toast = useToast();

const cadastro = ref(false);
const turma = ref({
    nomeTurma: '',
    sigla: '',
});

const items = ref([]);
const search = async(e) => {

    let value = e.query;
    const response = await api.get('pessoa/search', { nome: value });

    if (response.isSuccess) {
        items.value = response.data;
        console.log('items', items.value);
    }
}

const cLabel = computed(() => {
    if (turma.value.id) {
        return 'Alterar';
    }

    return 'Cadastrar';
})

const consultar = async () => {
    const response = await api.get('turma/search', query.value);

    if (response.isSuccess) {
        turmas.value = response.data;
    } else {
        toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao consultar turma' + response.error, life: 3000 });
    }
}

const alterar = (model) => {
    turma.value = toRaw(model);
    cadastro.value = true;
}

const deletar = (model) => {
    confirm.require({
        message: 'Você tem certeza que quer remover o registro?',
        header: 'Confirmação',
        icon: 'pi pi-exclamation-triangle',
        rejectProps: {
            label: 'Cancelar',
            severity: 'secondary',
            outlined: true
        },
        acceptProps: {
            label: 'Confirmar'
        },
        accept: async () => {
            let selected = toRaw(model);
            const response = await api.delete('turma/delete/' + selected.id)

            if (response.isSuccess) {
                turmas.value = turmas.value.filter(item => item.id !== selected.id);
                toast.add({ severity: 'info', summary: 'Sucesso', detail: 'O registro foi deletado', life: 3000 });
            }
            else {
                toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao remover turma' + response.error, life: 3000 });
            }
        },
        reject: () => {
            toast.add({ severity: 'error', summary: 'Cancelado', detail: 'A operação foi cancelada', life: 3000 });
        }
    });
}


const submitForm = async () => {
    let response;

    if (turma.value.id) {
        response = await api.put('turma/update/' + turma.value.id, {
            id: turma.value.id,
            nomeTurma: turma.value.nomeTurma,
            sigla: turma.value.sigla,
            idProfessor: turma.value.professor?.id??0,
        });
    } else {
        response = await api.post('turma/create', {
            nomeTurma: turma.value.nomeTurma,
            sigla: turma.value.sigla,
            idProfessor: turma.value.professor?.id??0,
        });
    }


    if (response.isSuccess) {
        if (turma.value.id){
            turmas.value = turmas.value.filter(item => item.id !== turma.value.id);
            toast.add({ severity: 'success', summary: 'Sucesso', detail: `Turma ${turma.value.nomeTurma} - ${turma.value.sigla} alterada com sucesso`, life: 3000 });
        } else {
            toast.add({ severity: 'success', summary: 'Sucesso', detail: `Turma ${turma.value.nomeTurma} - ${turma.value.sigla} cadastrada com sucesso`, life: 3000 });
        }

        turma.value = { nomeTurma: '', sigla: '', idProfessor: null };


        turmas.value.unshift(response.data);
        cadastro.value = false;
    } else {
        if (response.validationErrors){
            for (const error of response.validationErrors){
                toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao cadastrar turma' + error, life: 3000 });

            }
        }
    }
};

const toggleMenu = (model, event) => {
    menuModel.value = [];

    let arrayMenu = [
        {
            label: "Alterar",
            icon: "pi pi-pencil",
            command: () => alterar(model),
            rf: "RF003",
        },
        {
            label: "Remover",
            icon: "pi pi-fw pi-trash",
            command: () => deletar(model),
        },
    ];

    menuModel.value = arrayMenu;

    menu.value.toggle(event);
};


onMounted(() => {
    const response = api.get('pessoa/search', {});

    if (response.isSuccess) {
        items.value = response.data;
    }
})


</script>

<template>
    <ConfirmDialog></ConfirmDialog>
    <div class="w-full">
        <Fieldset class="m-0 p-0">
            <template #legend>
                <span class="text-xl text-primary font-bold"><i
                        class="pi pi-search mr-2 text-primary text-xl font-bold"></i>Filtro de turmas</span>
            </template>


            <div class="p-fluid grid gap-2 m-2">

                <float-label>
                    <InputText id="nome" v-model="query.nome" required autofocus />
                    <label>Nome</label>
                </float-label>

                <float-label>
                    <InputText id="sigla" v-model="query.sigla" required autofocus />
                    <label>Sigla</label>
                </float-label>

                <p-button @click="consultar" icon="pi pi-search" label="Consultar"></p-button>

            </div>
        </Fieldset>

        <Toolbar class="mt-2">
            <template #start>
                <p-button v-if="!cadastro" label="Cadastrar turma" severity="info" icon="pi pi-plus"
                    @click.stop="cadastro = !cadastro;"></p-button>
                <p-button v-if="cadastro" label="Voltar a lista" severity="info" icon="pi pi-arrow-left"
                    @click.stop="cadastro = !cadastro;"></p-button>
            </template>
        </Toolbar>



        <div class="w-full flex flex-column gap-2" v-if="!cadastro">
            <h2 class="mt-2">turmas</h2>

            <DataTable :value="cturmas">
                <Column :exportable="false" style="width: 3rem">
                    <template #body="slotProps">
                        <PButton type="button" @click="toggleMenu(slotProps.data, $event)" icon="pi pi-bars"
                            class="w-2rem h-2rem" title="Menu de Ações" />
                        <PMenu ref="menu" :model="menuModel" :popup="true" />
                    </template>
                </Column>
                <Column header="Nome" field="nomeTurma"></Column>
                <Column header="Sigla" field="sigla"></Column>
                <Column header="Professor" field="professor.nome"></Column>

            </DataTable>
        </div>


        <div class="w-full flex flex-column gap-2 mt-4" v-if="cadastro">
            <h2>Cadastro de turma</h2>
            <form @submit.prevent="submitForm">
                <div class="p-fluid">
                    <div class="flex col-3 gap-2 flex-column">
                        <label for="nome">Nome</label>
                        <InputText id="nome" v-model="turma.nomeTurma" required autofocus />
                    </div>
                    <div class="flex gap-2 col-3 flex-column">
                        <label for="sigla">Sigla</label>
                        <InputText id="sigla" v-model="turma.sigla" required />
                    </div>

                    <div class="flex gap-2 col-3 flex-column">
                        <label for="idprofesssor">Professor</label>

                    <AutoComplete option-label="nome" dropdown :suggestions="items" @complete="search" id="idpessoa" v-model="turma.professor"
                        required autofocus />


                    </div>


                    <p-button type="submit" icon="pi pi-save" :label="cLabel" class="ml-2 mt-2" />
                </div>
            </form>
        </div>
    </div>
</template>