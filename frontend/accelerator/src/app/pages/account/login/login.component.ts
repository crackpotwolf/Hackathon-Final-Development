import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {AuthService} from "../../../../services/auth/auth.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  form: FormGroup;

  /// Флаг ошибки авторизации
  isErrorAuthentication: boolean = true;
  response: any;


  constructor(private fb: FormBuilder,
              private authService: AuthService,
              private http: HttpClient,
              private router: Router) {
    this.form = this.fb.group({
      email: ['test@test.ru', Validators.required],
      password: ['672412Aa', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  /// Обработка нажатия кнопки "Войти"
  onLogin() {
    const val = this.form.value;
    //console.log(val);
    if (val.email && val.password) {
      this.authService.login(val.email, val.password)
        .subscribe((resp) => {
          localStorage.setItem('token', resp.access_token);
          this.http.get('/api/authentication/v1/account/user-info')
            .subscribe((userInfo: any) => {
              localStorage.setItem('showColumnsEmployees', JSON.stringify(userInfo['showColumnsEmployees']));
              this.router.navigate(['']);
            }, (err) => {
              this.isErrorAuthentication = true;
              this.response = err;
            });
        }, error => {
          this.isErrorAuthentication = true;
          this.response = error;
        });
      // .subscribe(
      //   () => {
      //     console.log("User is logged in");
      //     this.router.navigateByUrl('/');
      //   }
      // );
    }
  }

  /// Получение описания ошибки
  getErrorMessage() {
    if (this.response) {
      if (this.response.error.errorText == 'Invalid username or password.') {
        return 'Не верная почта или пароль'
      } else if (this.response.status != 200) {
        return 'Сервер не доступен';
      }
    }
    return '';
  }
}
