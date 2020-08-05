import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { Athlete, AthleteSearchRequestModel, AthleteSearchResponseModel } from '../models/search-model';
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
    /*
    this.http.post<AthleteSearchResponseModel>(
      'https://localhost:44372/api/athletes/searches',
      searchEvent).subscribe(data => {
        this.athletes = data.athletes;
      })


    

    groupsFeedback(users: User[]): Observable<GeneralResponse> {
        const URL = this.backendAddress + "groups/feedback";
        let response$ = this.http.post<GeneralResponse>(URL, { users: users }, this.options(this.json()));
        let result$ = new Observable<GeneralResponse>(observer => {
            response$.subscribe(response => {
                if (!response.isOK) {
                    return observer.error(response);
                }
                return observer.next(response);
            });
        });
        return result$;
    }
*/

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
