import { createMemoryHistory, createRouter } from 'vue-router'

import Pessoa from './pages/Pessoa/Pessoa.vue'
import Home from './pages/Home.vue'
import Configuracoes from './pages/Configuracoes/Configuracoes.vue'
import Turma from './pages/Turma/Turma.vue'
import Presenca from './pages/Presenca/Presenca.vue'

const routes = [
    { path: '/', component: Home },
    { path: '/pessoa', component: Pessoa },
    { path: '/turma', component: Turma },
    { path: '/presenca', component: Presenca },
    { path: '/configuracoes', component: Configuracoes },
]

const router = createRouter({
    history: createMemoryHistory(),
    routes,
})

export default router;