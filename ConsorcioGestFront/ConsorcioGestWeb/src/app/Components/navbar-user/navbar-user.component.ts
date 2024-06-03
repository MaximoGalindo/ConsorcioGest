import { Component, ElementRef, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';
import { UserDataSharedService } from 'src/app/Services/Shared/user-data-shared.service';
import { StorageService } from 'src/app/Services/storage.service';

@Component({
  selector: 'app-navbar-user',
  templateUrl: './navbar-user.component.html',
  styleUrls: ['./navbar-user.component.css']
})
export class NavbarUserComponent {
  isMenuOpen: boolean = false;
  User:UserModelDTO = new UserModelDTO();
  title:string = "Inicio";
  constructor(private router:Router,
   private SharedDataUser:UserDataSharedService
  ) {
    
  }

  ngOnInit(){
    this.SharedDataUser.User$.subscribe((data)=>{
      this.User = data;
    })     
  }

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

  GoToClaims(){
    this.router.navigate(['/main-page-user/claim-user']);
  }
}
