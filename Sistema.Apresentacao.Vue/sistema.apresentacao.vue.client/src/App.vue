<template>
    <div class="layout-wrapper">
        <header class="layout-topbar">
            <div class="layout-topbar-left">
                <p-button icon="pi pi-bars" @click="toggleMenu" class="p-button-text" />
                <h1>Meu Sistema</h1>
            </div>
            <div class="layout-topbar-right">
                <p-button icon="pi pi-user" class="p-button-rounded p-button-text" />
            </div>
        </header>

        <div class="layout-sidebar" :class="{ 'active': menuActive }">
            <panel-menu :model="menu">
            </panel-menu>
        </div>

        <div class="layout-main-container">
            <div class="layout-main">
                <router-view></router-view>
            </div>
        </div>

        <footer class="layout-footer">
            <span>© 2024 Meu Sistema. Todos os direitos reservados.</span>
        </footer>
    </div>
    <Toast />
</template>

<script>
    import { ref } from 'vue';
    import { useRouter } from 'vue-router';
    import Toast from 'primevue/toast';

    export default {
        name: 'App',
        setup() {
            const menuActive = ref(true);

            const router = useRouter();

            const toggleMenu = () => {
                menuActive.value = !menuActive.value;
            };

            const menu = [
                {
                    label: 'Home',
                    icon: 'pi pi-fw pi-home',
                    to: '/',
                    command: () => {
                        router.push('/');
                    }
                },
                {
                    label: 'Pessoas',
                    icon: 'pi pi-fw pi-users',
                    to: '/pessoa',
                    command: () => {
                        router.push('/pessoa');
                    }
                },
                {
                    label: 'Configurações',
                    icon: 'pi pi-fw pi-cog',
                    to: '/configuracoes',
                    command: () => {
                        router.push('/configuracoes');
                    }
                }
            ];

            return {
                menuActive,
                toggleMenu,
                menu
            };
        }
    };
</script>

<style>
    .layout-wrapper {
        min-height: 98vh;
        display: flex;
        flex-direction: column;
    }

    .layout-topbar {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 1rem;
        background-color: var(--surface-a);
        box-shadow: 0 2px 4px -1px rgba(0, 0, 0, 0.2);
    }

    .layout-topbar-left {
        display: flex;
        align-items: center;
        gap: 1rem;
    }

    .layout-sidebar {
        width: 250px;
        background-color: var(--surface-a);
        transition: transform 0.3s;
        transform: translateX(-100%);
        z-index: 999;
    }

        .layout-sidebar.active {
            transform: translateX(0);
        }

    .layout-main-container {
        flex: 1;
        margin-left: 250px;
        padding: 2rem;
        transition: margin-left 0.3s;
    }

    .layout-footer {
        padding: 1rem;
        background-color: var(--surface-a);
        text-align: center;
    }

    @media screen and (max-width: 768px) {
        .layout-sidebar {
            transform: translateX(-100%);
        }

        .layout-main-container {
            margin-left: 0;
        }
    }
</style>