import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-vue';

export default defineConfig({
    plugins: [plugin()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '/api': {
                target: 'http://localhost:7247/', // Altere para HTTP
                secure: false
            }
        },
        port: 5173,
        // Removendo a configuração HTTPS para utilizar HTTP apenas
    }
});
