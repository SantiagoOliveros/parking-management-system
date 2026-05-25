import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { VehicleService } from '../../../../services/vehicle.service';

@Component({
  selector: 'app-register-exit',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './register-exit.html',
  styleUrls: ['./register-exit.scss']
})
export class RegisterExit {

  private fb = inject(FormBuilder);

  private vehicleService = inject(VehicleService);

  loading = false;

  errorMessage = '';

  exitResult: any = null;

  form = this.fb.group({
    plate: ['', [
      Validators.required,
      Validators.minLength(5)
    ]]
  });

  submit() {

    if (this.form.invalid) return;

    this.loading = true;

    this.errorMessage = '';

    this.exitResult = null;

    const plate = this.form.value.plate!;

    this.vehicleService.registerExit(plate)
      .subscribe({
        next: (response) => {

          this.exitResult = response;

          this.loading = false;

          this.form.reset();
        },

        error: (err) => {

          this.errorMessage =
            err?.error?.message ||
            'Vehicle exit failed';

          this.loading = false;
        }
      });
  }
}