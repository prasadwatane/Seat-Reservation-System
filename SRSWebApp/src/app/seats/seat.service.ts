import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, observable } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class SeatService {

  readonly APIUrl = "https://localhost:7183/api/Seats"
  constructor(private service: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    })
  };

  getSeatList(): Observable<any[]> {
    return this.service.get<any>(this.APIUrl + '/seats');
  }

  add(data: any) {
    return this.service.post(this.APIUrl + '/seat', data);
  }

  removeSeat(ids: any) {
    return this.service.delete(this.APIUrl + '/ids', { headers: this.httpOptions.headers, body: JSON.stringify(ids[0]) });
  }
  addmultiple(data:any){
    return this.service.post(this.APIUrl + '/addseats',data) ;
  }
}
