<template>
    <div class="card">
        <h2>Cadastro de Pessoa</h2>
        <form @submit.prevent="submitForm">
            <div class="p-fluid  mt-4">
                <div class="flex col-3 gap-2 flex-column">
                    <label for="nome">Nome</label>
                    <InputText id="nome" v-model="pessoa.nome" required autofocus />
                </div>
                <div class="flex gap-2 col-3 flex-column">
                    <label for="cpf">CPF</label>
                    <InputMask id="cpf" v-model="pessoa.cpf" mask="999.999.999-99" required />
                </div>
                <!--<div class="p-field">
                    <label for="dataNascimento">Data de Nascimento</label>
                    <Calendar id="dataNascimento" v-model="pessoa.dataNascimento" dateFormat="dd/mm/yy" required />
                </div>-->
                <p-button type="submit" label="Cadastrar" class="p-mt-2" />
            </div>
        </form>
    </div>
</template>

<script setup>
import { ref } from 'vue';
import { useToast } from 'primevue/usetoast';
import api from '../../services/api'

const toast = useToast();

const pessoa = ref({
    nome: '',
    cpf: '',
});

const submitForm = async () => {

    const response = await api.post('pessoa/create', pessoa.value);

    if (response.isSuccess) {
        toast.add({ severity: 'success', summary: 'Sucesso', detail: 'Pessoa cadastrada com sucesso', life: 3000 });
        pessoa.value = { nome: '', cpf: '', dataNascimento: null };

    } else {
        toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao cadastrar pessoa' + response.error, life: 3000 });
    }
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