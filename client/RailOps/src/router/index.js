import Vue from 'vue'
import Router from 'vue-router'

import Dashboard from '@/views/Dashboard'

import Roster from '@/views/roster/Roster'
import Roads from '@/views/roster/Roads'

Vue.use(Router);

export default new Router({
    mode: 'history',
    routes: [
        { 
            path: '/',
            name: 'Dashboard',
            component: Dashboard
        },
        { 
            path: '/roster',
            component: Roster,
            children: [
                { 
                    path: 'roads',
                    component: Roads
                }
            ]
        }
    ]
})