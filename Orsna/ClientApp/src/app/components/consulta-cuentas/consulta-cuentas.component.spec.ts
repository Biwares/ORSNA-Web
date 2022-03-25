import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaCuentasComponent } from './consulta-cuentas.component';

describe('ConsultaCuentasComponent', () => {
  let component: ConsultaCuentasComponent;
  let fixture: ComponentFixture<ConsultaCuentasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConsultaCuentasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsultaCuentasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
