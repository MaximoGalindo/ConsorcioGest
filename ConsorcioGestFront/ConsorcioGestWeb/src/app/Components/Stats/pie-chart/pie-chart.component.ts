import { Component, Input, SimpleChanges } from '@angular/core';
import Chart, { ChartType } from 'chart.js/auto';
import { FiltersSharedService } from 'src/app/Services/Shared/filters-shared.service';
import { StatsService } from 'src/app/Services/stats.service';
import { Utils } from 'src/app/Helpers/Utils';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent {

  public chart: Chart | undefined;
  constructor(private statsService: StatsService, private dateFilter: FiltersSharedService) {

  }

  ngOnInit(): void {
    this.dateFilter.DateFilter$.subscribe(date => {
      this.GetStats(date.dateFrom, date.dateTo);
    })
  }
  GetStats(dateFrom: string | null, dateTo: string | null) {
    if (dateFrom && !dateTo) { 
        dateTo = Utils.parseDate(new Date());
    }
    if (this.chart) {
      this.chart.destroy()
    }
    this.statsService.GetMostFrequentComplaintsByCauseOfComplaint(dateFrom, dateTo).subscribe(stats => {
      var labels: string[] = ['No hay resultados']
      var values: number[] = [1]
      if (Object.keys(stats.data).length !== 0 || stats.data.constructor !== Object) {
        labels = Object.keys(stats.data);
        values = Object.values(stats.data).map(Number);
      }

      const data = {
        labels: labels,
        datasets: [{
          label: 'Causa de Reclamo Mas Frecuente',
          data: values,
          backgroundColor: [
            'rgb(255, 99, 132)',
            'rgb(54, 162, 235)',
            'rgb(255, 205, 86)',
            'rgb(75, 192, 192)',
            'rgb(153, 102, 255)',
          ],
          hoverOffset: 4
        }]
      };
      const options = {
        plugins: {
          legend: {
            labels: {
              color: 'white',
              font: {
                size: 10
              }
            }
          }
        }
      };

      this.chart = new Chart('pieChart', {
        type: 'pie' as ChartType,
        data: data,
        options: options
      });
    })
  }

}
