import { Injectable } from '@angular/core';
import { UserModelDTO } from '../Models/DTO/UserModelDTO';

const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor() {}

  clean(): void {
    window.sessionStorage.clear();
  }

  public saveToken(token:string): void {   
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY,token);    
  }

  public getToken(): any {
    const token = window.sessionStorage.getItem(USER_KEY); 
    return token;
  }

  public isLoggedIn(): boolean {
    const user = window.sessionStorage.getItem(USER_KEY);
    if (user) {
      return true;
    }
    return false;
  }
}
