import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private vieneDesdeLaGrillaDeProyectos = new BehaviorSubject(false);
  private messageSource = new BehaviorSubject('0');
  private messageSource2 = new BehaviorSubject('0');
  currentMessage = this.messageSource.asObservable();
  currentMessage2 = this.messageSource2.asObservable();
  currentMessage3 = this.vieneDesdeLaGrillaDeProyectos.asObservable();

  constructor() { }

  changeMessage(message: string) {
    this.messageSource.next(message)
    this.vieneDesdeLaGrillaDeProyectos.next(false);
  }

  create(message: string) {
    this.messageSource2.next(message);
    this.vieneDesdeLaGrillaDeProyectos.next(true);
  }
}
