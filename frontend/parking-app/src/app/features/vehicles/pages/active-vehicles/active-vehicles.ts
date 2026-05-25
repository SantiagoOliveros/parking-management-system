import {
  Component,
  OnInit,
  inject
} from '@angular/core';

import { CommonModule, DatePipe } from '@angular/common';

import { VehicleService } from '../../../../services/vehicle.service';

import { MatTableModule } from '@angular/material/table';

import { MatButtonModule } from '@angular/material/button';

import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { MatChipsModule } from '@angular/material/chips';

@Component({
  selector: 'app-active-vehicles',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatChipsModule,
    DatePipe
  ],
  templateUrl: './active-vehicles.html',
  styleUrls: ['./active-vehicles.scss']
})
export class ActiveVehicles implements OnInit {

  private vehicleService = inject(VehicleService);

  vehicles: any[] = [];

  loading = true;

  displayedColumns: string[] = [
    'plate',
    'vehicleType',
    'entryTime',
    'actions'
  ];

  ngOnInit(): void {
    this.loadVehicles();
  }

  loadVehicles(): void {

    this.loading = true;

    this.vehicleService
      .getActiveVehicles()
      .subscribe({
        next: (response: any) => {

          this.vehicles = response;

          this.loading = false;
        },

        error: (err) => {

          console.error(err);

          this.loading = false;
        }
      });
  }

  registerExit(plate: string): void {

    this.loading = true;

    this.vehicleService
      .registerExit(plate)
      .subscribe({
        next: () => {

          this.loadVehicles();
        },

        error: (err) => {

          console.error(err);

          this.loading = false;
        }
      });
  }
}