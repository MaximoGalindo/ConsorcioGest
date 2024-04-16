import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserDataSharedService {

  private userDocument = new BehaviorSubject<any>(null);

  UserDocument$ = this.userDocument.asObservable();

  setUserDocument(data:number){
    this.userDocument.next(data);
  }
  constructor() { }
}
