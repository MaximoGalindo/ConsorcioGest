import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../Helpers/Envriorment";
import { ReservationDTO, ReservationUser, UpdateStateReservationDTO } from "../Models/DTO/ReservationsDTO";
import { Observable } from "rxjs";
import { CommonSpacesModel } from "../Models/Models/ConsortiumConfigModel";
import { FilterReservationDTO, FilterReservationUserDTO } from "../Models/DTO/FiltersDTO";

@Injectable({
    providedIn: 'root'
})
export class ReservationsService {

    baseUrl = `${environment.API_URL}/reservations/`;
    constructor(private http: HttpClient) { }

    GetCommonSpacesByUser(): Observable<CommonSpacesModel[]> {
        return this.http.get<CommonSpacesModel[]>(this.baseUrl + 'get-common-spaces-by-user')
    }

    GetCommonSpaces(): Observable<CommonSpacesModel[]> {
        return this.http.get<CommonSpacesModel[]>(this.baseUrl + 'get-common-spaces')
    }

    GetCommonSpaceById(id: number): Observable<CommonSpacesModel> {
        return this.http.get<CommonSpacesModel>(this.baseUrl + 'get-common-space/' + id)
    }

    SaveReservation(reservation: ReservationDTO): Observable<any> {
        return this.http.post(this.baseUrl + 'save-reservation', reservation)
    }

    GetSchedulesAvailable(date: string, commonSpaceID: number): Observable<any> {
        return this.http.get(`${this.baseUrl}get-schedules-available/${commonSpaceID}?date=${date}`)
    }

    GetReservations(filter: FilterReservationDTO): Observable<any> {
        let params = new HttpParams();
        if (filter.commonSpaceID) {
            params = params.append('CommonSpaceID', filter.commonSpaceID.toString());
        }
        if(filter.document){
            params = params.append('Document', filter.document.toString());
        }
        if (filter.dateFrom) {
            params = params.append('DateFrom', filter.dateFrom);
        }
        if (filter.dateTo) {
            params = params.append('DateTo', filter.dateTo);
        }
        return this.http.get<any>(`${this.baseUrl}get-reservations`, { params });
    }
    GetReservationsByUserID(filter:FilterReservationUserDTO): Observable<ReservationUser[]> {
        let params = new HttpParams();
        if (filter.commonSpaceID) {
            params = params.append('CommonSpaceID', filter.commonSpaceID.toString());
        }
        if (filter.dateFrom) {
            params = params.append('DateFrom', filter.dateFrom);
        }
        if (filter.dateTo) {
            params = params.append('DateTo', filter.dateTo);
        }
        return this.http.get<any>(`${this.baseUrl}get-reservations-by-user-id`, { params });
    }
    CancelReservation(reservationID: number): Observable<any> {
        return this.http.post<any>(this.baseUrl + 'cancel-reservation-by-user', reservationID);
    }
    UpdateStateReservation(updateReservation:UpdateStateReservationDTO): Observable<any> {
        return this.http.post<any>(this.baseUrl + 'update-state-reservation', updateReservation);
    }
}
