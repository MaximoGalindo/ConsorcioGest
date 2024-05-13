import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClaimsInfoComponent } from './claims-info.component';

describe('ClaimsInfoComponent', () => {
  let component: ClaimsInfoComponent;
  let fixture: ComponentFixture<ClaimsInfoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ClaimsInfoComponent]
    });
    fixture = TestBed.createComponent(ClaimsInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
