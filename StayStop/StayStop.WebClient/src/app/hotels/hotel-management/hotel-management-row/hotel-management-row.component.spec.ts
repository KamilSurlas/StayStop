import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HotelManagementRowComponent } from './hotel-management-row.component';

describe('HotelManagementRowComponent', () => {
  let component: HotelManagementRowComponent;
  let fixture: ComponentFixture<HotelManagementRowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HotelManagementRowComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HotelManagementRowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
