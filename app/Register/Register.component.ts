import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-Register',
  templateUrl: './Register.component.html',
  styles: ['input.ng-invalid{border:5px solid red;} input.ng-valid{border-left:5px solid green;}']
})
export class RegisterComponent implements OnInit {
  username:any=" ";
  mobileno:any=" ";
  emailid:any=" ";
  constructor(private user:UserService) { }
submitform()
{
  var info={
    username:this.username,
    mobileno:this.mobileno,
    emailid:this.emailid
  };
  this.user.adduserInformation(info).subscribe(d=>{
    alert("submitted!!!")
  })
}
  ngOnInit() {
  }

}
