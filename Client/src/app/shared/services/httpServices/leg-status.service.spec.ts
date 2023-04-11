import { TestBed } from '@angular/core/testing';

import { LegStatusService } from './leg-status.service';

describe('LegStatusService', () => {
  let service: LegStatusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LegStatusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
