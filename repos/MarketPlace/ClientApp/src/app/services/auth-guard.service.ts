import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuardService {

  constructor(protected auth: AuthService) { }

  canActivate() {
    if (this.auth.authenticated())
      return true;
    window.location.href = 'https://bijshakya77.auth0.com/login?client=8PTWwjXAICLKLX2Sk1LzHGzWYAsUfJ5j';
    return false;
  }

}
