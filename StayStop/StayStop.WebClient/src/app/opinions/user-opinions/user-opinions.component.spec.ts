import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserOpinionsComponent } from './user-opinions.component';

describe('UserOpinionsComponent', () => {
  let component: UserOpinionsComponent;
  let fixture: ComponentFixture<UserOpinionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UserOpinionsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UserOpinionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
