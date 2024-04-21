import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectConsortiumComponent } from './select-consortium.component';

describe('SelectConsortiumComponent', () => {
  let component: SelectConsortiumComponent;
  let fixture: ComponentFixture<SelectConsortiumComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SelectConsortiumComponent]
    });
    fixture = TestBed.createComponent(SelectConsortiumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
