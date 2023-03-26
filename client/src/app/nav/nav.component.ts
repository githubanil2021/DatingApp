import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
//import { Observable } from 'rxjs';
//import { of } from 'rxjs/internal/observable/of';
 
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any={};
  //loggedIn = false;
  currentUser$ : Observable<User | null> = of(null);

  constructor(public accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    // this.getCurrentUser();
    //this.currentUser$ = this.accountService.currentUser$;
  }

  // getCurrentUser(){
  //   this.accountService.currentUser$.subscribe({
  //     next: user => this.loggedIn = !!user,
  //     error: error => console.log(error)
  //   })
  // };

  login(){
    this.accountService.login(this.model)
      .subscribe({
        next: response => {

          console.log('success login:'+response);
          this.router.navigateByUrl('/members');
          //this.loggedIn = true;
        },
        error: error => {
          console.log(error);
          alert(error.error);
        }
      })
    console.log(this.model);
  }

  logout(){
    //this.loggedIn = false;
    this.accountService.logout();
    this.router.navigateByUrl('/')
  }
}
