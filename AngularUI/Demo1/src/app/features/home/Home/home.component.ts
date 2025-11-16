import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { CategoryComponent } from "../../Category/category/category.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
 currentSlide = 1;
  totalSlides = 3;
  floatingBooks: any[] = [];

  constructor(private router: Router) {}

  ngOnInit() {
    
  }

 
}