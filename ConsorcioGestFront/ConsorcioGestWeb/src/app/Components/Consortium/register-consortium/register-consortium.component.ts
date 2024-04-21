

import { Component } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ConfigTowerModalComponent } from '../Modals/config-tower-modal/config-tower-modal.component';

@Component({
  selector: 'app-register-consortium',
  templateUrl: './register-consortium.component.html',
  styleUrls: ['./register-consortium.component.css']
})
export class RegisterConsortiumComponent {

  selectedTab: number = 2 ;
  form:FormGroup = new FormGroup({})

  constructor(
    private formBuilder:FormBuilder,
    private modalService: NgbModal
  ){

    this.form  = this.formBuilder.group({
      Name: ['', Validators.required],
      Location: ['', Validators.required],
      CUIT: ['', [Validators.required/*, Validators.minLength(11), Validators.maxLength(11)*/]],
      Towers: this.formBuilder.array([''])
    })

  }

  get Towers() {
    return this.form.get('Towers') as FormArray;
  }

  agregarItem() {
    this.Towers.push(this.formBuilder.control(''));
    console.log(this.form.value);
    
    console.log(this.Towers.value);
    
  }

  removerItem(index: number) {
    this.Towers.removeAt(index);
  }

  configureTower(item:object){
    console.log(item);
    this.modalService.open(ConfigTowerModalComponent);
  }




  Back(){
    this.selectedTab = this.selectedTab > 1   ? this.selectedTab - 1 : this.selectedTab
  }
  Next(){
    this.selectedTab = this.selectedTab < 5 ? this.selectedTab + 1 : this.selectedTab
  }
}