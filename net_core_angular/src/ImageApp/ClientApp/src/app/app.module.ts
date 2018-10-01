import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxUploaderModule } from 'ngx-uploader';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from "./home/home.component";
import { ImageAddComponent } from "./image-add/image-add.component";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    ImageAddComponent
  ],
  imports: [
    BrowserModule,
    NgxUploaderModule,
    HttpModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
