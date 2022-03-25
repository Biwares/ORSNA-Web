import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LibranzaViewComponent } from './libranza-view.component';

describe('LibranzaViewComponent', () => {
  let component: LibranzaViewComponent;
  let fixture: ComponentFixture<LibranzaViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LibranzaViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LibranzaViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
