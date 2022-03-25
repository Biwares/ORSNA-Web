import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaBeneficariosComponent } from './consulta-beneficiarios.component';

describe('ConsultaBeneficiariosComponent', () => {
  let component: ConsultaBeneficariosComponent;
  let fixture: ComponentFixture<ConsultaBeneficariosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConsultaBeneficariosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsultaBeneficariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
