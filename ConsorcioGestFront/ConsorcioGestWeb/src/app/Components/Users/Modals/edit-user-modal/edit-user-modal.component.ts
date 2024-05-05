import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UpdateUserDTO } from 'src/app/Models/DTO/UpdateUserDTO';
import { UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';
import { ListItemDTO } from 'src/app/Models/HelperModel/ListItemDTO';
import { ConsortiumService } from 'src/app/Services/consortium.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-edit-user-modal',
  templateUrl: './edit-user-modal.component.html',
  styleUrls: ['./edit-user-modal.component.css']
})
export class EditUserModalComponent implements OnInit {

  @Output() _CloseModal =  new EventEmitter<Boolean>();

  @Input() User = new UserModelDTO();

  condominiums:ListItemDTO[] = [];
  towers:ListItemDTO[] = [];
  profiles:ListItemDTO[] = [];
  statuses:ListItemDTO[] = [];

  selectedTower:string = "";
  selectedCondominium:number = 0;
  property:number = 0;
  userUpdated:UpdateUserDTO = new UpdateUserDTO()
  constructor(
    private UserSevice:UserService,
    private ConsortiumService:ConsortiumService
  ){

  }
  ngOnInit() {
    this.ConsortiumService.GetTowers().subscribe((towers)=>{
      this.towers = towers;
    })
    this.UserSevice.GetProfiles().subscribe((profiles)=>{
      this.profiles = profiles;
    })
    this.UserSevice.GetStatus().subscribe((statuses)=>{     
      this.statuses = statuses;
    })   
    console.log(this.User);
    
  }
  CloseModal(){
    this._CloseModal.emit(false);
    this.selectedTower = "";
    this.selectedCondominium = 0;
    console.log(this.User);
    
  }
  LoadConsortiums(tower:any){  
    this.selectedTower = tower.value;
    this.ConsortiumService.GetCondominiums(tower.value).subscribe((condominiums)=>{
      console.log(condominiums);
      this.condominiums = condominiums
    })
  }

  SelectConsortium($event:any) {
    this.selectedCondominium = $event.target.value;
    console.log(this.selectedCondominium);  
  }

  EditUser(){
    this.userUpdated.Email = this.User.email;
    this.userUpdated.Phone = this.User.phone;
    this.userUpdated.IdCondominium = this.selectedCondominium   
    this.userUpdated.IdProfile = this.User.profile?.id
    this.userUpdated.IdUserState = this.User.state?.id
    this.UserSevice.UpdateUser(this.User.document,this.userUpdated).subscribe((data)=>{
      console.log(data);
      this.CloseModal();
    })
  }
      
}
