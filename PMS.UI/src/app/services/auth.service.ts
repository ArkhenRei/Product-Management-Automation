import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl:string = "https://localhost:44348/api/User/"
  constructor(private http: HttpClient) { }

  signUp(userObj:any){
    return this.http.post<any>(`${this.baseUrl}register`,userObj);
    //return this.http.post<any>(this.baseUrl + '/api/User/register',userObj);
  }

  login(loginObj:any){
    return this.http.post<any>(`${this.baseUrl}authenticate`,loginObj); 
    //return this.http.post<any>(this.baseUrl + '/api/User/authenticate',loginObj);
  }
}
