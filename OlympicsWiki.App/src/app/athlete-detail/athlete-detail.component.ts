import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { BackendService } from '../service/backend-service';
import { Sport, Athlete } from '../models/search-model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import COUNTRIES from '../countries';
@Component({
  selector: 'app-athlete-detail',
  templateUrl: './athlete-detail.component.html',
  styleUrls: ['./athlete-detail.component.scss']
})
export class AthleteDetailComponent implements OnInit {
  selectedId: string;
  allSports: Sport[] = [];
  newSportName: string = null;
  form: FormGroup;
  formBuilder: FormBuilder = new FormBuilder();
  allCountries = COUNTRIES;
  constructor(private location: Location, private backendService: BackendService, private route: ActivatedRoute, private snackBar: MatSnackBar) {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      country: ['', Validators.required],
      birth: ['', Validators.required],
      birthPlace: ['', Validators.required],
      selectedSports: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.selectedId = this.route.snapshot.paramMap.get("id");
    this.backendService.getAllSports().subscribe((response: Sport[]) => {
      this.allSports = response;
    }, (err) => {
      console.log("err: " + err);
    });

    if (this.selectedId == "-1") {
      return;
    }
    this.backendService.getAthlete(this.selectedId).subscribe((response: Athlete) => {
      this.form.controls["name"].setValue(response.fullName);
      this.form.controls["birth"].setValue(response.birth);
      this.form.controls["birthPlace"].setValue(response.birthPlace);
      this.form.controls["country"].setValue(response.country);
      this.form.controls["selectedSports"].setValue(response.sports);     
    }, (err) => {
      console.log("err: " + err);
    });
  }

  onBirthChange($event) { }

  newSport(newSportName) {
    this.newSportName = "";
    this.backendService.saveSport({ id: -1, name: newSportName }).subscribe(() => {
      this.ngOnInit();
    }, (err) => {
      console.log("err: " + err);
    });
  }

  deleteSport(sport) {
    this.snackBar.open('Not implemented  yet', "ok", {
      duration: 2000,
    });
  }

  isSavingInProgress = false;
  save() {
    var athleteToSave: Athlete;
    athleteToSave = {
      id: this.selectedId == "-1" ? null : Number(this.selectedId),
      birth: this.form.controls["birth"].value,
      birthPlace: this.form.controls["birthPlace"].value,
      country: this.form.controls["country"].value,
      fullName: this.form.controls["name"].value,
      sports: this.form.controls["selectedSports"].value,
    }
    this.isSavingInProgress = true;
    this.backendService.saveAthlete(athleteToSave).subscribe(() => {
      this.back();
    }, (err) => {
      this.isSavingInProgress = false;
      console.log("err: " + err);
    });
  }

  back() {
    this.location.back();
  }
}
