import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveysGestComponent } from './surveys-gest.component';

describe('SurveysGestComponent', () => {
  let component: SurveysGestComponent;
  let fixture: ComponentFixture<SurveysGestComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SurveysGestComponent]
    });
    fixture = TestBed.createComponent(SurveysGestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
