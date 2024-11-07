import { createMemoryHistory, createRouter } from 'vue-router'

import Pessoa from './pages/Pessoa/PessoaPage.vue'
import Home from './pages/HomePage.vue'
import Configuracoes from './pages/Configuracoes/ConfiguracoesPage.vue'
import Turma from './pages/Turma/TurmaPage.vue'
import Presenca from './pages/Presenca/PresencaPage.vue'

const routes = [
    { path: '/', component: Pessoa },
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