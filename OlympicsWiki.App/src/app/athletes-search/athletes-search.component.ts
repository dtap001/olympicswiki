import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AthleteSearchRequestModel } from '../models/search-model';
import { BackendService } from '../service/backend-service';
@Component({
  selector: 'app-athletes-search',
  templateUrl: './athletes-search.component.html',
  styleUrls: ['./athletes-search.component.scss']
})
export class AthletesSearchComponent implements OnInit {
  athleteName: string;
  country: string;
  minDateX: Date;
  maxDateX: Date;
  allCountries: string[] = [];
  @Output() onNewSearch = new EventEmitter<AthleteSearchRequestModel>();

  search() {
    this.onNewSearch.emit({
      name: this.athleteName,
      country: this.country,
      maxBirth: this.maxDateX,
      minBirth: this.minDateX
    });
  }

  onMinDateChange(minDate) {
    this.minDateX = minDate.value;
  }
  onMaxDateChange(maxDate) {
    this.maxDateX = maxDate.value;
  }
  constructor(private backendService: BackendService) { }

  ngOnInit(): void {
    this.backendService.getAllCountries().subscribe((response: string[]) => {
      this.allCountries = response;
    }, (err) => {
      console.log("err: " + err);
    });
  }

}
