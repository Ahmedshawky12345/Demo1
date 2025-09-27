import { Routes } from '@angular/router';
import { CategoryComponent } from './features/Category/category/category.component';
import { HomeComponent } from './features/home/Home/home.component';

export const routes: Routes = [
    {path:"",redirectTo:"home",pathMatch:"full"},
      { path: 'home', component: HomeComponent },

    {path:"categories",component:CategoryComponent}
];
