import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigTowerModalComponent } from './config-tower-modal.component';

describe('ConfigTowerModalComponent', () => {
  let component: ConfigTowerModalComponent;
  let fixture: ComponentFixture<ConfigTowerModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ConfigTowerModalComponent]
    });
    fixture = TestBed.createComponent(ConfigTowerModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
