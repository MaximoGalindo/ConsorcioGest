import { Component, OnInit } from '@angular/core';
import { UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';
import { ConsortiumService } from 'src/app/Services/consortium.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-users-gest',
  templateUrl: './users-gest.component.html',
  styleUrls: ['./users-gest.component.css']
})
export class UsersGestComponent implements OnInit{

  users:UserModelDTO[] = [];

  constructor(
    private userService:UserService,
    private consortiumService:ConsortiumService
  ){}

  ngOnInit(){
    this.LoadUsers();
  }

  LoadUsers(){

    //ESTO ES TEMPORAL HASTA QUE HAGA LA GESTION DE CONSORCIOS
    this.consortiumService.getCurrentConsortium(1).subscribe({
      next: data => {
        console.log(data);        
      }
    })  

    this.userService.GetUsers().subscribe({
      next: data => {
        if (data != null)
          this.users = data;
          console.log(this.users);
        
      }
    })
  }
}
