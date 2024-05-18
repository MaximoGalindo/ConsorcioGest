import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UpdateUserDTO } from 'src/app/Models/DTO/UpdateUserDTO';
import { UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-users-gest',
  templateUrl: './users-gest.component.html',
  styleUrls: ['./users-gest.component.css']
})
export class UsersGestComponent implements OnInit{

  _ShowModalEditUser:boolean = false;
  users:UserModelDTO[] = [];
  selectedUser:UserModelDTO = new UserModelDTO();

  constructor(
    private userService:UserService,
    private modalService: NgbModal
  ){}

  ngOnInit(){
    this.LoadUsers();
  }

  LoadUsers(){
    this.userService.GetUsers().subscribe({
      next: data => {
        if (data != null)
          this.users = data;
          console.log(this.users);        
      }
    }) 
  }

  ActivateUser(userDocument:number){
    var user:UpdateUserDTO = {
      IdUserState: 1
    };
    this.userService.UpdateUser(userDocument,user).subscribe({
      next: data => {
        console.log(data);
        this.LoadUsers();
      }      
    })
  }

  DeactivateUser(userDocument:number){
    var user:UpdateUserDTO = {
      IdUserState: 2
    };
    this.userService.UpdateUser(userDocument,user).subscribe({
      next: data => {
        console.log(data);
        this.LoadUsers();
      }      
    })
  }

  EditUser(userDocument:number){
    this._ShowModalEditUser = true;
    this.userService.GetUserByDocument(userDocument).subscribe({
      next: data => {
        this.selectedUser = data;
      }
    })    
  }

  CloseModal(){
    this._ShowModalEditUser = false;
    this.LoadUsers();
  }
}
