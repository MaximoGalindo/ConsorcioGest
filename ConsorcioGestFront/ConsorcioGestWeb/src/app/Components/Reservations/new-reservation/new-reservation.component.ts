import { Component, Injectable, OnInit, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbCalendar, NgbDate, NgbDateAdapter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { CommonSpacesModel } from 'src/app/Models/Models/ConsortiumConfigModel';
import { ReservationsService } from 'src/app/Services/reservations.service';
@Injectable()
export class CustomAdapter extends NgbDateAdapter<string> {
	readonly DELIMITER = '-';

	fromModel(value: string | null): NgbDateStruct | null {
		if (value) {
			const date = value.split(this.DELIMITER);
			return {
				day: parseInt(date[0], 10),
				month: parseInt(date[1], 10),
				year: parseInt(date[2], 10),
			};
		}
		return null;
	}

	toModel(date: NgbDateStruct | null): string | null {
		return date ? date.year + this.DELIMITER + date.month + this.DELIMITER + date.day : null;
	}
}


@Component({
	selector: 'app-new-reservation',
	templateUrl: './new-reservation.component.html',
	styleUrls: ['./new-reservation.component.css'],
	providers: [
		{ provide: NgbDateAdapter, useClass: CustomAdapter },
	],
})

export class NewReservationComponent implements OnInit {

	ShowSchedules:boolean = false;

	model: string = '';
	minDate: NgbDate = this.ngbCalendar.getToday();
	id: number = 0;
	schedules: { hour: string, available: boolean }[] = [];
	//private calendar = inject(NgbCalendar);
	constructor(
		private ngbCalendar: NgbCalendar,
		private dateAdapter: NgbDateAdapter<string>,
		private reservationService:ReservationsService,
		private route: ActivatedRoute
	) {

	}

	ngOnInit(): void {
		this.id = this.route.snapshot.params['commonSpaceID'];


	}

	get today() {
		return this.dateAdapter.toModel(this.ngbCalendar.getToday())!;
	}

	//isWeekend = (date: NgbDate) => this.calendar.getWeekday(date) >= 6 
	Next(){

		this.ShowSchedules = true
		console.log(this.model);
		this.reservationService.GetSchedulesAvailable(this.model,this.id).subscribe((data)=>{
			console.log(data);
			this.schedules = data	
		})
	}

	Back(){
		this.ShowSchedules = false
	}


}
