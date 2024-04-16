import { Component, OnInit } from '@angular/core';
import { UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';
import { UserModel } from 'src/app/Models/Models/UserModel';
import { UserDataSharedService } from 'src/app/Services/Shared/user-data-shared.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-edit-user-modal',
  templateUrl: './edit-user-modal.component.html',
  styleUrls: ['./edit-user-modal.component.css']
})
export class EditUserModalComponent implements OnInit {

   userDocument:number = 0; 
   _UserIsPresent:boolean = false;

   User = new UserModelDTO();

  constructor(
    private UserDataShared:UserDataSharedService,
    private UserSevice:UserService
  ){

  }
  ngOnInit(): void {
    this.UserDataShared.UserDocument$.subscribe((data)=>{
      this.userDocument = data
      this._UserIsPresent = true
      this.GetUser(this.userDocument)
    })
  }
  closeModal(){
    this._UserIsPresent = false
    this.userDocument = 0;
  }

  GetUser(userDocument:number){
    this.UserSevice.GetUserByDocument(userDocument).subscribe((data)=>{
      this.User = data
      console.log(this.User);
      
    })
  }

}
