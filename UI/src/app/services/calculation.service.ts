import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import {
  CalculationDto,
  CalculationResultDto,
} from '../models/calculation-dto.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CalculationService {
  private apiUrl = `${environment.rootUrl}api/calculations`;

  constructor(private http: HttpClient) {}

  calculate(obj: CalculationDto): Observable<CalculationResultDto> {
    return this.http.post<CalculationResultDto>(
      `${this.apiUrl}/CalculetinResult`,
      obj
    );
  }

  getOperations(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/GetOperations`);
  }
}
