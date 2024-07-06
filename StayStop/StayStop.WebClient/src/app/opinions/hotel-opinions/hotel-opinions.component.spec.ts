import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HotelOpinionsComponent } from './hotel-opinions.component';

describe('HotelOpinionsComponent', () => {
  let component: HotelOpinionsComponent;
  let fixture: ComponentFixture<HotelOpinionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HotelOpinionsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HotelOpinionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
