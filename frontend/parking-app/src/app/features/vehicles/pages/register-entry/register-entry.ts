import { Component, inject } from '@angular/core';

import { CommonModule } from '@angular/common';

import {
  ReactiveFormsModule,
  FormBuilder,
  Validators
} from '@angular/forms';

import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { VehicleService } from '../../../../services/vehicle.service';

@Component({
  selector: 'app-register-entry',

  standalone: true,

  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatSnackBarModule
  ],

  templateUrl: './register-entry.html',

  styleUrl: './register-entry.scss'
})
export class RegisterEntry {

  private fb = inject(FormBuilder);

  private vehicleService = inject(VehicleService);

  private snackBar = inject(MatSnackBar);

  loading = false;

  form = this.fb.group({
    plate: [
      '',
      [
        Validators.required,
        Validators.minLength(5)
      ]
    ],

    vehicleType: [
      1,
      [Validators.required]
    ]
  });

  submit(): void {

    if (this.form.invalid) {

      this.form.markAllAsTouched();

      return;
    }

    this.loading = true;

    this.vehicleService
      .registerEntry(this.form.value)
      .subscribe({

        next: () => {

          this.snackBar.open(
            'Vehicle registered successfully',
            'Close',
            {
              duration: 3000
            }
          );

          this.form.reset({
            vehicleType: 1
          });

          this.loading = false;
        },

        error: (err) => {

          this.snackBar.open(
            err?.error?.message ||
            'Error registering vehicle',
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