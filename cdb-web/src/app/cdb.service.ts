import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface CdbCalculationRequest {
  initialValue: number;
  months: number;
}

export interface CdbCalculationResult {
  grossValue: number;
  netValue: number;
}

@Injectable({
  providedIn: 'root'
})
export class CdbService {
  private readonly apiUrl = 'https://localhost:7058/api/cdb/calculate';  

  constructor(private http: HttpClient) {}

  calculate(request: CdbCalculationRequest): Observable<CdbCalculationResult> {
    return this.http.post<CdbCalculationResult>(this.apiUrl, request);
  }
}
