import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewsurveyComponent } from './newsurvey.component';

describe('NewsurveyComponent', () => {
  let component: NewsurveyComponent;
  let fixture: ComponentFixture<NewsurveyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewsurveyComponent]
    });
    fixture = TestBed.createComponent(NewsurveyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
