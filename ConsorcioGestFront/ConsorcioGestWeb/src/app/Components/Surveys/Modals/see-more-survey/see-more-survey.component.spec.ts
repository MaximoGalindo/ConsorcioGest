import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeeMoreSurveyComponent } from './see-more-survey.component';

describe('SeeMoreSurveyComponent', () => {
  let component: SeeMoreSurveyComponent;
  let fixture: ComponentFixture<SeeMoreSurveyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SeeMoreSurveyComponent]
    });
    fixture = TestBed.createComponent(SeeMoreSurveyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
