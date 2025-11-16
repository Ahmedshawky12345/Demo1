import { Component, HostListener } from '@angular/core';
import { CategoryService } from '../../../Core/Services/category.service';
import { Category } from '../../../Core/Models/category.model';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent {
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

  showScrollButton = false;

  constructor(private router: Router) {}

  // Listen to scroll events
  @HostListener('window:scroll', [])
  onWindowScroll() {
    // Show button when scrolled down 300px
    this.showScrollButton = window.scrollY > 300;
  }

  exploreCategory(category: string) {
    this.router.navigate(['/books'], { queryParams: { category } });
  }

  // Scroll to top function
  scrollToTop() {
    window.scrollTo({
      top: 0,
      behavior: 'smooth'
    });
  }

  // Alternative: Scroll to specific section
  scrollToSection(sectionId: string) {
    const element = document.getElementById(sectionId);
    if (element) {
      element.scrollIntoView({
        behavior: 'smooth',
        block: 'start'
      });
    }
  }
}