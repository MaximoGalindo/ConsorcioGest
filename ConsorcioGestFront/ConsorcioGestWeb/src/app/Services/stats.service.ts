import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../Helpers/Envriorment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StatsService {

  baseUrl = `${environment.API_URL}/stats/`;
  constructor(private http: HttpClient) { }

  GetMostFrequentComplaintsByCauseOfComplaint(dateFrom: string | null, dateTo: string | null): Observable<any> {
    let params: string[] = [];

    if (dateFrom) {
      params.push(`dateFrom=${dateFrom}`);
    }

    if (dateTo) {
      params.push(`dateTo=${dateTo}`);
    }

    const queryString = params.length > 0 ? `?${params.join('&')}` : '';

    return this.http.get<any>(`${this.baseUrl}get-most-frequent-coplaints-by-cause-complaint${queryString}`);
  }

  GetNumberOfClaimsPerMonths(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}get-number-of-claims-per-months`);
  }

  GetNumberOfGestionClaimsPerMonth(month:number,year:number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}get-number-of-gestion-claims-per-month/${month},${year}`);
  }

}
