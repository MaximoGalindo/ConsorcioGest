import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';
import { StorageService } from 'src/app/Services/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: any = {
    Email: null,
    Password: null
  };
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  roles: string[] = [];

  constructor(
    private authService: AuthService, 
    private storageService: StorageService,
    private router:Router) { }

  ngOnInit(): void {
    if (this.storageService.isLoggedIn()) {
      this.isLoggedIn = true;
      //this.router.navigate(['/main-page']);
    }
  }

  onSubmit() {   
    this.authService.login(this.form).subscribe({  
        next: (data:any) => {   
          //Hace el inciio de sesion
        this.authService.getUser().subscribe(user => {
          //Pregunta si es un admin
          if(user.profile.name == 'Admin'){
            this.storageService.saveToken(data);
            this.isLoginFailed = false;
            this.isLoggedIn = true;   
          }
          else
          {
            this.isLoginFailed = true;
            this.isLoggedIn = false;
          }
        })            
      },
      error: err => {
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
        this.isLoggedIn = false;
      }
    });

  }

  register() {
    this.router.navigate(['/register']);
  }
}
