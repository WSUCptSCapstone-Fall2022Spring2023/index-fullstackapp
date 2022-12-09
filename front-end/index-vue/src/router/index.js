import { createRouter, createWebHistory} from 'vue-router'
import HomePage from './../components/HomePage.vue'
import EventsPage from '../components/EventsPage.vue'
import AboutUs from '../components/AboutUs.vue'
const routes = [
    {
        path: '/',
        name: 'HomePage',
        component: HomePage
    },
    {
        path: '/events',
        name: 'EventsPage',
        component: EventsPage
    },
    {
        path: '/about',
        name: 'AboutUs',
        component: AboutUs
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router