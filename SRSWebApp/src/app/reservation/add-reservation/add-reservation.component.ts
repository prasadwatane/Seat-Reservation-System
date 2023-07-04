import { Component, OnInit, Input } from '@angular/core';
import { SeatService } from 'src/app/seats/seat.service';
import { ReservationService } from '../reservation.service';

@Component({
  selector: 'app-add-reservation',
  templateUrl: './add-reservation.component.html',
  styleUrls: ['./add-reservation.component.css']
})
export class AddReservationComponent implements OnInit {

  constructor(private service:ReservationService, private seatService: SeatService) { }

  ngOnInit(): void {
    this.ReservationId=this.reservation.ReservationId,
    this.EmployeeId=this.reservation.EmployeeId,
    this.SeatId=this.reservation.SeatId,

    this.getUnreservedList(this.BookingDate);

    this.BookingDate =new Date().toISOString().slice(0, 10);
  
  }

  unreservedList:any=[];
  // getUnreservedList(){
  //   this.unreservedList=this.seatService.getSeatList().subscribe(result=>{
  //     this.unreservedList=result;
  //   })

    getUnreservedList(date:any){
      this.unreservedList=this.service.getUnreservedSeats(date).subscribe(result=>{
        this.unreservedList=result;
      })
  }
  today: string = new Date().toLocaleDateString().slice(0, 10)

  @Input() reservation:any;
  ReservationId:any;
  EmployeeId:any;
  EmployeeName:any;
  SeatId:any;
  SeatName:any;
  BookingDate:any= new Date().toLocaleDateString().slice(0, 10);

  addReservation(){
    var data={
      ReservationId:this.ReservationId,
      EmployeeId:this.EmployeeId,
      EmployeeName:this.EmployeeName,
      SeatId:this.SeatId,
      SeatName:this.SeatName,
      BookingDate:this.BookingDate

    };   
    console.log(this.BookingDate);
    this.service.add(data).subscribe((result : any)=>{
        if(result.statusCode == 201){
          alert("Added Successfully!");
          window.location.reload();
        }       
        else
          alert('Try Another');  
      });

  }

}


