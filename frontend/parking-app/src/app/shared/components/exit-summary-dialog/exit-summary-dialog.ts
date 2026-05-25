import { Component, Inject } from '@angular/core';

import { MAT_DIALOG_DATA } from '@angular/material/dialog';

import { CommonModule, DatePipe } from '@angular/common';

import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-exit-summary-dialog',
  standalone: true,
  imports: [
    CommonModule,
    DatePipe,
    MatButtonModule
  ],
  templateUrl: './exit-summary-dialog.html',
  styleUrls: ['./exit-summary-dialog.scss']
})
export class ExitSummaryDialogComponent {

  constructor(
    @Inject(MAT_DIALOG_DATA)
    public data: any
  ) {}

}