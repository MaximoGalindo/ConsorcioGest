import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonSpaceConfig } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ConsortiumConfigSharedService } from 'src/app/Services/Shared/consortium-config-shared.service';

@Component({
  selector: 'app-common-space-config',
  templateUrl: './common-space-config.component.html',
  styleUrls: ['./common-space-config.component.css']
})
export class CommonSpaceConfigComponent {
  @Output() _ShowModal = new EventEmitter<boolean>()

  @Input() commonSpaceConfig:CommonSpaceConfig = new CommonSpaceConfig()

  commonSpacesList:CommonSpaceConfig[] = []

  ngOnInit(){

  }

  constructor(private consortiumServiceShared:ConsortiumConfigSharedService) { } 
  
  Save(){
    this.consortiumServiceShared.CommonSpaceList$.subscribe({
      next: data => {
        if(data != null) this.commonSpacesList = data
      }
    })

    if(this.commonSpacesList.find(cs => cs.idSpace == this.commonSpaceConfig.idSpace)){
      var index = this.commonSpacesList.findIndex(cs => cs.idSpace === this.commonSpaceConfig.idSpace);
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
  
}
