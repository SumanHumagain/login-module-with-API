import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AppService {
  //private apiUrl = 'https://appdemosuman.azurewebsites.net';
  private apiUrl = 'https://localhost:44376';
  constructor(private http: HttpClient) { }

   login(formData: any): Observable<any> {
    debugger;
    return this.http.post<any>(`${this.apiUrl}/api/auth/Validate`, formData);
  }

}
