import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-carrusel-images-modal',
  templateUrl: './carrusel-images-modal.component.html',
  styleUrls: ['./carrusel-images-modal.component.css']
})
export class CarruselImagesModalComponent {

  @Output() _ShowModal = new EventEmitter<boolean>();
  @Input() Images: any[] = [];


  CloseModal(){
    this._ShowModal.emit(false);
  }
}
