import { CommonModule, NgIf } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { ConfirmPasswordValidator } from 'src/app/helpers/confirm-password.validator';
import ValidateForm from 'src/app/helpers/validateForm';
import { ResetPassword } from 'src/app/models/reset-password.model';
import { ResetPasswordService } from 'src/app/services/reset-password.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, NgIf, CommonModule]
})
export class ResetPasswordComponent implements OnInit {

  resetPasswordForm!: FormGroup;
  emailToReset!: string;
  emailToken!: string;
  resetPasswordObj = new ResetPassword();

  constructor(private fb: FormBuilder, private activatedRoute: ActivatedRoute, private resetService: ResetPasswordService, private toast: NgToastService, private router: Router) { }

  ngOnInit(): void {
    this.resetPasswordForm = this.fb.group({
      password: [null, Validators.required],
      confirmPassword: [null, Validators.required]
    },{
      validator: ConfirmPasswordValidator("password","confirmPassword")
    });

    this.activatedRoute.queryParams.subscribe(val=>{
      this.emailToReset = val['email'];
      let urlToken = val['code'];
      this.emailToken = urlToken.replace(/ /g,'+');
      console.log(this.emailToken);
      console.log(this.emailToReset);
    })
  }

  reset(){
    if(this.resetPasswordForm.valid){
      this.resetPasswordObj.email = this.emailToReset;
      this.resetPasswordObj.newPassword = this.resetPasswordForm.value.password;
      this.resetPasswordObj.confirmPassword = this.resetPasswordForm.value.confirmPassword;
      this.resetPasswordObj.emailToken = this.emailToken;

      this.resetService.resetPassword(this.resetPasswordObj)
      .subscribe({
        next:(res)=>{
          this.toast.success({
            detail: 'SUCCESS',
            summary: "Password reset successfully",
            duration: 3000
          })
          this.router.navigate(['/'])
        },
        error:(err)=>{
          this.toast.error({
            detail: 'ERROR',
            summary: 'Something went wrong!',
            duration: 3000
          });
        }
      })
    }else{
      ValidateForm.validateAllFormFields(this.resetPasswordForm);
    }
  }

}
