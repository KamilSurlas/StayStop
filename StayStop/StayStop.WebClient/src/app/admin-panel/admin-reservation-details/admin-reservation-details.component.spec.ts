import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminReservationDetailsComponent } from './admin-reservation-details.component';

describe('AdminReservationDetailsComponent', () => {
  let component: AdminReservationDetailsComponent;
  let fixture: ComponentFixture<AdminReservationDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminReservationDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AdminReservationDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
