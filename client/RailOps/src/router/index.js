import Vue from 'vue'
import Router from 'vue-router'

import Dashboard from '@/views/Dashboard'

import Roster from '@/views/roster/Roster'
import Roads from '@/views/roster/Roads'
import EnginesList from '@/views/roster/engines/EngineList'
import NewEngine from '@/views/roster/engines/NewEngine'
import EditEngine from '@/views/roster/engines/EditEngine'

Vue.use(Router);

// Apparently you can't nest more than 1 level

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
                },
                {
                    path: 'engines',
                    component: EnginesList
                },
                { 
                    path: 'engines/new',
                    component: NewEngine
                },
                {
                    name: 'EditEngine',
                    path: 'engines/edit/:id',
                    component: EditEngine
                }
            ]
        }
    ]
})