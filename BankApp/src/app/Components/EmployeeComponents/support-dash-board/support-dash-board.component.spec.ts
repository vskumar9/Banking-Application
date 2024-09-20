import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SupportDashBoardComponent } from './support-dash-board.component';

describe('SupportDashBoardComponent', () => {
  let component: SupportDashBoardComponent;
  let fixture: ComponentFixture<SupportDashBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SupportDashBoardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SupportDashBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
