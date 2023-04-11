import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LegsStatusComponent } from './legs-status.component';

describe('LegsStatusComponent', () => {
  let component: LegsStatusComponent;
  let fixture: ComponentFixture<LegsStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LegsStatusComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LegsStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
