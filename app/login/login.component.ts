import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ConfirmedValidator } from '../Conformed.validator';

@Component({
  selector: 'app-login',
  templateUrl:'./login.component.html',
  styles: ['input.ng-invalid{border:5px solid red;} input.ng-valid{border-left:5px solid green;}']
})
export class LoginComponent implements OnInit {
loginform=new FormGroup({
username:new FormControl(""),
password:new FormControl(),
cpassword:new FormControl(),

});
  constructor(private fb:FormBuilder) { }
  loginforms=this.fb.group({
    username:["enter username",[Validators.required,Validators.minLength(3)]],
    password:[" ",[Validators.required]],
    cpassword:[" ",[Validators.required]],
 },{validator:ConfirmedValidator('password','cpassword')});

  ngOnInit() {
  }

}
