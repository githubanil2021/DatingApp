import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { of } from 'rxjs/internal/observable/of';
 
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

  constructor(public accountService: AccountService) { }

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
          //this.loggedIn = true;
        },
        error: error => console.log(error)
      })
    console.log(this.model);
  }

  logout(){
    //this.loggedIn = false;
    this.accountService.logout();
  }
}
