import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SaveConsortiumComponent } from './save-consortium.component';

describe('SaveConsortiumComponent', () => {
  let component: SaveConsortiumComponent;
  let fixture: ComponentFixture<SaveConsortiumComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SaveConsortiumComponent]
    });
    fixture = TestBed.createComponent(SaveConsortiumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
