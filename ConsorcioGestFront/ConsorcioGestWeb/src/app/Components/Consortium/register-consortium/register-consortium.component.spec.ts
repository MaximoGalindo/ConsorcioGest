import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterConsortiumComponent } from './register-consortium.component';

describe('RegisterConsortiumComponent', () => {
  let component: RegisterConsortiumComponent;
  let fixture: ComponentFixture<RegisterConsortiumComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegisterConsortiumComponent]
    });
    fixture = TestBed.createComponent(RegisterConsortiumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
