import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatIconModule} from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import {MatToolbarModule} from '@angular/material/toolbar';

// import {LayoutsModule} from "../layouts/layouts.module";
import {PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface} from 'ngx-perfect-scrollbar/public-api';

import {DemoMaterialModule} from "../core/material-module";
import {TableModule} from "primeng/table";
import {SliderModule} from "primeng/slider";
import {MultiSelectModule} from "primeng/multiselect";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {InputTextModule} from "primeng/inputtext";
import {CalendarModule} from 'primeng/calendar';
import {SidebarModule} from "primeng/sidebar";

import {DropdownModule} from "primeng/dropdown";
import {TooltipModule} from "primeng/tooltip";
import {ChipsModule} from "primeng/chips";
import {RippleModule} from "primeng/ripple";
import {ToolbarModule} from "primeng/toolbar";
import {DialogModule} from "primeng/dialog";
import {ConfirmPopupModule} from "primeng/confirmpopup";
import {ConfirmationService, MessageService} from "primeng/api";
import {InputMaskModule} from "primeng/inputmask";
import {NgxMaskModule} from "ngx-mask/index";
import {InputTextareaModule} from "primeng/inputtextarea";
import {FileUploadModule} from "primeng/fileupload";
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {AuthInterceptor} from "../../interceptors/auth/auth.interceptor";
import {ToastModule} from "primeng/toast";
import {AppModule} from "../app.module";
import {KnobModule} from "primeng/knob";
import {NgCircleProgressModule} from "ng-circle-progress/public-api";
import {ProgressSpinnerModule} from "primeng/progressspinner";
import {BlockUIModule} from "primeng/blockui";
import {TagModule} from "primeng/tag";
import {ChipModule} from "primeng/chip";
import {CheckboxModule} from "primeng/checkbox";
import {ToggleButtonModule} from "primeng/togglebutton";
import {CDK_DRAG_CONFIG} from "@angular/cdk/drag-drop";
import {SplitButtonModule} from "primeng/splitbutton";
import {OverlayPanelModule} from "primeng/overlaypanel";
import {GalleriaModule} from "primeng/galleria";
import {SkeletonModule} from "primeng/skeleton";
import {DividerModule} from "primeng/divider";
import { MainComponent } from './main/main.component';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true,
  wheelSpeed: 0.3
};

const DragConfig = {
  // dragStartThreshold: 0,
  // pointerDirectionChangeThreshold: 5,
  zIndex: 10000
};

@NgModule({
  declarations:
    [
    MainComponent
  ],
  imports: [
    CommonModule,
    // LayoutsModule,

    // NG Material Modules
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatToolbarModule,
    DemoMaterialModule,
    TableModule,
    SliderModule,
    MultiSelectModule,
    FormsModule,
    InputTextModule,
    CalendarModule,
    SidebarModule,
    DropdownModule,
    ReactiveFormsModule,
    TooltipModule,
    ChipsModule,
    RippleModule,
    ToolbarModule,
    DialogModule,
    ConfirmPopupModule,
    InputMaskModule,
    // NgxMatTimepickerModule,
    // NgxMatDatetimePickerModule
    NgxMaskModule.forRoot(),
    InputTextareaModule,
    FileUploadModule,
    ToastModule,
    KnobModule,
    NgCircleProgressModule.forRoot({
      "startFromZero": false,
    }),
    ProgressSpinnerModule,
    BlockUIModule,
    TagModule,
    ChipModule,
    CheckboxModule,
    ToggleButtonModule,
    SplitButtonModule,
    OverlayPanelModule,
    GalleriaModule,
    SkeletonModule,
    DividerModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    // DynamicFormControlService,
    ConfirmationService,
    MessageService,
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    },
    {provide: CDK_DRAG_CONFIG, useValue: DragConfig}
  ]
})
export class PagesModule {
}
