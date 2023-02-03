import { createRouter, createWebHistory} from 'vue-router'
import HomePage from './../components/HomePage.vue'
import EventsPage from '../components/EventsPage.vue'
import AboutUs from '../components/AboutUs.vue'
import NewsPage from '../components/NewsPage.vue'
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
        path: '/news',
        name: 'Newsletter',
        component: NewsPage
    },
    {
        path: '/about',
        name: 'AboutUs',
        component: AboutUs
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
  })

export default router