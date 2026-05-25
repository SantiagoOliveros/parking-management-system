import {
  Component,
  OnInit,
  inject,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';

import { VehicleService } from '../../../../services/vehicle.service';

import { MatTableModule } from '@angular/material/table';

import { MatButtonModule } from '@angular/material/button';

import { MatChipsModule } from '@angular/material/chips';

import {
  MatDialog,
  MatDialogModule
} from '@angular/material/dialog';

import { ConfirmDialogComponent }
from '../../../../shared/components/confirm-dialog/confirm-dialog';

import {
  MatSnackBar,
  MatSnackBarModule
} from '@angular/material/snack-bar';

import { finalize } from 'rxjs';

@Component({
  selector: 'app-active-vehicles',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatChipsModule,
    MatDialogModule,
    MatSnackBarModule
  ],
  templateUrl: './active-vehicles.html',
  styleUrls: ['./active-vehicles.scss']
})
export class ActiveVehicles implements OnInit {

  private vehicleService = inject(VehicleService);

  private dialog = inject(MatDialog);

  private snackBar = inject(MatSnackBar);

  private cdr = inject(ChangeDetectorRef);

  vehicles: any[] = [];

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

    this.vehicleService
      .getActiveVehicles()
      .subscribe({

        next: (response: any) => {

          this.vehicles = [...(response || [])];

          this.cdr.detectChanges();
        },

        error: (err) => {

          console.error(err);

          this.snackBar.open(
            'Failed to load vehicles',
            'Close',
            {
              duration: 4000
            }
          );
        }
      });
  }

  registerExit(plate: string): void {

    const dialogRef = this.dialog.open(
      ConfirmDialogComponent,
      {
        width: '400px',
        data: { plate }
      }
    );

    dialogRef.afterClosed()
      .subscribe(result => {

        if (!result) return;

        this.vehicleService
          .registerExit(plate)
          .subscribe({

            next: () => {

              this.snackBar.open(
                'Vehicle exit registered successfully',
                'Close',
                {
                  duration: 3000
                }
              );

              this.loadVehicles();
            },

            error: (err) => {

              console.error(err);


              this.snackBar.open(
                err?.error?.message ||
                'Vehicle exit failed',
                'Close',
                {
                  duration: 4000
                }
              );
            }
          });
      });
  }
}