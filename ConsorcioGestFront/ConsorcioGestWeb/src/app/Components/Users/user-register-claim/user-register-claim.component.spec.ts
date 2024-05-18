import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserRegisterClaimComponent } from './user-register-claim.component';

describe('UserRegisterClaimComponent', () => {
  let component: UserRegisterClaimComponent;
  let fixture: ComponentFixture<UserRegisterClaimComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserRegisterClaimComponent]
    });
    fixture = TestBed.createComponent(UserRegisterClaimComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
