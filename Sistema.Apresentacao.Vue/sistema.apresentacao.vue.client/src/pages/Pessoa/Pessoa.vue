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

                <p-button label="consultar"></p-button>

            </div>
        </Fieldset>

        <Toolbar class="mt-2">
            <template #start>
                <p-button label="Cadastrar pessoa" icon="pi pi-plus" @click.stop="cadastro = !cadastro;"></p-button>
            </template>
        </Toolbar>



        <div class="w-full flex flex-column gap-2" v-if="!cadastro">
            <h2 class="mt-2">Pessoas</h2>

            <DataTable :value="pessoas">
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
                        <label for="nome">Email</label>
                        <InputText id="nome" v-model="pessoa.email" required autofocus />
                    </div>

                    <p-button type="submit" label="Cadastrar" class="p-mt-2" />
                </div>
            </form>
        </div>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { useToast } from 'primevue/usetoast';
import api from '../../services/api'
import Column from 'primevue/column';
import DataTable from 'primevue/datatable';
import Toolbar from 'primevue/toolbar';

const menu = ref();
const menuModel = ref([]);




const pessoas = [
    {
        id: 1,
        nome: "Lucas Silva",
        email: "lucassmsilvadev@gmail.com",
        telefone: "65984699962",
    },
    {
        id: 2,
        nome: "Isabel Camila",
        email: "teste@gmail.com",
        telefone: "65999999999",
    },
];

const query = ref({});

const toast = useToast();

const cadastro = ref(false);
const pessoa = ref({
    nome: '',
    cpf: '',
});

const alterar = (model) => {

}

const deletar = (model) => {

}
const submitForm = async () => {

    const response = await api.post('pessoa/create', pessoa.value);

    if (response.isSuccess) {
        toast.add({ severity: 'success', summary: 'Sucesso', detail: 'Pessoa cadastrada com sucesso', life: 3000 });
        pessoa.value = { nome: '', cpf: '', dataNascimento: null };

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
            label: "Remover",
            icon: "pi pi-fw pi-trash",
            command: () => deletar(model),
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