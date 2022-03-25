async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RolModuloComponent } from './rolmodulo.component';

describe('RolModuloComponent', () => {
  let component: RolModuloComponent;
  let fixture: ComponentFixture<RolModuloComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [RolModuloComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RolModuloComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
