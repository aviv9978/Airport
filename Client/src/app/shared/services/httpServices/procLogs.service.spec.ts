import { TestBed } from '@angular/core/testing';

import { ProcLogsService } from './procLogs.service';

describe('LogsService', () => {
  let service: ProcLogsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProcLogsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
