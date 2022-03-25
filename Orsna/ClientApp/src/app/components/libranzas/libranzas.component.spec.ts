import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LibranzasComponent } from './libranzas.component';

describe('LibranzasComponent', () => {
  let component: LibranzasComponent;
  let fixture: ComponentFixture<LibranzasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LibranzasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LibranzasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
