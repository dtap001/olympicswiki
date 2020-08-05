export class AthleteSearchRequestModel {
    name: string;
    maxBirth: Date;
    minBirth: Date;
    country: string;
}

export class AthleteSearchResponseModel {
    athletes: Athlete[]
}

export class Athlete {
    id: number
    fullName: string;
    birth: string;
    birthPlace: string;
    country: string;
    sports: Sport[];
}

export class Sport {
    id: number;
    name: string;
}