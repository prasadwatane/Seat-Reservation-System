import { Component, Input, OnInit } from '@angular/core';

import { SeatService } from '../seats/seat.service';


@Component({
  selector: 'app-multiple',
  templateUrl: './multiple.component.html',
  styleUrls: ['./multiple.component.css']
})
export class MultipleComponent implements OnInit {

  constructor(private service: SeatService) { }

  ngOnInit(): void {
    this.SeatId = this.seat.SeatId;
    this.SeatName = this.seat.SeatName;
    this.seatList=this.seat.SeatName.split(',');
  }
  @Input()
   seat: any;
  SeatId: any;
  SeatName: any;
  seatList:any=[];
 


  addSeat() {
    this.seatList=this.SeatName.split(',');
    console.warn(this.seatList);
    this.service.addmultiple(this.seatList).subscribe((result: any) => {
      if (result.statusCode == 201)
        alert('Added Successfully! ')
      else if(result.statusCode==200)
        alert('Some Seats Already Exists!Valid New Seats are Added!')
      
      else{
        alert("Seat/s Already Exists! Try Another Name")
      }
      
        
        
        
    });   
    };

  }




