import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterEntry } from './register-entry';

describe('RegisterEntry', () => {
  let component: RegisterEntry;
  let fixture: ComponentFixture<RegisterEntry>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterEntry],
    }).compileComponents();

    fixture = TestBed.createComponent(RegisterEntry);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
