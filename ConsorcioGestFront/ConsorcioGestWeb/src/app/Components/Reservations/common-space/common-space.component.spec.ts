import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommonSpaceComponent } from './common-space.component';

describe('CommonSpaceComponent', () => {
  let component: CommonSpaceComponent;
  let fixture: ComponentFixture<CommonSpaceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CommonSpaceComponent]
    });
    fixture = TestBed.createComponent(CommonSpaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
