import { Component } from '@angular/core';
import Chart, { ChartType } from 'chart.js/auto';
import { StatsService } from 'src/app/Services/stats.service';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})
export class PieChartComponent {

  public chart: Chart | undefined;

  constructor(private statsService: StatsService) {

  }

  ngOnInit(): void {

    let dateFrom: Date | null = null;
    let dateTo: Date | null = null;

    this.statsService.GetMostFrequentComplaintsByCauseOfComplaint(dateFrom, dateTo).subscribe(stats => {

      const labels: string[] = Object.keys(stats.data); // Obteniendo las claves del objeto como un array
      const values: number[] = Object.values(stats.data).map(Number);; // Obteniendo los valores del objeto como un array

      const data = {
        labels: labels,
        datasets: [{
          label: 'Causa de Reclamo Mas Frecuente',
          data: values,
          backgroundColor: [
            'rgb(255, 99, 132)',   // Rojo
            'rgb(54, 162, 235)',   // Azul
            'rgb(255, 205, 86)',   // Amarillo
            'rgb(75, 192, 192)',   // Verde agua
            'rgb(153, 102, 255)',  // Morado
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
                size: 14 // Tamaño de fuente para las etiquetas
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
