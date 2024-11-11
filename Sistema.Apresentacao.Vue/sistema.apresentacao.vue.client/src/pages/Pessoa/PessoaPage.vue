<script setup>
import { ref, computed, toRaw } from 'vue';
import { useToast } from 'primevue/usetoast';
import api from '../../services/api'
import Column from 'primevue/column';
import DataTable from 'primevue/datatable';
import Toolbar from 'primevue/toolbar';
import { useConfirm } from "primevue/useconfirm";
import PessoaVinculoTurmaModal from './PessoaVinculoTurmaModal.vue';
import PessoaVinculoRemoverTurmaModal from './PessoaVinculoRemoverTurmaModal.vue';

const menu = ref();
const menuModel = ref([]);
const confirm = useConfirm();

const pessoas = ref([]);
const cPessoas = computed(() => pessoas.value);

const query = ref({});

const toast = useToast();

const cadastro = ref(false);
const modalVinculo = ref(false);
const modalVinculorRemover = ref(false);

const pessoa = ref({
    nome: '',
    cpf: '',
});

const cLabel = computed(() => {
    if (pessoa.value.id) {
        return 'Alterar';
    }

    return 'Cadastrar';
})

const consultar = async () => {
    const response = await api.get('pessoa/search', query.value);

    if (response.isSuccess) {
        pessoas.value = response.data;
    } else {
        toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao consultar pessoa' + response.error, life: 3000 });
    }
}

const alterar = (model) => {
    pessoa.value = toRaw(model);
    cadastro.value = true;
}

const vincular = (model) => {
    pessoa.value = toRaw(model);
    modalVinculo.value = true;
}

const desvincular = (model) => {
    pessoa.value = toRaw(model);
    modalVinculorRemover.value = true;
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
            const response = await api.delete('pessoa/delete/' + selected.id)

            if (response.isSuccess) {
                pessoas.value = pessoas.value.filter(item => item.id !== selected.id);
                toast.add({ severity: 'info', summary: 'Sucesso', detail: 'O registro foi deletado', life: 3000 });
            }
            else {
                toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao remover pessoa' + response.error, life: 3000 });
            }
        },
        reject: () => {
            toast.add({ severity: 'error', summary: 'Cancelado', detail: 'A operação foi cancelada', life: 3000 });
        }
    });
}


const submitForm = async () => {
    let response;

    if (pessoa.value.id) {
        response = await api.put('pessoa/update/' + pessoa.value.id, pessoa.value);
    } else {
        response = await api.post('pessoa/create', pessoa.value);
    }


    if (response.isSuccess) {
        pessoa.value = { nome: '', cpf: '', dataNascimento: null };
        
        if (pessoa.value.id){
            pessoas.value = pessoas.value.filter(item => item.id !== pessoa.value.id);
            toast.add({ severity: 'success', summary: 'Sucesso', detail: 'Pessoa alterada com sucesso', life: 3000 });
        } else {
            toast.add({ severity: 'success', summary: 'Sucesso', detail: 'Pessoa cadastrada com sucesso', life: 3000 });
        }

        pessoas.value.unshift(response.data);
        cadastro.value = false;

    } else {
        toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao cadastrar pessoa' + response.error, life: 3000 });
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
            label: "Vincular",
            icon: "pi pi-fw pi-book",
            command: () => vincular(model),
        },
        {
            label: "Remover Vinculo",
            icon: "pi pi-fw pi-undo",
            command: () => desvincular(model),
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
 const voltar = () => {
    cadastro.value = !cadastro.value;
    pessoa.value = {
        nome: '',
        cpf: '',
    }
 }

</script>

<template>
    <div class="w-full">
        <Fieldset class="m-0 p-0">
            <template #legend>
                <span class="text-xl text-primary font-bold"><i
                        class="pi pi-search mr-2 text-primary text-xl font-bold"></i>Filtro de Pessoas</span>
            </template>


            <div class="p-fluid grid gap-2 m-2">
                <float-label>
                    <InputText id="nome" v-model="query.nome" required autofocus />
                    <label>Nome</label>
                </float-label>

                <float-label>
                    <InputText id="email" v-model="query.email" required autofocus />
                    <label>Email</label>
                </float-label>

                <p-button @click="consultar" icon="pi pi-search" label="Consultar"></p-button>

            </div>
        </Fieldset>

        <Toolbar class="mt-2">
            <template #start>
                <p-button v-if="!cadastro" label="Cadastrar pessoa" severity="info" icon="pi pi-plus" @click.stop="cadastro = !cadastro;"></p-button>
                <p-button v-if="cadastro" label="Voltar a lista" severity="info" icon="pi pi-arrow-left" @click.stop="voltar"></p-button>
            </template>
        </Toolbar>



        <div class="w-full flex flex-column gap-2" v-if="!cadastro">
            <h2 class="mt-2">Pessoas</h2>

            <DataTable :value="cPessoas">
                <Column :exportable="false" style="width: 3rem">
                    <template #body="slotProps">
                        <PButton type="button" @click="toggleMenu(slotProps.data, $event)" icon="pi pi-bars"
                            class="w-2rem h-2rem" title="Menu de Ações" />
                        <PMenu ref="menu" :model="menuModel" :popup="true" />
                    </template>
                </Column>
                <Column header="Nome" field="nome"></Column>
                <Column header="Email" field="email"></Column>
                <Column header="Telefone" field="telefone"></Column>

            </DataTable>
        </div>


        <div class="w-full flex flex-column gap-2 mt-4" v-if="cadastro">
            <h2>Cadastro de Pessoa</h2>
            <form @submit.prevent="submitForm">
                <div class="p-fluid">
                    <div class="flex col-3 gap-2 flex-column">
                        <label for="nome">Nome</label>
                        <InputText id="nome" v-model="pessoa.nome" required autofocus />
                    </div>
                    <div class="flex gap-2 col-3 flex-column">
                        <label for="cpf">CPF</label>
                        <InputMask id="cpf" v-model="pessoa.cpf" mask="999.999.999-99" required />
                    </div>

                    <div class="flex gap-2 col-3 flex-column">
                        <label for="dataNascimento">Data de Nascimento</label>
                        <PCalendar id="dataNascimento" v-model="pessoa.dataNascimento" dateFormat="dd/mm/yy" required />
                    </div>

                    <div class="flex gap-2 col-3 flex-column">
                        <label for="telefone">Telefone</label>
                        <InputMask id="telefone" v-model="pessoa.telefone" mask="(99) 99999-9999" required />
                    </div>

                    <div class="flex col-3 gap-2 flex-column">
                        <label for="email">Email</label>
                        <InputText id="email" v-model="pessoa.email" required autofocus />
                    </div>

                    <p-button type="submit" icon="pi pi-save" :label="cLabel" class="ml-2 mt-2" />
                </div>
            </form>
        </div>
    </div>

    <PessoaVinculoTurmaModal :pessoa="pessoa" v-model:visible="modalVinculo"></PessoaVinculoTurmaModal>
    <PessoaVinculoRemoverTurmaModal :pessoa="pessoa" v-model:visible="modalVinculorRemover"></PessoaVinculoRemoverTurmaModal>
</template>