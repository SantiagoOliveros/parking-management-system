import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterExit } from './register-exit';

describe('RegisterExit', () => {
  let component: RegisterExit;
  let fixture: ComponentFixture<RegisterExit>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterExit],
    }).compileComponents();

    fixture = TestBed.createComponent(RegisterExit);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
