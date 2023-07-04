import { Component, OnInit } from '@angular/core';
import { SeatService } from '../seat.service';

@Component({
  selector: 'app-get-all-seats',
  templateUrl: './get-all-seats.component.html',
  styleUrls: ['./get-all-seats.component.css']
})
export class GetAllSeatsComponent implements OnInit {

  constructor(private service:SeatService) { }

  seatList:any=[];
  addToList:any[]=[];

  
  ModalTitle:string='';
  ActivateAddSeat:boolean=false;
  ActivateAddseats:boolean=false;
  seat:any;
  page:number = 1;

  ngOnInit(): void {
    this.getAllList();
  }
 
  addToIdList(id:any){
      
    if(this.addToList.length == 0)
    {
      this.addToList.push(id);
      console.warn(this.addToList)     
    }
    else{
      var flag= 0;

      for(var i=0;i<this.addToList.length;i++){
        if(this.addToList[i] === id){
          flag=1;
          break;  
        }
        else{
          flag=0;
        }
      }

      if(flag===0)
      {
        this.addToList.push(id);
        console.warn(this.addToList);       
        return;
      }
      if(flag === 1){
        const index = this.addToList.indexOf(id);
        this.addToList.splice(index,1);
      }

    } 
  }

  getAllList(){
    this.service.getSeatList().subscribe((result)=> {
      this.seatList = result;
    })
  }

  addClick(){
    this.seat={
      SeatId:0,
      SeatName:''
    }
    this.ModalTitle="Add Seat";
    this.ActivateAddSeat=true;

  }
  addmultipleClick(){
    this.seat={
      SeatId:0,
      SeatName:''
    }
    this.ModalTitle="Add Seats";
    this.ActivateAddseats=true;

  }
  closeClick(){
    this.ActivateAddSeat=false;
    this.getAllList();

  }

  deleteClick(seatIdList:any){
  
    if(seatIdList.length === 0){
      //console.warn(seatIdList);
      alert('please select seats to delete');    
      
    }
    else{
      
      if(confirm('are you sure you want to delete?')){
        this.service.removeSeat(seatIdList).subscribe((result:any)=>{
          if (result.statusCode == 200){
            alert('Deleted Successfully! ')

            this.getAllList();
            window.location.reload();
           // console.warn(seatIdList.length);
            for(var i=0;i< seatIdList.length;i++){
              seatIdList.splice(i,  1);
            }
            
          }
        else{
          alert('Failed to delete!')

        }
          
          this.getAllList();
        })
      }
      var length = seatIdList.length;
      console.log(length);
      while(length > 0 ){
        seatIdList.pop();
        length--;

      }
      console.warn(seatIdList)
    }
    
  }

  status:boolean=false;
  CheckStatus(){
    this.status = true;
  }

  
  deleteAll(){
    console.log(this.seatList)
    for(var i=0;i< this.seatList.length;i++)
    {
      var seatIds = this.seatList.map((obj:any)=>obj.seatId);
         
    }
    for(var i=0;i<seatIds.length;i++){
      this.addToList.push(seatIds[i]);
    }
    
    console.warn(this.addToList);
    
   
  }
}


