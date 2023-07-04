import { Component, OnInit, Input } from '@angular/core';
import { SeatService } from '../seat.service';


@Component({
  selector: 'app-add-seat',
  templateUrl: './add-seat.component.html',
  styleUrls: ['./add-seat.component.css']
})


export class AddSeatComponent implements OnInit {


  constructor(private service: SeatService) { }

  ngOnInit(): void {
    this.SeatId = this.seat.SeatId;
    this.SeatName = this.seat.SeatName;

  }
  @Input() seat: any;
  SeatId: any;
  SeatName: any;


  addSeat() {
    var seat = {
      SeatId: this.SeatId,
      SeatName: this.SeatName
    };
    this.service.add(seat).subscribe((result: any) => {
      if (result.statusCode == 201)
        alert('Added Successfully! ')
      else
        alert('Seat already exists!')
        
        
    });

  }




}
