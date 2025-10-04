import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../../Core/Services/category.service';
import { Category } from '../../../Core/Models/category.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})

export class CategoryComponent  {

// constructor(private categoryService:CategoryService){

// }
categories = [
  { id: 'fiction', icon: '📖', title: 'Fiction', count: '1,250 books' },
  { id: 'science', icon: '🔬', title: 'Science', count: '890 books' },
  { id: 'history', icon: '🏛️', title: 'History', count: '670 books' },
  { id: 'technology', icon: '💻', title: 'Technology', count: '920 books' },
  { id: 'philosophy', icon: '💭', title: 'Philosophy', count: '430 books' },
  { id: 'cooking', icon: '👨‍🍳', title: 'Cooking', count: '310 books' },
  { id: 'development', icon: '🚀', title: 'Self Development', count: '580 books' },
  { id: 'literature', icon: '✍️', title: 'Literature', count: '750 books' }
];
constructor(private router: Router) {}

  exploreCategory(category: string) {
    this.router.navigate(['/books'], { queryParams: { category } });
  }
//   ngOnInit(): void {
//    this.GetCategory()
//   }
// categoris:Category[]=[]
//   GetCategory(){
//     this.categoryService.GetCategory(1,3).subscribe({
//       next:(response)=>{
// this.categoris=response.data.data
// console.log(this.categoris)
//       },
//       error:(err)=>{
//         console.log(err)
//       }
//     })
//   }

}
