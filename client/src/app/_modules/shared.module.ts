import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,


    //ToastrModule.forRoot(),
    BsDropdownModule.forRoot(),
    
  ],

  exports:[
    BsDropdownModule
  ]
})
export class SharedModule { }
