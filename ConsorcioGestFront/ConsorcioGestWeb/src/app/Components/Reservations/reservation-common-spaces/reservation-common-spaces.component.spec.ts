import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationCommonSpacesComponent } from './reservation-common-spaces.component';

describe('ReservationCommonSpacesComponent', () => {
  let component: ReservationCommonSpacesComponent;
  let fixture: ComponentFixture<ReservationCommonSpacesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReservationCommonSpacesComponent]
    });
    fixture = TestBed.createComponent(ReservationCommonSpacesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
