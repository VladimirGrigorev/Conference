import { TestBed, inject } from '@angular/core/testing';

import { ConferencesService } from './conferences.service';

describe('ConferencesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConferencesService]
    });
  });

  it('should be created', inject([ConferencesService], (service: ConferencesService) => {
    expect(service).toBeTruthy();
  }));
});
