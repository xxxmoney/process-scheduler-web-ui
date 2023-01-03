import { createWebHistory, createRouter } from "vue-router";
import Home from "@/pages/Home.vue";
import i18n from '../i18n'

const routes = [
    {
        path: '/',
        redirect: `/${i18n.locale}`
    },
    {
        path: '/:lang',
        component: Home,
        children: [
            
        ]
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;