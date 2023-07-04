import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, observable} from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  readonly APIUrl="https://localhost:7183/api/Reservations";

  constructor(private http:HttpClient) { }

  add(reservation:any){
    return this.http.post(this.APIUrl+'/reservation',reservation);
  }

  getByDate(date:any):Observable<any[]> {
    return this.http.get<any>(this.APIUrl+'/Date?date='+date);
  }

  delete(id:any){
    return this.http.delete(this.APIUrl+'/reservationId?reservationId='+id);
  }

  // getUnreservedSeats(){
  //   return this.http.get<any>('https://localhost:7183/api/Seats/get');
  // }

  getUnreservedSeats(date:any){
    return this.http.get<any>('https://localhost:7183/api/Reservations/GetSeatsByDate?date='+date);
  }


}
