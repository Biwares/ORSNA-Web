import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaRolModuloComponent } from './consulta-rolmodulo.component';

describe('ConsultaRolModuloComponent', () => {
  let component: ConsultaRolModuloComponent;
  let fixture: ComponentFixture<ConsultaRolModuloComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ConsultaRolModuloComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsultaRolModuloComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
