<template>
    <div class="card">
        <h2>Cadastro de Pessoa</h2>
        <form @submit.prevent="submitForm">
            <div class="p-fluid">
                <div class="p-field">
                    <label for="nome">Nome</label>
                    <InputText id="nome" v-model="pessoa.nome" required autofocus />
                </div>
                <div class="p-field">
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
    import axios from 'axios';

    const toast = useToast();

    const pessoa = ref({
        nome: '',
        cpf: '',
    });

    const submitForm = async () => {
        try {
            const response = await axios.post('https://localhost:7247/api/pessoa/create', pessoa.value);
            toast.add({ severity: 'success', summary: 'Sucesso', detail: 'Pessoa cadastrada com sucesso', life: 3000 });
            // Limpar o formulário após o cadastro bem-sucedido
            pessoa.value = { nome: '', cpf: '', dataNascimento: null };
        } catch (error) {
            toast.add({ severity: 'error', summary: 'Erro', detail: 'Erro ao cadastrar pessoa', life: 3000 });
            console.error('Erro ao cadastrar pessoa:', error);
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