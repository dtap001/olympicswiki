import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HeaderComponent } from './header/header.component';
import { AthletesComponent } from './athletes/athletes.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { AthletesSearchComponent } from './athletes-search/athletes-search.component';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { AthletesTableComponent } from './athletes-table/athletes-table.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { MatNativeDateModule } from '@angular/material/core';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AthleteDetailComponent } from './athlete-detail/athlete-detail.component';
import {MatSnackBarModule} from '@angular/material/snack-bar';
@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    AthletesComponent,
    AthletesSearchComponent,
    AthletesTableComponent,
    PageNotFoundComponent,
    AthleteDetailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    MatToolbarModule,
    MatIconModule,
    MatTableModule,
    MatCardModule,
    MatInputModule,
    MatDatepickerModule,
    FormsModule,
    MatSelectModule,
    MatNativeDateModule,
    NoopAnimationsModule,
    MatButtonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatSnackBarModule,
    ReactiveFormsModule
  ],
  providers: [HttpClientModule, HttpClient,MatSnackBarModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
