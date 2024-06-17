import { Component } from '@angular/core';
import { NgbCalendar, NgbDate, NgbDateStruct, NgbDatepickerNavigateEvent } from '@ng-bootstrap/ng-bootstrap';
import { StatsService } from 'src/app/Services/stats.service';

@Component({
  selector: 'app-number-claims-chart',
  templateUrl: './number-claims-chart.component.html',
  styleUrls: ['./number-claims-chart.component.css']
})
export class NumberClaimsChartComponent {

  selectedMonth: number = 0;
  selectedYear: number = 0;
  claimsS:number = 0
  claimsR:number = 0
  claimsI:number = 0

  months = [
    { name: 'Enero', value: 1 },
    { name: 'Febrero', value: 2 },
    { name: 'Marzo', value: 3 },
    { name: 'Abril', value: 4 },
    { name: 'Mayo', value: 5 },
    { name: 'Junio', value: 6 },
    { name: 'Julio', value: 7 },
    { name: 'Agosto', value: 8 },
    { name: 'Septiembre', value: 9 },
    { name: 'Octubre', value: 10 },
    { name: 'Noviembre', value: 11 },
    { name: 'Diciembre', value: 12 },
  ];

  years: number[] = [];

  constructor(private statsService: StatsService) {
    const currentYear = new Date().getFullYear();
    for (let i = currentYear - 10; i <= currentYear; i++) {
      this.years.push(i);
    }

    this.selectedMonth = new Date().getMonth() + 1;
    this.selectedYear = new Date().getFullYear();
  }

  ngOnInit(): void { 
    this.GetStats(this.selectedMonth, this.selectedYear);
  }

  GetStats(month:number,year:number) {
    this.statsService.GetNumberOfGestionClaimsPerMonth(month,year).subscribe(stats => {
      if (stats && stats.data) {
        this.claimsS = stats.data.Satisfechos || 0;
        this.claimsR = stats.data.Regular || 0;
        this.claimsI = stats.data.Insatisfechos || 0;
      }
    });
  }
  onMonthChange() {
    this.GetStats(this.selectedMonth, this.selectedYear);
  }
}
