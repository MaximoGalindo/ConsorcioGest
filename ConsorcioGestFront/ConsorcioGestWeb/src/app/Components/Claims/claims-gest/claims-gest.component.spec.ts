import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClaimsGestComponent } from './claims-gest.component';

describe('ClaimsGestComponent', () => {
  let component: ClaimsGestComponent;
  let fixture: ComponentFixture<ClaimsGestComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClaimsGestComponent]
    });
    fixture = TestBed.createComponent(ClaimsGestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
