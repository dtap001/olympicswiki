import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-athlete-detail',
  templateUrl: './athlete-detail.component.html',
  styleUrls: ['./athlete-detail.component.scss']
})
export class AthleteDetailComponent implements OnInit {
  selectedId: string;
  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.selectedId = this.route.snapshot.paramMap.get("id");
  }

  onBirthChange($event) { }

}
