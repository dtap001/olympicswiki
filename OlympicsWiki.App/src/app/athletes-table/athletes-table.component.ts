import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Athlete } from '../models/search-model';
import { MatTableDataSource } from '@angular/material/table';
import COUNTRIES from '../countries';

@Component({
  selector: 'app-athletes-table',
  templateUrl: './athletes-table.component.html',
  styleUrls: ['./athletes-table.component.scss']
})
export class AthletesTableComponent implements OnInit {
  displayedColumns: string[] = ['name', 'country', 'age', 'sports', 'edit', 'delete'];

  @Output() onDelete = new EventEmitter<number>();
  @Input()
  set athletes(val: Athlete[]) {
    this.dataSource = new MatTableDataSource<Athlete>(val);
    console.log("athletes set");
  }

  dataSource = new MatTableDataSource<Athlete>();

  constructor() { }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<Athlete>(this.athletes);
    console.log("ngOnInit");

  }
  getSports(athlete) {
    return athlete.sports.map(function (item) {
      return item.name;
    }).join(" ")
  }

  getAge(birthday) { // birthday is a date
    var today = new Date();
    var birthDate = new Date(birthday);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }
    return age;
  }
  getCountryFlag(countryName) {
     let country = COUNTRIES.filter(function (element, index, array) {
      if (element.name == countryName) {
        return true;
      }
      return false;
    });
    if(country.length ==0){return "";}
    return `https://www.countryflags.io/${country[0].code}/flat/32.png`;
  }

  deleteAthlete(id) {
    console.log("deleteAthlete");
    this.onDelete.emit(id);
  }
}
