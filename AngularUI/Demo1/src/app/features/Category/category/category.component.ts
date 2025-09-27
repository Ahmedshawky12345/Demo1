import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../../Core/Services/category.service';
import { Category } from '../../../Core/Models/category.model';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})

export class CategoryComponent implements OnInit {

constructor(private categoryService:CategoryService){

}
  ngOnInit(): void {
   this.GetCategory()
  }
categoris:Category[]=[]
  GetCategory(){
    this.categoryService.GetCategory(1,3).subscribe({
      next:(response)=>{
this.categoris=response.data.data
console.log(this.categoris)
      },
      error:(err)=>{
        console.log(err)
      }
    })
  }

}
