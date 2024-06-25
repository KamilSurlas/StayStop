import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignManagerFormComponent } from './assign-manager-form.component';

describe('AssignManagerFormComponent', () => {
  let component: AssignManagerFormComponent;
  let fixture: ComponentFixture<AssignManagerFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssignManagerFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AssignManagerFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
