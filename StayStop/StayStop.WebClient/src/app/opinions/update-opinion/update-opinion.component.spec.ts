import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateOpinionComponent } from './update-opinion.component';

describe('UpdateOpinionComponent', () => {
  let component: UpdateOpinionComponent;
  let fixture: ComponentFixture<UpdateOpinionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdateOpinionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpdateOpinionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
