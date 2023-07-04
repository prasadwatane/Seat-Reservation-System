import { Component, OnInit, Input } from '@angular/core';
import { ReservationService } from './reservation.service';



@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css']

})
export class ReservationComponent implements OnInit {
  

  constructor(private service:ReservationService) {}

  reservationList:any=[];
  ActivateAddReservation:boolean=false;
  reservation:any;
  page:number = 1;

  ngOnInit(): void {
    this.BookingDate =new Date().toISOString().slice(0, 10)

    this.getReservationByDate(this.BookingDate);
    this.getList()

  }

  @Input() BookingDate: string = new Date().toLocaleDateString().slice(0, 10)

  getList(){
    this.getReservationByDate(this.BookingDate);
  }

  getReservationByDate(date:any){
    this.service.getByDate(date).subscribe((result: any) => {
      this.reservationList=result;
      

    })
  }
  
  addReservation(){
    this.reservation={
      ReservationId:0,
      EmployeeId:0,
      EmployeeName:'',
      SeatId:0,
      SeatName:'',
      BookingDate:''
    }
    this.ActivateAddReservation=true;

  }

  closeClick(){
    this.ActivateAddReservation=false;
  }

  deleteReservation(reservationId:any){
    if(confirm('are you sure?')){
      this.service.delete(reservationId).subscribe((result:any)=>{
        if (result.statusCode == 200)
        alert('Deleted Successfully! ')
      else
        alert('Failed!')
         this.getList();
      });
    }
  }
}
