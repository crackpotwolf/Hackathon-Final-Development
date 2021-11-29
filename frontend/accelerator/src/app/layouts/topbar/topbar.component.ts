import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AuthService} from "../../../services/auth/auth.service";
import {MessageService} from "primeng/api";
import {AppComponent} from "../../app.component";
import {AppModule} from "../../app.module";
import {WrapperComponent} from "../wrapper/wrapper.component";


@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.sass']
})

export class TopbarComponent implements OnInit {

  // @Output() mobileMenuButtonClicked = new EventEmitter();
  @Output() settingsButtonClicked = new EventEmitter();


  /**
   * Количество новых уведомлений
   */
  countNewNotifications: number = 0;


  constructor(private http: HttpClient,
              private messageService: MessageService,
              private authService: AuthService,) {
  }

  ngOnInit(): void {
  }

  // /**
  //  * Toggle the menu bar when having mobile screen
  //  */
  // toggleMobileMenu(event: any) {
  //   event.preventDefault();
  //   this.mobileMenuButtonClicked.emit();
  // }

  /**
   * Выход
   */
  logout() {
    this.authService.logout();
  }

  /**
   * Клик по кнопе "Поиск"
   * @param event
   */
  onClickSearch(event: MouseEvent) {
    WrapperComponent.notImplemented(this.messageService);
  }

  /**
   * Клик по кнопе "Избранное"
   * @param event
   */
  onClickFavorites(event: MouseEvent) {
    WrapperComponent.notImplemented(this.messageService);
  }
}
