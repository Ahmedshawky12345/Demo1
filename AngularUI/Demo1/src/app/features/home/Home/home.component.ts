import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

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
    this.createFloatingBooks();
  }

  createFloatingBooks() {
    for (let i = 0; i < 12; i++) {
      this.floatingBooks.push({
        style: {
          'left': Math.random() * 100 + '%',
          'top': Math.random() * 100 + '%',
          'animation-delay': Math.random() * 5 + 's',
          'transform': 'rotate(' + (Math.random() * 360) + 'deg)'
        }
      });
    }
  }

  nextSlide() {
    if (this.currentSlide < this.totalSlides) {
      this.currentSlide++;
    } else {
      this.skipOnboarding();
    }
  }

  goToSlide(slideNumber: number) {
    this.currentSlide = slideNumber;
  }

  skipOnboarding() {
    // الانتقال للصفحة الرئيسية
    this.router.navigate(['/home']);
  }
}