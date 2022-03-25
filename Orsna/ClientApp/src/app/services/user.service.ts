import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from '../Models';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) { }
}
