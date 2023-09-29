import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/helpers/validateForm';
import { AuthService } from 'src/app/services/auth.service';
import { ResetPasswordService } from 'src/app/services/reset-password.service';
import { UserStoreService } from 'src/app/services/user-store.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";
  public resetPasswordEmail!: string;
  public isValidEmail!: boolean;

  loginForm!: FormGroup;
  constructor(
    private fb: FormBuilder, 
    private auth: AuthService, 
    private router: Router,
    private toast: NgToastService,
    private userStore: UserStoreService,
    private resetService: ResetPasswordService
    ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['',Validators.required],
      password: ['',Validators.required]
    })
  }

  hideShowPass(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

  onLogin(){
      if(this.loginForm.valid){
      // send the object to database
      this.auth.login(this.loginForm.value)
        .subscribe({
          next:(res) => {
            this.auth.storeToken(res.accessToken);
            this.auth.storeRefreshToken(res.refreshToken);
            const tokenPayload = this.auth.decodedToken();
            this.userStore.setFullNameForStore(tokenPayload.unique_name);
            this.userStore.setRoleForStore(tokenPayload.role);
            this.toast.success({detail:"SUCCESS", summary:res.message,duration:5000});
            this.loginForm.reset();
            this.router.navigate(['dashboard'])
          },
          error:(err) => {
            this.toast.error({detail:"ERROR", summary:err?.error.message,duration:5000});
            this.loginForm.reset();
          }
        });
        
    }
    else{
      //throw the error using toaster and with required fields
      ValidateForm.validateAllFormFields(this.loginForm);
      alert("your form is invalid")
    }
  }

  checkValidEmail(event: string){
    const value = event;
    const emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,3}$/;
    this.isValidEmail = emailRegex.test(value);
    return this.isValidEmail;
  }

  confirmToSend(){
    if(this.checkValidEmail(this.resetPasswordEmail)){
      console.log(this.resetPasswordEmail);
      
      //API call
      this.resetService.sendResetPasswordLink(this.resetPasswordEmail)
      .subscribe({
        next:(res)=>{
          this.toast.success({
            detail: 'Success',
            summary: 'Reset password success',
            duration: 3000
          });
          this.resetPasswordEmail = "";
          const buttonRef = document.getElementById("closeBtn");
          buttonRef?.click();
        },
        error:(err)=>{
          this.toast.error({
            detail: 'ERROR',
            summary: 'Something went wrong!',
            duration: 3000
          });
        }
      })
    }
  }
  
}


