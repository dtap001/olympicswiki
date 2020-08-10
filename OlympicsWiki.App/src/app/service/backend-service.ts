import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Athlete, AthleteSearchRequestModel, AthleteSearchResponseModel, Sport } from '../models/search-model';
@Injectable({
    providedIn: "root"
})
export class BackendService {
    constructor(private http: HttpClient) { }
    private backendAddress = environment.backend;

    athletesSearch(search: AthleteSearchRequestModel): Observable<AthleteSearchResponseModel> {
        const URL = this.backendAddress + "api/athletes/search";
        let response$ = this.http.post<AthleteSearchResponseModel>(URL, search, this.options(this.json()));
        let result$ = new Observable<AthleteSearchResponseModel>(observer => {
            response$.subscribe(response => {

                return observer.next(response);
            });
        });
        return result$;
    }
    getAthlete(id): Observable<Athlete> {
        const URL = this.backendAddress + "api/athletes/" + id;
        let response$ = this.http.get<Athlete>(URL, this.options(this.json()));
        let result$ = new Observable<Athlete>(observer => {
            response$.subscribe(response => {
                return observer.next(response);
            });
        });
        return result$;
    }
    saveAthlete(athlete: Athlete): Observable<void> {      
        const URL = this.backendAddress + "api/athletes/";
        let response$ = this.http.post<void>(URL, athlete, this.options(this.json()));
        let result$ = new Observable<void>(observer => {
            response$.subscribe(response => {
                return observer.next(response);
            });
        });
        return result$;
    }
    deleteAthlete(id): Observable<void> {
        const URL = this.backendAddress + "api/athletes/"+id;
        let response$ = this.http.delete<void>(URL, this.options(this.json()));
        let result$ = new Observable<void>(observer => {
            response$.subscribe(response => {
                return observer.next(response);
            });
        });
        return result$;
    }
    getAllCountries(): Observable<string[]> {
        const URL = this.backendAddress + "api/country/";
        let response$ = this.http.get<string[]>(URL, this.options(this.json()));
        let result$ = new Observable<string[]>(observer => {
            response$.subscribe(response => {
                return observer.next(response);
            });
        });
        return result$;
    }
    getAllSports(): Observable<Sport[]> {
        const URL = this.backendAddress + "api/sports/";
        let response$ = this.http.get<Sport[]>(URL, this.options(this.json()));
        let result$ = new Observable<Sport[]>(observer => {
            response$.subscribe(response => {
                return observer.next(response);
            });
        });
        return result$;
    }

    saveSport(sport: Sport): Observable<void> {
        const URL = this.backendAddress + "api/sports/";
        let response$ = this.http.post<void>(URL, sport, this.options(this.json()));
        let result$ = new Observable<void>(observer => {
            response$.subscribe(response => {
                return observer.next(response);
            });
        });
        return result$;
    }  

    private options(headers?: HttpHeaders) {
        return { withCredentials: true, headers };
    }

    private json(): HttpHeaders {
        return new HttpHeaders({
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Access-Control-Allow-Origin': ["http://localhost:4200", "http://localhost:3000", "http://localhost:5000", "https://localhost:5001"],
            'Access-Control-Allow-Method': ["POST", "GET"],
            "Access-Control-Allow-Headers": "*"
        });
    }
}
