import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  private http = inject(HttpClient);

  private apiUrl = `${environment.apiUrl}/vehicles`;

  getActiveVehicles(): Observable<any> {
    return this.http.get(`${this.apiUrl}/active`);
  }

  registerEntry(payload: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/entry`, payload);
  }

  registerExit(plate: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/exit/${plate}`, {});
  }

  getDashboardStats() {
    return this.http.get(
      `${this.apiUrl}/dashboard-stats`
    );
  }
}