import { TestBed } from '@angular/core/testing';

import { Data.ServiceService } from './data.service.service';

describe('Data.ServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: Data.ServiceService = TestBed.get(Data.ServiceService);
    expect(service).toBeTruthy();
  });
});
