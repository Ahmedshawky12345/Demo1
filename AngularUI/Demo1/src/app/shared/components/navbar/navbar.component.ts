import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterModule, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'] // ✅ خليها "styleUrls" مش "styleUrl"
})
export class NavbarComponent {
 navItems = [
  { name: 'Home', icon: '🏠', link: '/' },
  { name: 'Categories', icon: '📚', link: '/categories' },
  { name: 'Favorites', icon: '❤️', link: '/favorites' },
  { name: 'Cart', icon: '🛒', link: '/cart' },
  { name: 'Account', icon: '👤', link: '/profile' }
];
  isMenuOpen = false;
  isOnboardingPage = false; // 🟡 عشان نعرف لو احنا في صفحة الـ onboarding

  constructor(private router: Router) {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.isOnboardingPage = event.url === '/onboarding';
      }
    });
  }

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
}
