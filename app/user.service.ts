import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

constructor(private client:HttpClient) { }

adduserInformation(info:any){

  return this.client.post("http://localhost:3000/user",info);


}

}

