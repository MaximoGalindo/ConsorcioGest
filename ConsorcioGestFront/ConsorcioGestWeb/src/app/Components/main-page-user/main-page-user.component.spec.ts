import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MainPageUserComponent } from './main-page-user.component';

describe('MainPageUserComponent', () => {
  let component: MainPageUserComponent;
  let fixture: ComponentFixture<MainPageUserComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MainPageUserComponent]
    });
    fixture = TestBed.createComponent(MainPageUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
