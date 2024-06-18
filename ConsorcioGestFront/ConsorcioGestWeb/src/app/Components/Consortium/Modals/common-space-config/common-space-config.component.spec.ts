import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommonSpaceConfigComponent } from './common-space-config.component';

describe('CommonSpaceConfigComponent', () => {
  let component: CommonSpaceConfigComponent;
  let fixture: ComponentFixture<CommonSpaceConfigComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CommonSpaceConfigComponent]
    });
    fixture = TestBed.createComponent(CommonSpaceConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
