import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationsGestComponent } from './reservations-gest.component';

describe('ReservationsGestComponent', () => {
  let component: ReservationsGestComponent;
  let fixture: ComponentFixture<ReservationsGestComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReservationsGestComponent]
    });
    fixture = TestBed.createComponent(ReservationsGestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
