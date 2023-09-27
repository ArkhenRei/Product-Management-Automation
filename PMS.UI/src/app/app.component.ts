import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { UserStoreService } from './services/user-store.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit{
  title = 'PMS.UI';

  public fullName : string = "";
  public role : string = "";
  public showHeader: boolean = true;

  constructor(private auth: AuthService, private userStore: UserStoreService, private router: Router){
    router.events.subscribe((val)=>{
      if(val instanceof NavigationEnd){
        if(val.url == '/' || val.url == '/login'){
          this.showHeader = false;
        }
        else{
          this.showHeader = true;
        }
      }
    });
  }

  ngOnInit(){
    this.userStore.getFullNameFromStore()
    .subscribe(val=>{
      let fullNameFromToken = this.auth.getFullNameFromToken();
      this.fullName = val || fullNameFromToken
    })

    this.userStore.getRoleFromStore()
    .subscribe(val=>{
      const roleFromToken = this.auth.getRoleFromToken();
      this.role = val || roleFromToken;
    })
  }

  
}
