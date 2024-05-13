import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';
import { UserModel } from 'src/app/Models/Models/UserModel';
import { UserDataSharedService } from 'src/app/Services/Shared/user-data-shared.service';
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
    private router:Router,
    private userSharedService:UserDataSharedService
  ) { }

  ngOnInit(): void {
    if (this.storageService.isLoggedIn()) {
      this.isLoggedIn = true;
      //this.router.navigate(['/main-page']);
    }
  }

  onSubmit() {   
    console.log(this.form);    
    this.authService.login(this.form).subscribe({  
      next: (data) => {                
        this.storageService.saveToken(data.token); 
        
        this.SaveUser(data);       

        if(data.profile.name == 'Admin'){
          this.isLoginFailed = false;
          this.isLoggedIn = true;  
          this.router.navigate(['/consortium'])
        }
        else if(data.profile.name == 'Residente')
        {
          this.router.navigate(['/main-page-user']);
          this.isLoginFailed = true;
          this.isLoggedIn = false;
        } 
      },
      error: err => {
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
        this.isLoggedIn = false;
      }
    });   
  }
  SaveUser(data: UserModel) {
    var User = new UserModelDTO();
    User.condominium = data.condominio
    User.name = data.name + ' ' + data.lastName
    this.userSharedService.setUser(User);
  }

  register(parametro:string) {
    this.router.navigate(['/register',{ userType: parametro }]);
  }
}
