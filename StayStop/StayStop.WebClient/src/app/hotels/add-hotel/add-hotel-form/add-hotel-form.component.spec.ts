import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddHotelFormComponent } from './add-hotel-form.component';

describe('AddHotelFormComponent', () => {
  let component: AddHotelFormComponent;
  let fixture: ComponentFixture<AddHotelFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddHotelFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AddHotelFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
