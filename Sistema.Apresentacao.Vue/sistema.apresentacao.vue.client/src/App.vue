<template>
    <div class="layout-wrapper">
        <ConfirmDialog></ConfirmDialog>
        <Toast />


        <!-- Topbar -->
        <header class="layout-topbar">
            <div class="layout-topbar-left">
                <p-button icon="pi pi-bars" @click="toggleMenu" class="p-button-text menu-button" />
                <img height="120px" src="./assets/logo-presenca-nobackground.svg" alt="Presença" @click="goHome" style="cursor: pointer;" />
            </div>
            <div class="layout-topbar-right">
                <p-button icon="pi pi-user" class="p-button-rounded p-button-text" />
            </div>
        </header>

        <!-- Sidebar -->
        <div class="layout-sidebar" :class="{ 'active': menuActive }">
            <div class="sidebar-header">
                <span class="logo-text">Presença</span>
            </div>
            <panel-menu :model="menu" class="menu-items" />
        </div>

        <!-- Main Content -->
        <div class="layout-main-container" :class="{ 'menu-active': menuActive }">
            <div class="layout-main">
                <router-view></router-view>
            </div>
        </div>

        <!-- Footer -->
        <footer class="layout-footer">
            <span> 2024 Sistema Presença. Todos os direitos reservados.</span>
        </footer>
    </div>

</template>

<script>
    import { ref } from 'vue';
    import { useRouter } from 'vue-router';
    import Toast from 'primevue/toast';
    import PanelMenu from 'primevue/panelmenu'
    import ConfirmDialog from 'primevue/confirmdialog';


    export default {
        name: 'App',
        components: {
            Toast,
            PanelMenu,
            ConfirmDialog

        },
        setup() {
            const menuActive = ref(true);
            const router = useRouter();

            const toggleMenu = () => {
                menuActive.value = !menuActive.value;
            };

            const menu = [
                {
                    label: 'Início',
                    icon: 'pi pi-home',
                    to: '/',
                    command: () => router.push('/')
                },
                {
                    label: 'Pessoas',
                    icon: 'pi pi-users',
                    to: '/pessoa',
                    command: () => router.push('/pessoa')
                },
                {
                    label: 'Turma',
                    icon: 'pi pi-book',
                    to: '/pessoa',
                    command: () => router.push('/turma')
                },
                {
                    label: 'Presença',
                    icon: 'pi pi-check-square',
                    to: '/pessoa',
                    command: () => router.push('/presenca')
                },
                {
                    label: 'Configuraçoes',
                    icon: 'pi pi-cog',
                    to: '/configuracoes',
                    command: () => router.push('/configuracoes')
                }
            ];

            const goHome = () => router.push('/')

            return {
                menuActive,
                toggleMenu,
                menu,
                goHome
            };
        }
    };
</script>

<style>
    /* Reset e estilos base */
    * {
        margin: 0;
        padding: 0;
        box-sizing: border-box;
    }

    /* Layout principal */
    .layout-wrapper {
        min-height: 100vh;
        display: flex;
        flex-direction: column;
        background-color: #f8f9fa;
    }

    /* Topbar */
    .layout-topbar {
        position: fixed;
        top: 0;
        left: 0;
        right: 0;
        height: 4rem;
        padding: 0 2rem;
        background-color: #ffffff;
        display: flex;
        align-items: center;
        justify-content: space-between;
        box-shadow: 0 1px 3px rgba(0,0,0,0.1);
        z-index: 999;
    }

    .layout-topbar-left {
        display: flex;
        align-items: center;
        gap: 1rem;
    }

        .layout-topbar-left h1 {
            font-size: 1.25rem;
            color: #1a1a1a;
            font-weight: 500;
        }

    /* Sidebar */
    .layout-sidebar {
        position: fixed;
        left: 0;
        top: 0;
        width: 250px;
        height: 100vh;
        background-color: #ffffff;
        box-shadow: 2px 0 4px rgba(0,0,0,0.05);
        z-index: 998;
        transition: transform .3s;
        transform: translateX(-100%);
    }

        .layout-sidebar.active {
            transform: translateX(0);
        }

    .sidebar-header {
        height: 4rem;
        display: flex;
        align-items: center;
        justify-content: center;
        border-bottom: 1px solid #f0f0f0;
    }

    .logo-text {
        font-size: 1.5rem;
        font-weight: 600;
        color: #2563eb;
    }

    /* Menu Items */
    .menu-items {
        padding: 1rem 0;
    }


    /* Main Container */
    .layout-main-container {
        margin-top: 4rem;
        padding: 2rem;
        transition: margin-left .3s;
    }

        .layout-main-container.menu-active {
            margin-left: 250px;
        }

    .layout-main {
        background-color: #ffffff;
        padding: 2rem;
        border-radius: 8px;
        box-shadow: 0 1px 3px rgba(0,0,0,0.05);
    }

    /* Footer */
    .layout-footer {
        background-color: #ffffff;
        padding: 1rem;
        text-align: center;
        color: #6b7280;
        margin-top: auto;
        border-top: 1px solid #f0f0f0;
    }

    /* Responsividade */
    @media screen and (max-width: 768px) {
        .layout-main-container.menu-active {
            margin-left: 0;
        }

        .layout-sidebar {
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }

        .layout-topbar {
            padding: 0 1rem;
        }

        .layout-main-container {
            padding: 1rem;
        }

        .layout-main {
            padding: 1.5rem;
        }
    }
</style>