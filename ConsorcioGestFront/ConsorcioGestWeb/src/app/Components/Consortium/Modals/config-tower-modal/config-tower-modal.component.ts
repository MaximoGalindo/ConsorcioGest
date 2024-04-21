import { Component } from '@angular/core';

@Component({
  selector: 'app-config-tower-modal',
  templateUrl: './config-tower-modal.component.html',
  styleUrls: ['./config-tower-modal.component.css']
})
export class ConfigTowerModalComponent {

  _TowerIsPresent: boolean = true;

  closeModal(){
    this._TowerIsPresent = false
  }
}
