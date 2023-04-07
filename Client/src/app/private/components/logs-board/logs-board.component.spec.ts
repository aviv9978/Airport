import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LogsBoardComponent } from './logs-board.component';

describe('LogsBoardComponent', () => {
  let component: LogsBoardComponent;
  let fixture: ComponentFixture<LogsBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LogsBoardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LogsBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
