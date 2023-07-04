import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HttpClient} from '@angular/common/http';
import { AddSeatComponent } from './seats/add-seat/add-seat.component';
import { GetAllSeatsComponent } from './seats/get-all-seats/get-all-seats.component';
import { SeatsComponent } from './seats/seats.component';
import { ReservationComponent } from './reservation/reservation.component';


const routes: Routes = [
  {
    component: SeatsComponent,
    path:'home'
  },
  {
    component: AddSeatComponent,
    path:'add'
  },
  {
    component: GetAllSeatsComponent,
    path:'seats'
  },
  {
    component:ReservationComponent,
    path:'reservations'
  },
  {
    component:ReservationComponent,
    path:''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
