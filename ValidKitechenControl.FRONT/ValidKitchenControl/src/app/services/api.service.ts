import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from '../environments/environment';
import { HttpClient, HttpErrorResponse, HttpHeaders, provideHttpClient } from '@angular/common/http';
import { bootstrapApplication } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})

export class ApiService {

  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // Método para fazer uma requisição GET com headers
  getOrders(): Observable<any> {
    const headers = this.getHeaders();
    return this.http.get(`${this.baseUrl}/Orders`, { headers })
    .pipe(
      catchError(this.handleError) // Adiciona tratamento de erro
    );;
  }

  // Método para fazer uma requisição POST com headers
  addOrder(order: any): Observable<any> {
    const headers = this.getHeaders();
    return this.http.post(`${this.baseUrl}/Orders`, order, { headers });
  }

  // Configura os headers padrão
  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Accept': '*/*'
    });
  }

  
  private handleError(error: HttpErrorResponse) {
    console.error('Houve um erro:', error.message);
    return throwError(() => new Error('Algo deu errado; por favor tente novamente mais tarde.'));
  }
}