import { Component } from '@angular/core';
import { StorageService } from './Services/storage.service';
import { AuthService } from './Services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ConsorcioGestWeb';
  isLoggedIn = false;
  showAdminBoard = false;
  showModeratorBoard = false;

  constructor(private storageService: StorageService, private authService: AuthService) { }

  ngOnInit(): void {
    this.isLoggedIn = !!this.storageService.isLoggedIn();

    if (this.isLoggedIn) {
      const user = this.storageService.getToken();
      
    //  this.showAdminBoard = this.roles.includes('ROLE_ADMIN');
    // this.showModeratorBoard = this.roles.includes('ROLE_MODERATOR');
    }
  }

 /* logout(): void {
    this.authService.logout().subscribe({
      next: res => {
        console.log(res);
        this.storageService.clean();

        window.location.reload();
      },
      error: err => {
        console.log(err);
      }
    });
  }*/
}
