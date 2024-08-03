import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UpdateUserDTO } from 'src/app/Models/DTO/UpdateUserDTO';
import { UserModelByDocumentDTO, UserModelDTO } from 'src/app/Models/DTO/UserModelDTO';
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

  User = new UserModelByDocumentDTO();
  @Input() userDocument:number = 0;
  @Input() isAdmin:boolean = false;

  condominiums:ListItemDTO[] = [];
  towers:ListItemDTO[] = [];
  profiles:ListItemDTO[] = [];
  statuses:ListItemDTO[] = [];

  selectedTower:string = "";
  selectedCondominium:number | undefined = 0;
  condominium:string | undefined = "";
  selectedProfile:number | undefined = 0;
  selectedState:number | undefined = 0;
  property:number = 0;
  userUpdated:UpdateUserDTO = new UpdateUserDTO()
  constructor(
    private UserSevice:UserService,
    private ConsortiumService:ConsortiumService
  ){

  }
  ngOnInit() {
    this.UserSevice.GetUserByDocument(this.userDocument).subscribe({
      next: data => {
        this.User = data;
        if(this.User.state.id === 1 || this.User.state.id === 2){
          this.selectedProfile = this.User.profile?.id;
          this.selectedTower = this.User.tower;     
          this.selectedState = this.User.state?.id;  
          this.LoadConsortiums(this.selectedTower);
        }
      }
    })    
    if(!this.isAdmin){
      this.ConsortiumService.GetTowers().subscribe((towers)=>{
        this.towers = towers;
      })
    }

    this.UserSevice.GetProfiles().subscribe((profiles)=>{
      this.profiles = profiles;
    })
    this.UserSevice.GetStatus().subscribe((statuses)=>{     
      this.statuses = statuses;
    })   
  }
  CloseModal(){
    this._CloseModal.emit(false);
    this.selectedTower = "";
    this.selectedCondominium = 0;    
  }
  LoadConsortiums(tower:any){  
    if(this.User.tower === null){
      this.selectedTower = tower.value;
    }    
    this.ConsortiumService.GetCondominiums(this.selectedTower).subscribe((condominiums)=>{
      this.condominiums = condominiums
      this.selectedCondominium = this.condominiums.find(x => x.name === this.User.condominium)?.id;
    })
  }

  SelectConsortium($event:any) {
    this.selectedCondominium = parseInt($event.target.value);
    this.condominium = this.condominiums.find(x => x.id === this.selectedCondominium)?.name;
  }

  EditUser(){
    this.userUpdated.Email = this.User.email;
    this.userUpdated.Phone = this.User.phone;
    this.userUpdated.IdCondominium = this.selectedCondominium   
    this.userUpdated.IdProfile = this.selectedProfile
    this.userUpdated.IdUserState = this.selectedState

    

    this.UserSevice.UpdateUser(this.User.id,this.userUpdated).subscribe((data)=>{
      console.log(data);
      this.CloseModal();
    })
  }
      
}
