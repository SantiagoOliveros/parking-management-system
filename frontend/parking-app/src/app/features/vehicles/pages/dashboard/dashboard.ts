import {
  Component,
  OnInit,
  inject,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';

import { MatCardModule } from '@angular/material/card';

import { MatIconModule } from '@angular/material/icon';

import { VehicleService } from '../../../../services/vehicle.service';


@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatIconModule
  ],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})
export class Dashboard implements OnInit {

  private vehicleService = inject(VehicleService);

  private cdr = inject(ChangeDetectorRef);


  stats: any = {
    totalActiveVehicles: 0,
    totalCars: 0,
    totalMotorcycles: 0,
    estimatedRevenue: 0
  };

  ngOnInit(): void {

    this.loadStats();
  }

  loadStats(): void {

    this.vehicleService
      .getDashboardStats()
      .subscribe({

        next: (response: any) => {

          this.stats = {
            ...response
          };

          this.cdr.detectChanges();
        },

        error: (err) => {

          console.error(err);
        }
      });
  }
}