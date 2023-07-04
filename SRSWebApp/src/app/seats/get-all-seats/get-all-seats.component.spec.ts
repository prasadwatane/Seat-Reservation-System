import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetAllSeatsComponent } from './get-all-seats.component';

describe('GetAllSeatsComponent', () => {
  let component: GetAllSeatsComponent;
  let fixture: ComponentFixture<GetAllSeatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GetAllSeatsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetAllSeatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
