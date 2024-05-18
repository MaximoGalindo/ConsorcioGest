import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarruselImagesModalComponent } from './carrusel-images-modal.component';

describe('CarruselImagesModalComponent', () => {
  let component: CarruselImagesModalComponent;
  let fixture: ComponentFixture<CarruselImagesModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CarruselImagesModalComponent]
    });
    fixture = TestBed.createComponent(CarruselImagesModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
