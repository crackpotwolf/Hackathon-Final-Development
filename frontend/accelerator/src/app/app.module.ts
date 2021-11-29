import {NgModule, OnInit} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {LoginComponent} from "./pages/account/login/login.component";
import {FilterMatchMode, MessageService, PrimeNGConfig} from "primeng/api";
import {TranslateLoader, TranslateModule, TranslateService} from "@ngx-translate/core";
import {HTTP_INTERCEPTORS, HttpClient, HttpClientModule} from "@angular/common/http";
import {TranslateHttpLoader} from "@ngx-translate/http-loader";
import {MatFormFieldModule} from "@angular/material/form-field";
import {ReactiveFormsModule} from "@angular/forms";
import {DemoMaterialModule} from "./core/material-module";
import {NoopAnimationsModule} from "@angular/platform-browser/animations";
import {AuthInterceptor} from "../interceptors/auth/auth.interceptor";
import {LayoutsModule} from "./layouts/layouts.module";
import {PagesModule} from "./pages/pages.module";
import {ToastModule} from "primeng/toast";

export function createTranslateLoader(http: HttpClient): any {
  return new TranslateHttpLoader(http, 'assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NoopAnimationsModule,
    DemoMaterialModule,
    ReactiveFormsModule,
    PagesModule,
    LayoutsModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: createTranslateLoader,
        deps: [HttpClient]
      }
    }),
    ToastModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})

export class AppModule implements OnInit {

  constructor(private config: PrimeNGConfig,
              private messageService: MessageService,
              private translateService: TranslateService) {
    this.config.ripple = true;
    this.translateService.addLangs(['ru', 'en']);
    this.translateService.use('ru');
    this.translateService.setDefaultLang('ru');
    this.translateService.get('primeng').subscribe(res => this.config.setTranslation(res));
  }

  ngOnInit(): void {
  }

}
