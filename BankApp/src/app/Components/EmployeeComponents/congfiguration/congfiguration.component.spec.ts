import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CongfigurationComponent } from './congfiguration.component';

describe('CongfigurationComponent', () => {
  let component: CongfigurationComponent;
  let fixture: ComponentFixture<CongfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CongfigurationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CongfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
