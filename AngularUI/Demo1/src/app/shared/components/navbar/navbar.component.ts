import { CommonModule } from '@angular/common';
import { Component, HostListener, OnInit, OnDestroy, Inject, PLATFORM_ID } from '@angular/core';
import { Router, RouterModule, NavigationEnd } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, OnDestroy {
  navItems = [
    { name: 'Home', icon: '🏠', link: '/', sectionId: 'home-section' },
    { name: 'Categories', icon: '📚', link: '/', sectionId: 'categories-section' },
    { name: 'Favorites', icon: '❤️', link: '/favorites' },
    { name: 'Cart', icon: '🛒', link: '/cart' },
    { name: 'Account', icon: '👤', link: '/profile' }
  ];
  
  isMenuOpen = false;
  isScrolled = false;
  isLightBackground = true; // افتراضي light عشان يظهر على كل الصفحات

  private observer: IntersectionObserver | undefined;

  constructor(
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: any
  ) {}

  ngOnInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.setupIntersectionObserver();
    }
  }

  ngOnDestroy() {
    if (this.observer) {
      this.observer.disconnect();
    }
  }

  private setupIntersectionObserver() {
    this.observer = new IntersectionObserver((entries) => {
      entries.forEach(entry => {
        // إذا دخلنا منطقة الـ home (الداكنة) غير اللون لأبيض
        if (entry.target.id === 'home-section') {
          this.isLightBackground = !entry.isIntersecting;
        }
      });
    }, { 
      threshold: 0.1,
      rootMargin: '-100px 0px 0px 0px' // ابدأ الفحص قبل الـ section بـ 100px
    });

    // راقب الـ home section
    const homeSection = document.getElementById('home-section');
    if (homeSection) {
      this.observer.observe(homeSection);
    }

    // راقب الـ categories section ك backup
    const categoriesSection = document.getElementById('categories-section');
    if (categoriesSection) {
      this.observer.observe(categoriesSection);
    }
  }

  @HostListener('window:scroll')
  onWindowScroll() {
    this.isScrolled = window.pageYOffset > 50;
    
    // طريقة بديلة إذا Intersection Observer مش شغالة
    this.checkScrollPosition();
  }

  private checkScrollPosition() {
    const homeSection = document.getElementById('home-section');
    if (!homeSection) return;

    const homeRect = homeSection.getBoundingClientRect();
    
    // إذا خرجنا من منطقة الـ home (أي وصلنا للـ categories)
    if (homeRect.bottom < 100) {
      this.isLightBackground = true;
    } else {
      this.isLightBackground = false;
    }
  }

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
  }

  scrollToSection(sectionId: string): void {
    this.isMenuOpen = false;
    const element = document.getElementById(sectionId);
    if (element) {
      element.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
  }

  onNavItemClick(item: any, event: Event): void {
    event.preventDefault();
    if (item.sectionId) {
      this.scrollToSection(item.sectionId);
    } else {
      this.router.navigate([item.link]);
    }
    this.isMenuOpen = false;
  }
}