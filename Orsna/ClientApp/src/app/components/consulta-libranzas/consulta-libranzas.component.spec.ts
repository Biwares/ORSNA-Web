import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultaLibranzasComponent } from './consulta-libranzas.component';

describe('ConsultaLibranzasComponent', () => {
  let component: ConsultaLibranzasComponent;
  let fixture: ComponentFixture<ConsultaLibranzasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConsultaLibranzasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConsultaLibranzasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
