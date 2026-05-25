import { Routes } from '@angular/router';

import { MainLayout } from './layout/main-layout/main-layout';

import { Dashboard } from './features/vehicles/pages/dashboard/dashboard';

import { RegisterEntry } from './features/vehicles/pages/register-entry/register-entry';

import { ActiveVehicles } from './features/vehicles/pages/active-vehicles/active-vehicles';

import { RegisterExit } from './features/vehicles/pages/register-exit/register-exit';

export const routes: Routes = [
  {
    path: '',
    component: MainLayout,
    children: [
      {
        path: '',
        component: Dashboard
      },
      {
        path: 'register-entry',
        component: RegisterEntry
      },
      {
        path: 'active-vehicles',
        component: ActiveVehicles
      },
      {
        path: 'register-exit',
        component: RegisterExit
      }
    ]
  }
];