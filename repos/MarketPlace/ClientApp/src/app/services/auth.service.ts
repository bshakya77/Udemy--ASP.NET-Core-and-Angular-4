import { Injectable } from '@angular/core';
import { JwtHelper } from 'angular2-jwt';
import { tokenNotExpired } from 'angular2-jwt';

// Avoid name not found warnings
import Auth0Lock from 'auth0-lock';
import { error } from 'util';

@Injectable()
export class AuthService {

  profile: any;
  private roles: string[] = [];

  // Configure Auth0
  lock = new Auth0Lock('8PTWwjXAICLKLX2Sk1LzHGzWYAsUfJ5j', 'bijshakya77.auth0.com', {});

  constructor() {

    this.readUserFromLocalStorage();

    //Add callback for lock 'authenticated' event
    this.lock.on("authenticated", (authResult) => this.onUserAutheniticated(authResult));
  }

  private readUserFromLocalStorage() {
    this.profile = JSON.parse(localStorage.getItem('profile'));

    var token = localStorage.getItem('token');
    if (token) {
      var jwtHelper = new JwtHelper();
      var decodedToken = jwtHelper.decodeToken(token);
      this.roles = decodedToken['https://marketplace.com/roles'] || [];
    }
  }

  private onUserAutheniticated(authResult) {
    localStorage.setItem('token', authResult.accessToken);

    this.lock.getUserInfo(authResult.accessToken, (error, profile) => {
      if (error)
        throw error;

      localStorage.setItem('profile', JSON.stringify(profile));
      this.readUserFromLocalStorage();
    });
  }

  public isInRole(roleName) {
    return this.roles.indexOf(roleName) > -1;
  }

  public login() {
    // Call the show method to display the widget.
    this.lock.show();
  }

  public authenticated() {
    // Check if there's an unexpired JWT
    // This searches for an item in localStorage with key == 'token'
    return tokenNotExpired('token');
  }

  public logout() {
    // Remove token from localStorage
    localStorage.removeItem('token');
    localStorage.removeItem('profile');
    this.profile = null;
    this.roles = [];
  }

}
