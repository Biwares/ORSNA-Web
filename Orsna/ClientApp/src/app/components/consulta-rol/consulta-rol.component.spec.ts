import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaRolComponent } from './consulta-rol.component';

describe('ConsultaRolComponent', () => {
  let component: ConsultaRolComponent;
  let fixture: ComponentFixture<ConsultaRolComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConsultaRolComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsultaRolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
