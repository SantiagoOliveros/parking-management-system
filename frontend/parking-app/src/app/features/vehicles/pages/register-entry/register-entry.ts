import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { VehicleService } from '../../../../services/vehicle.service';

@Component({
  selector: 'app-register-entry',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register-entry.html'
})
export class RegisterEntry {

  private fb = inject(FormBuilder);
  private vehicleService = inject(VehicleService);

  loading = false;
  successMessage = '';
  errorMessage = '';

  form = this.fb.group({
    plate: ['', [Validators.required, Validators.minLength(5)]],
    vehicleType: [1, [Validators.required]]
  });

  submit() {
    if (this.form.invalid) return;

    this.loading = true;
    this.successMessage = '';
    this.errorMessage = '';

    this.vehicleService.registerEntry(this.form.value)
      .subscribe({
        next: () => {
          this.successMessage = 'Vehicle registered successfully';
          this.form.reset({ vehicleType: 1 });
          this.loading = false;
        },
        error: (err) => {
          this.errorMessage = err?.error?.message || 'Error registering vehicle';
          this.loading = false;
        }
      });
  }
}