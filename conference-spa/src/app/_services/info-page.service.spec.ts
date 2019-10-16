import { TestBed, inject } from '@angular/core/testing';

import { InfoPageService } from './info-page.service';

describe('InfoPageService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [InfoPageService]
    });
  });

  it('should be created', inject([InfoPageService], (service: InfoPageService) => {
    expect(service).toBeTruthy();
  }));
});
