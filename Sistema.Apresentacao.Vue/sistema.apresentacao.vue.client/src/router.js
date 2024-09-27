import { createMemoryHistory, createRouter } from 'vue-router'

import Pessoa from './pages/Pessoa.vue'
import Home from './pages/Home.vue'
import Configuracoes from './pages/Configuracoes.vue'

const routes = [
    { path: '/', component: Home },
    { path: '/pessoa', component: Pessoa },
    { path: '/configuracoes', component: Configuracoes },
]

const router = createRouter({
    history: createMemoryHistory(),
    routes,
})

export default router;