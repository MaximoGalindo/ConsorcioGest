import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeeMoreClaimComponent } from './see-more-claim.component';

describe('SeeMoreClaimComponent', () => {
  let component: SeeMoreClaimComponent;
  let fixture: ComponentFixture<SeeMoreClaimComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SeeMoreClaimComponent]
    });
    fixture = TestBed.createComponent(SeeMoreClaimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
