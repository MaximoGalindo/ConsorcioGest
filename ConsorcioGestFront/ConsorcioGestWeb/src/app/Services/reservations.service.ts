import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../Helpers/Envriorment";
import { ReservationDTO } from "../Models/DTO/ReservationsDTO";
import { Observable } from "rxjs";
import { CommonSpacesModel } from "../Models/Models/ConsortiumConfigModel";

@Injectable({
    providedIn: 'root'
})
export class ReservationsService {

    baseUrl = `${environment.API_URL}/reservations/`;
    constructor(private http: HttpClient) { }

    GetCommonSpaces():Observable<CommonSpacesModel[]> {
        return this.http.get<CommonSpacesModel[]>(this.baseUrl + 'get-common-spaces')
    }

    GetCommonSpaceById(id:number):Observable<CommonSpacesModel> {
        return this.http.get<CommonSpacesModel>(this.baseUrl + 'get-common-space/'+ id)
    }

    SaveReservation(reservation: ReservationDTO):Observable<any> {
        return this.http.post(this.baseUrl + 'save-reservation', reservation)
    }

    GetSchedulesAvailable(date:string, commonSpaceID:number):Observable<any> {
        return this.http.get(`${this.baseUrl}get-schedules-available/${commonSpaceID}?date=${date}`)
    }
}
