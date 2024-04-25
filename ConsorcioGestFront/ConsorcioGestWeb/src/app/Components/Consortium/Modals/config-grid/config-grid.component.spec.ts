import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigGridComponent } from './config-grid.component';

describe('ConfigGridComponent', () => {
  let component: ConfigGridComponent;
  let fixture: ComponentFixture<ConfigGridComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ConfigGridComponent]
    });
    fixture = TestBed.createComponent(ConfigGridComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
