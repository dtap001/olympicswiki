import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AthletesComponent } from './athletes/athletes.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AthleteDetailComponent } from './athlete-detail/athlete-detail.component';


const routes: Routes = [
  { path: 'athletes', component: AthletesComponent },
  { path: 'athlete/:id', component: AthleteDetailComponent },
  { path: '', redirectTo: '/athletes', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
