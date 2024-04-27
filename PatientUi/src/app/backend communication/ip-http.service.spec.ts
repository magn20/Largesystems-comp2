import { TestBed } from '@angular/core/testing';

import { IpHttpService } from './ip-http.service';

describe('IpHttpService', () => {
  let service: IpHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IpHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
