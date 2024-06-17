import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NumberClaimsChartComponent } from './number-claims-chart.component';

describe('NumberClaimsChartComponent', () => {
  let component: NumberClaimsChartComponent;
  let fixture: ComponentFixture<NumberClaimsChartComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NumberClaimsChartComponent]
    });
    fixture = TestBed.createComponent(NumberClaimsChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
