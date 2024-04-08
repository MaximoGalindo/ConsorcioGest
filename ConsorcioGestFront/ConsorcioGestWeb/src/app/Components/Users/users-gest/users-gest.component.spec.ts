import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersGestComponent } from './users-gest.component';

describe('UsersGestComponent', () => {
  let component: UsersGestComponent;
  let fixture: ComponentFixture<UsersGestComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UsersGestComponent]
    });
    fixture = TestBed.createComponent(UsersGestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
