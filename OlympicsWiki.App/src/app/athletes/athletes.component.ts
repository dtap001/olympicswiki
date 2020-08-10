import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AthleteSearchRequestModel, AthleteSearchResponseModel, Athlete } from '../models/search-model';
import { BackendService } from '../service/backend-service';
@Component({
  selector: 'app-athletes',
  templateUrl: './athletes.component.html',
  styleUrls: ['./athletes.component.scss']
})
export class AthletesComponent implements OnInit {

  constructor(private backendService: BackendService) { }
  public athletes: Athlete[] = [];
  lastSearch:AthleteSearchRequestModel;
  ngOnInit(): void {
    this.doSearch({ name: null, country: null, maxBirth: null, minBirth: null });
  }

  doSearch(searchEvent: AthleteSearchRequestModel) {
    this.lastSearch = searchEvent;
    this.backendService.athletesSearch(searchEvent).subscribe((response: AthleteSearchResponseModel) => {
      this.athletes = response.athletes;
    }, (err) => {
      console.log("err: " + err);
    });
  }
  doDelete(id){
    console.log("doDelete")
    this.backendService.deleteAthlete(id).subscribe(() => {
      this.doSearch(this.lastSearch);
    }, (err) => {
      console.log("err: " + err);
    });  
  }

}
