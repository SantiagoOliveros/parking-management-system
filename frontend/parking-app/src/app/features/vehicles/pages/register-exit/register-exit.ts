import { Component, inject } from '@angular/core';

import { CommonModule } from '@angular/common';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { VehicleService } from '../../../../services/vehicle.service';

import {
  MatDialog,
  MatDialogModule,
} from '@angular/material/dialog';

import {
  MatSnackBar,
  MatSnackBarModule
} from '@angular/material/snack-bar';

import { MatCardModule } from '@angular/material/card';

import { MatFormFieldModule } from '@angular/material/form-field';

import { MatInputModule } from '@angular/material/input';

import { MatButtonModule } from '@angular/material/button';

import { ExitSummaryDialogComponent }
from '../../../../shared/components/exit-summary-dialog/exit-summary-dialog';

@Component({
  selector: 'app-register-exit',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatSnackBarModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './register-exit.html',
  styleUrls: ['./register-exit.scss']
})
export class RegisterExit {

  private fb = inject(FormBuilder);

  private vehicleService = inject(VehicleService);

  private dialog = inject(MatDialog);

  private snackBar = inject(MatSnackBar);

  loading = false;

  form = this.fb.group({
    plate: ['', [
      Validators.required,
      Validators.minLength(5)
    ]]
  });

  submit() {

    if (this.form.invalid) return;

    this.loading = true;

    const plate = this.form.value.plate!;

    this.vehicleService
      .registerExit(plate)
      .subscribe({

        next: (response) => {

          this.dialog.open(
            ExitSummaryDialogComponent,
            {
              data: response,
              width: '450px'
            }
          );

          this.snackBar.open(
            'Vehicle exit registered successfully',
            'Close',
            {
              duration: 3000
            }
          );

          this.loading = false;

          this.form.reset();
        },

        error: (err) => {

          this.snackBar.open(
            err?.error?.message ||
            'Vehicle exit failed',
            'Close',
            {
              duration: 4000
            }
          );

          this.loading = false;
        }
      });
  }
}