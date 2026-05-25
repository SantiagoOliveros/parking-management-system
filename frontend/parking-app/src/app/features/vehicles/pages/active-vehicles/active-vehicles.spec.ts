import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ActiveVehicles } from './active-vehicles';

describe('ActiveVehicles', () => {
  let component: ActiveVehicles;
  let fixture: ComponentFixture<ActiveVehicles>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ActiveVehicles],
    }).compileComponents();

    fixture = TestBed.createComponent(ActiveVehicles);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
