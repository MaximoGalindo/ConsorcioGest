import { Component, OnInit } from '@angular/core';
import { UpdateUserDTO } from 'src/app/Models/DTO/UpdateUserDTO';
import { UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';
import { ConsortiumService } from 'src/app/Services/consortium.service';
import { UserService } from 'src/app/Services/user.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EditUserModalComponent } from '../Modals/edit-user-modal/edit-user-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserDataSharedService } from 'src/app/Services/Shared/user-data-shared.service';



@Component({
  selector: 'app-users-gest',
  templateUrl: './users-gest.component.html',
  styleUrls: ['./users-gest.component.css']
})
export class UsersGestComponent implements OnInit{

  users:UserModelDTO[] = [];

  constructor(
    private userService:UserService,
    private consortiumService:ConsortiumService,
    private modalService: NgbModal,
    private UserDataShared:UserDataSharedService
  ){}

  ngOnInit(){
    this.LoadUsers();
  }

  LoadUsers(){

    //ESTO ES TEMPORAL HASTA QUE HAGA LA GESTION DE CONSORCIOS
    this.consortiumService.getCurrentConsortium(1).subscribe({
      next: data => {
        console.log(data);  
        this.userService.GetUsers().subscribe({
          next: data => {
            if (data != null)
              this.users = data;
              console.log(this.users);
            
          }
        })      
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
    this.modalService.open(EditUserModalComponent)
    this.UserDataShared.setUserDocument(userDocument);
  }
}
