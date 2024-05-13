import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';

@Injectable({
  providedIn: 'root'
})
export class UserDataSharedService {

  private user = new BehaviorSubject<any>(null);

  User$ = this.user.asObservable();

  setUser(data:UserModelDTO){
    this.user.next(data);
  }
  constructor() { }
}
