import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowConfigTowerComponent } from './show-config-tower.component';

describe('ShowConfigTowerComponent', () => {
  let component: ShowConfigTowerComponent;
  let fixture: ComponentFixture<ShowConfigTowerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ShowConfigTowerComponent]
    });
    fixture = TestBed.createComponent(ShowConfigTowerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
