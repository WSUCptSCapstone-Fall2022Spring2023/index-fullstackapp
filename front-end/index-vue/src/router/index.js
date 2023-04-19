import { createRouter, createWebHistory} from 'vue-router'
import HomePage from './../components/HomePage.vue'
import EventsPage from '../components/EventsPage.vue'
import AboutUs from '../components/AboutUs.vue'
import NewsPage from '../components/NewsPage.vue'
import Staff from '../components/Staff.vue'
import Event from '../components/Event.vue'
import News from '../components/News.vue'
import Contacts from '../components/Contacts.vue'
import Contact from '../components/Contact.vue'
import Speciality from '../components/Specialty.vue'
import Resources from '../components/Resources.vue'
import Resource from '../components/Resource.vue'

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
        path: '/event/:id',
        name: 'Event',
        component: Event
    },
    {
        path: '/news',
        name: 'Newsletter',
        component: NewsPage
    },
    {
        path: '/newsletter/:id',
        name: 'news',
        component: News
    },
    {
        path: '/about',
        name: 'AboutUs',
        component: AboutUs
    },
    {
        path: '/contact',
        name: 'Contacts',
        component: Contacts
    },
    {
        path: '/contact/:id',
        name: 'Contact',
        component: Contact
    },
    {
        path: '/staff/:id',
        name: 'staff',
        component: Staff
    },
    {
        path: '/programs-services/:id',
        name: 'Speciality',
        component: Speciality
    },
    {
        path: '/resources',
        name: 'Resources',
        component: Resources
    },
    {
        path: '/resources/:id',
        name: 'Resource',
        component: Resource
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
  })

export default router