import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoansDetailsComponent } from './loans-details.component';

describe('LoansDetailsComponent', () => {
  let component: LoansDetailsComponent;
  let fixture: ComponentFixture<LoansDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoansDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoansDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
