import { Component, OnInit } from '@angular/core';
import { SeatService } from './seat.service';

@Component({
  selector: 'app-seats',
  templateUrl: './seats.component.html',
  styleUrls: ['./seats.component.css']
})
export class SeatsComponent implements OnInit {

  constructor(private service:SeatService) { }

  ngOnInit(): void {
  }

  addSeat(data:any){
    this.service.add(data).subscribe((result)=>{
      console.warn(result);
    })
  }

}
