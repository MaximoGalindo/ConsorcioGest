import { Component, ElementRef, HostListener } from '@angular/core';

@Component({
  selector: 'app-navbar-user',
  templateUrl: './navbar-user.component.html',
  styleUrls: ['./navbar-user.component.css']
})
export class NavbarUserComponent {
  isMenuOpen: boolean = false;

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
  closeMenu(event: MouseEvent) {
    if (this.isMenuOpen && !(event.target as HTMLElement).closest('.menu')) {
      this.isMenuOpen = false;
    }
  }

  @HostListener('document:click', ['$event'])
  onClick(event: MouseEvent) {
      if (!(event.target as HTMLElement).closest('.menu-toggle')) {
      this.isMenuOpen = false;
    }
  }
}
