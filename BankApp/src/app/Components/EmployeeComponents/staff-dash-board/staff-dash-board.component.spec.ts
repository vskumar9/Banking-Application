import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffDashBoardComponent } from './staff-dash-board.component';

describe('StaffDashBoardComponent', () => {
  let component: StaffDashBoardComponent;
  let fixture: ComponentFixture<StaffDashBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StaffDashBoardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StaffDashBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
