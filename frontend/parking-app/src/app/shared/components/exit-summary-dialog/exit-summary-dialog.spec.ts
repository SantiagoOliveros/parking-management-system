import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExitSummaryDialog } from './exit-summary-dialog';

describe('ExitSummaryDialog', () => {
  let component: ExitSummaryDialog;
  let fixture: ComponentFixture<ExitSummaryDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ExitSummaryDialog],
    }).compileComponents();

    fixture = TestBed.createComponent(ExitSummaryDialog);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
