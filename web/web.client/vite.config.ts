import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import { env } from 'process';
import tailwindcss from "@tailwindcss/vite";
import react from '@vitejs/plugin-react';

const target = env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:7224';

// https://vitejs.dev/config/
export default defineConfig({
    base: './',
    plugins: [
        tailwindcss(),
        plugin(),
        react()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    build: {
        emptyOutDir: true,
    },
    server: {
        proxy: {
            '^/weatherforecast': {
                target,
                secure: false
            }
        },
        port: parseInt(env.DEV_SERVER_PORT || '56842')
    }
})
