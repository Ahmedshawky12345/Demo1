import { Routes } from '@angular/router';
import { CategoryComponent } from './features/Category/category/category.component';
import { HomeComponent } from './features/home/Home/home.component';
import { ProductComponent } from './features/Products/product/product.component';

export const routes: Routes = [
    {path:"",redirectTo:"home",pathMatch:"full"},
      { path: '', component: HomeComponent },

    {path:"categories",component:CategoryComponent},
    {path:"product",component:ProductComponent} ,
];
