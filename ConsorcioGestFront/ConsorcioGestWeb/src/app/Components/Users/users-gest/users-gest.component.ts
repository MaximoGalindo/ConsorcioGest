import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { filter } from 'rxjs';
import { FilterUserDTO } from 'src/app/Models/DTO/FiltersDTO';
import { UpdateUserDTO } from 'src/app/Models/DTO/UpdateUserDTO';
import { UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';
import { ListItemDTO } from 'src/app/Models/HelperModel/ListItemDTO';
import { ConsortiumService } from 'src/app/Services/consortium.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-users-gest',
  templateUrl: './users-gest.component.html',
  styleUrls: ['./users-gest.component.css']
})
export class UsersGestComponent implements OnInit{

  isAdmin:boolean = false;

  _ShowModalEditUser:boolean = false;
  users:UserModelDTO[] = [];
  userDocument:number = 0;
  towers:ListItemDTO[] = [];
  statuses:ListItemDTO[] = [];
  condominiums:ListItemDTO[] = [];

  //Filter
  selectedTower:string = '';
  document:string = "";
  seletedStatus:number = 0;
  selectedCondominium:string = '';

  constructor(
    private userService:UserService,
    private modalService: NgbModal,
    private ConsortiumService:ConsortiumService,
    private route:ActivatedRoute
  ){}

  ngOnInit(){
    this.route.data.subscribe(data => {     
      this.isAdmin = data['isAdmin'];

      var filter = new FilterUserDTO();
      this.LoadUsers(filter);

      if(!this.isAdmin){
        this.ConsortiumService.GetTowers().subscribe((towers)=>{
          this.towers = towers;
        })
    
        this.userService.GetStatus().subscribe((statuses)=>{     
          this.statuses = statuses;
        })     
      } 
    });  
  }

  LoadConsortiums(tower:any){  
    this.selectedTower = tower.value;
    this.ConsortiumService.GetCondominiums(tower.value).subscribe((condominiums)=>{
      console.log(condominiums);
      this.condominiums = condominiums
    })
  }

  LoadUsers(filter:FilterUserDTO){
    if(this.isAdmin){
      this.userService.GetAdminUsers().subscribe({
        next: data => {
          if (data != null)
            this.users = data;    
          console.log(this.users);  
        }
      })
    }
    else{
      this.userService.GetUsers(filter).subscribe({
        next: data => {
          if (data != null)
            this.users = data;      
        }
      }) 
    }

  }

  Search(){
    var filter = new FilterUserDTO();

    
    filter.document = parseInt(this.document);
    filter.userStateID = this.seletedStatus;
    filter.codominium = this.selectedCondominium;
    filter.tower = this.selectedTower;
    this.LoadUsers(filter);
  }

  ActivateUser(userDocument:number){
    var user:UpdateUserDTO = {
      IdUserState: 1
    };
    this.userService.UpdateUser(userDocument,user).subscribe({
      next: data => {
        console.log(data);
        var filter = new FilterUserDTO();
        this.LoadUsers(filter);
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
        var filter = new FilterUserDTO();
        this.LoadUsers(filter);
      }      
    })
  }

  EditUser(userDocument:number){
    this._ShowModalEditUser = true;
    this.userDocument = userDocument;
  }

  CloseModal(){
    this._ShowModalEditUser = false;
    var filter = new FilterUserDTO();
    this.LoadUsers(filter);
  }
}
