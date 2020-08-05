import { Component, OnInit, Input } from '@angular/core';
import { Athlete } from '../models/search-model';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-athletes-table',
  templateUrl: './athletes-table.component.html',
  styleUrls: ['./athletes-table.component.scss']
})
export class AthletesTableComponent implements OnInit {
  displayedColumns: string[] = ['name', 'country', 'age', 'sports', 'edit'];

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


}
