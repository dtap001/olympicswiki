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
  ngOnInit(): void { }

  doSearch(searchEvent: AthleteSearchRequestModel) {
    this.backendService.athletesSearch(searchEvent).subscribe((response: AthleteSearchResponseModel) => {     
      this.athletes = response.athletes;
    }, (err) => {     
      console.log("err: " + err);
    });
  }
}
