import { Component, Injectable, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbCalendar, NgbDate, NgbDateAdapter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { Utils } from 'src/app/Helpers/Utils';
import { ReservationDTO } from 'src/app/Models/DTO/ReservationsDTO';
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

	ShowSchedules: boolean = false;

	model: string = '';
	minDate: NgbDate = this.ngbCalendar.getToday();
	id: number = 0;
	schedules: Array<{ hour: string, available: boolean, selected: boolean }> = [];
	selectedHours: string[] = [];
	hoursAviable:number = 0;

	//private calendar = inject(NgbCalendar);
	constructor(
		private ngbCalendar: NgbCalendar,
		private dateAdapter: NgbDateAdapter<string>,
		private reservationService: ReservationsService,
		private route: ActivatedRoute,
		private router: Router
	) {

	}

	ngOnInit(): void {
		this.id = this.route.snapshot.params['commonSpaceID'];
		this.reservationService.GetCommonSpaceById(this.id).subscribe((data) => {
			this.hoursAviable = data.userLimit
		})

	}

	get today() {
		return this.dateAdapter.toModel(this.ngbCalendar.getToday())!;
	}

	//isWeekend = (date: NgbDate) => this.calendar.getWeekday(date) >= 6

	onCheckboxChange(hour: string, isChecked: boolean) {
		if (isChecked) {
			if (this.selectedHours.length >= this.hoursAviable) {
				Utils.error("Has alcanzado el límite de horas que se pueden reservar por usuario");
				const scheduleIndex = this.schedules.findIndex(s => s.hour === hour);
				if (scheduleIndex > -1) {
					this.schedules[scheduleIndex].selected = false;
					(document.getElementById('time' + scheduleIndex) as HTMLInputElement).checked = false;
				}
				return;
			}
			this.selectedHours.push(hour);
		} else {
			const index = this.selectedHours.indexOf(hour);
			if (index > -1) {
				this.selectedHours.splice(index, 1);
			}
		}

		if (!this.areSelectedHoursConsecutive()) {
			Utils.error("Las horas seleccionadas no son consecutivas");
			const scheduleIndex = this.schedules.findIndex(s => s.hour === hour);
			if (scheduleIndex > -1) {
				this.schedules[scheduleIndex].selected = false;
				(document.getElementById('time' + scheduleIndex) as HTMLInputElement).checked = false;
			}
			this.selectedHours = this.selectedHours.filter(h => h !== hour);
		}
	}

	areSelectedHoursConsecutive(): boolean {
		if (this.selectedHours.length < 2) {
			return true;
		}

		const sortedHours = this.selectedHours
			.map(hour => this.convertToMinutes(hour))
			.sort((a, b) => a - b);

		for (let i = 1; i < sortedHours.length; i++) {
			if (sortedHours[i] !== sortedHours[i - 1] + 60) { 
				return false;
			}
		}
		return true;
	}

	Save() {
		if (this.selectedHours.length > 0) {
			const sortedHours = this.selectedHours.sort((a, b) => {
				return this.convertToMinutes(a) - this.convertToMinutes(b);
			});
			const firstHour = sortedHours[0];
			const lastHour = sortedHours[sortedHours.length - 1];

			var reservationDTO = new ReservationDTO();
			reservationDTO = {
				date:this.model,
				commonSpaceConsortiumID: this.id,
				hourFrom: firstHour,
				hourTo: lastHour	
			}

			this.reservationService.SaveReservation(reservationDTO).subscribe((data) => {
				console.log(data);
				this.router.navigate(['/main-page-user/reservation-common-spaces']);
				Utils.success("Se genero correctamente la reserva")
			})
		}
	}

	convertToMinutes(time: string): number {
		const [hours, minutes] = time.split(':').map(Number);
		return hours * 60 + minutes;
	}

	Next() {
		this.ShowSchedules = true
		console.log(this.model);
		this.reservationService.GetSchedulesAvailable(this.model, this.id).subscribe((data) => {
			console.log(data);
			for (var schedule of data) {
				this.schedules.push({
					hour: schedule.hour,
					available: schedule.available,
					selected: false
				})
			}
		})
	}

	Back() {
		this.ShowSchedules = false
		this.selectedHours = []
		this.schedules = []
	}

	ToCommonSpaces() {
		this.selectedHours = []
		this.schedules = []
		this.model = ''
		this.router.navigate(['/main-page-user/reservation-common-spaces']);
	}
}
