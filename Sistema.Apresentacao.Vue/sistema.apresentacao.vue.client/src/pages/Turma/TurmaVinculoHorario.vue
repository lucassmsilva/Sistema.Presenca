<script setup>
import { ref, computed, watch } from 'vue';
import { useToast } from 'primevue/usetoast';
import api from '../../services/api';
import Calendar from 'primevue/calendar';

const props = defineProps({
    turma: {
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
const horaInicio = ref('');
const horaFim = ref('');
const datas = ref([]);
const submit = ref(false);

const formatTime = (date) => {
    if (!date) return '';
    return date.toTimeString().substring(0, 5);
}

function criarDataComHorario(horarioStr) {
    const hoje = new Date();

    // Divide a string de horário em horas, minutos e segundos
    const [horas, minutos, segundos] = horarioStr.split(":").map(Number);

    // Configura as horas, minutos e segundos na data atual
    hoje.setHours(horas, minutos, segundos);

    return hoje;
}

const getHorarios = async () => {
    const response = await api.get('turma/find/' + props.turma.id, {});
    console.log(response)
    if (response.isSuccess) {

        const horarios = response.data.horarios;

        horaInicio.value = criarDataComHorario(horarios[0].horaInicio);
        horaFim.value = criarDataComHorario(horarios[0].horaFim);
        datas.value = horarios.map(h => new Date(h.data));

    }
    else {
        toast.add({
            severity: 'error',
            summary: 'Erro',
            detail: 'Erro ao buscar horários',
            life: 3000
        });
    }
}

const sincronizarHorarios = async () => {
    submit.value = true;

    try {
        const datasFormatadas = datas.value.map(d => {
            return d.toLocaleDateString('pt-BR');
        });

        const response = await api.post('turma/sincronizar-horarios', {
            idTurma: props.turma.id,
            horaInicio: formatTime(horaInicio.value),
            horaFim: formatTime(horaFim.value),
            datas: datasFormatadas
        });

        if (response.isSuccess) {
            toast.add({
                severity: 'success',
                summary: 'Sucesso',
                detail: 'Horários sincronizados com sucesso',
                life: 3000
            });
            cVisible.value = false;
        } else {
            toast.add({
                severity: 'error',
                summary: 'Erro',
                detail: 'Erro ao sincronizar horários',
                life: 3000
            });
        }
    } catch (error) {
        console.error(error)
        toast.add({
            severity: 'error',
            summary: 'Erro',
            detail: 'Erro ao sincronizar horários',
            life: 3000
        });
    }

    submit.value = false;
}

watch(() => props.visible, (val) => {
    if (val === true) {
        getHorarios()
    } else {
        horaInicio.value = '';
        horaFim.value = '';
        datas.value = [];
    }
})

</script>

<template>
    <PDialog v-model:visible="cVisible" modal :closeOnEscape="true" maximizable :style="{ width: '60vw' }"
        :breakpoints="{ '1199px': '75vw', '575px': '90vw' }" :pt="{
        mask: { style: 'backdrop-filter: blur(2px)' }
    }">
        <template #header>
            <div class="w-full">
                <div class="row">
                    <div class="inline-flex align-items-center justify-content-center gap-2 text-xl text-primary">
                        <span class="font-semibold white-space-nowrap">
                            Gerenciar Horários da Turma: {{ turma?.nomeTurma }}
                        </span>
                    </div>
                </div>
            </div>
        </template>

        <div class="w-full p-fluid grid mt-1">
            <div class="col-6">
                <label class="pl-2">Hora Início</label>
                <div class="field col-12">
                    <Calendar v-model="horaInicio" timeOnly :showTime="true" :showSeconds="false" inputId="timeonly" />
                </div>
                <label class="pl-2">Hora Fim</label>
                <div class="field col-12">
                    <Calendar v-model="horaFim" timeOnly :showTime="true" :showSeconds="false" inputId="timeonly" />
                </div>
            </div>

            <div class="flex flex-column gap-2 col-6">
                <label>Selecione as Datas</label>
                <Calendar v-model="datas" selectionMode="multiple" :inline="true" :showTime="false"
                    dateFormat="dd/mm/yy" />
            </div>
        </div>

        <template #footer>
            <div class="w-full flex justify-end gap-2">
                <p-button severity="danger" @click="cVisible = false" label="Cancelar">
                </p-button>
                <p-button @click="sincronizarHorarios"
                    :disabled="!horaInicio || !horaFim || datas.length === 0 || submit" label="Sincronizar Horários">
                </p-button>
                <progress-spinner v-if="submit"></progress-spinner>
            </div>
        </template>
    </PDialog>
</template>