import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CdbService, CdbCalculationRequest } from './cdb.service';

describe('CdbService', () => {
  let service: CdbService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CdbService]
    });
    service = TestBed.inject(CdbService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should calculate the CDB successfully', () => {
    const mockRequest: CdbCalculationRequest = { initialValue: 1000, months: 12 };
    const mockResponse = { grossValue: 1100, netValue: 1080 };

    service.calculate(mockRequest).subscribe(result => {
      expect(result).toEqual(mockResponse);
    });

    const req = httpMock.expectOne('http://localhost:5000/api/cdb/calculate');
    expect(req.request.method).toBe('POST');
    req.flush(mockResponse);
  });
});
