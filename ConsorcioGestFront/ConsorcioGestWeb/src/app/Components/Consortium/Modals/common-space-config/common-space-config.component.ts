import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Utils } from 'src/app/Helpers/Utils';
import { CommonSpaceConfig } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';

@Component({
  selector: 'app-common-space-config',
  templateUrl: './common-space-config.component.html',
  styleUrls: ['./common-space-config.component.css']
})
export class CommonSpaceConfigComponent {
  @Output() _ShowModal = new EventEmitter<boolean>()

  intervaloHoras = [
    '09:00', '10:00', '11:00', '12:00', '13:00', '14:00', '15:00',
    '16:00', '17:00', '18:00', '19:00', '20:00', '21:00', '22:00', '23:00','00:00',  
  ];
  @Input() commonSpaceConfig:CommonSpaceConfig = new CommonSpaceConfig()

  commonSpacesList:CommonSpaceConfig[] = []
  errorMessage: { hourFrom: string, hourTo: string, limitUsers: string } = {
    hourFrom: '',
    hourTo: '',
    limitUsers: ''
  };
  ngOnInit(){
    this.consortiumServiceShared.CommonSpaceList$.subscribe({
      next: data => {
        if(data != null){
          if(data.find((cs: any) => cs.nameSpace === this.commonSpaceConfig.nameSpace)){
            this.commonSpaceConfig = data.find((cs: any) => cs.nameSpace === this.commonSpaceConfig.nameSpace);
          }           
        }        
      }
    })
  }

  constructor(private consortiumServiceShared:ConsortiumConfigSharedService) { } 
  
  Save(){
    this.resetErrors();
    if (!this.isValid()) {
      return;
    }
    this.consortiumServiceShared.CommonSpaceList$.subscribe({
      next: data => {
        if(data != null) this.commonSpacesList = data
      }
    })
    if(this.commonSpacesList.find(cs => cs.nameSpace == this.commonSpaceConfig.nameSpace)){
      var index = this.commonSpacesList.findIndex(cs => cs.nameSpace === this.commonSpaceConfig.nameSpace);
      this.commonSpacesList[index] = this.commonSpaceConfig
    }
    else{
      this.commonSpacesList.push(this.commonSpaceConfig)
    }
    this.consortiumServiceShared.setCommonSpaceList(this.commonSpacesList)
    console.log(this.commonSpacesList);        
    this.CloseModal();
  }

  CloseModal(){
    this._ShowModal.emit(false)
  }


  isValid() {
    let valid = true;
    this.resetErrors();
  
    if (!this.commonSpaceConfig.hourFrom) {
      this.errorMessage.hourFrom = 'Hora Desde es requerida.';
      valid = false;
    }
    if (!this.commonSpaceConfig.hourTo) {
      this.errorMessage.hourTo = 'Hora Hasta es requerida.';
      valid = false;
    }

    const horaDesde = this.hourToInt(this.commonSpaceConfig.hourFrom);
    const horaHasta = this.hourToInt(this.commonSpaceConfig.hourTo);
  
    if (horaHasta <= horaDesde) {
      this.errorMessage.hourTo = 'Hora Hasta debe ser mayor que Hora Desde.';
      valid = false;
    }
  
    if (this.commonSpaceConfig.limitUsers == null || this.commonSpaceConfig.limitUsers < 1) {
      this.errorMessage.limitUsers = 'El límite de horas debe ser mayor que 0.';
      valid = false;
    }
  
    return valid;
  }

  hourToInt(hourString: string): number {
    const [hour, minute] = hourString.split(':').map(num => parseInt(num, 10));
    return hour * 60 + minute;
  }
 

  resetErrors() {
    this.errorMessage = { hourFrom: '', hourTo: '', limitUsers: '' };
  }
}
