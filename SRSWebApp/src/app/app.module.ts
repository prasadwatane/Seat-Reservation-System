import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SeatsComponent } from './seats/seats.component';
import { AddSeatComponent } from './seats/add-seat/add-seat.component';
import { GetAllSeatsComponent } from './seats/get-all-seats/get-all-seats.component';
import { SeatService } from './seats/seat.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ReservationComponent } from './reservation/reservation.component';
import { ReservationService } from './reservation/reservation.service';
import { AddReservationComponent } from './reservation/add-reservation/add-reservation.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox'
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatFormFieldModule } from '@angular/material/form-field';
import {MatNativeDateModule} from '@angular/material/core';
import {MatInputModule} from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {MatIconModule} from '@angular/material/icon';
import {NgxPaginationModule} from 'ngx-pagination';
import { MultipleComponent } from './multiple/multiple.component'; 

@NgModule({
  declarations: [
    AppComponent,
    SeatsComponent,
    AddSeatComponent,
    GetAllSeatsComponent,
    ReservationComponent,
    AddReservationComponent,
    MultipleComponent

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
    ReactiveFormsModule,
    NgxPaginationModule

    
  ],
  providers: [SeatService, ReservationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
