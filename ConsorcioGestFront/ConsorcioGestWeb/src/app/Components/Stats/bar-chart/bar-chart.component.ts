import { Component } from '@angular/core';
import Chart, { ChartType } from 'chart.js/auto';
import { StatsService } from 'src/app/Services/stats.service';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent {
  public chart: Chart | undefined;

  constructor(private statsService: StatsService) {

  }

  ngOnInit(): void {

    this.statsService.GetNumberOfClaimsPerMonths().subscribe(stats => {
      const labels: string[] = Object.keys(stats.data); // Obteniendo las claves del objeto como un array
      const values: number[] = Object.values(stats.data).map(Number);; // Obteniendo los valores del objeto como un array

      const data2 = {
        labels: labels,
        datasets: [{
          label: 'Cantidad de Reclamos Por Mes',
          data: values,
          backgroundColor: [
            'rgba(255, 99, 132, 0.2)',    // Rojo claro
            'rgba(255, 159, 64, 0.2)',    // Naranja claro
            'rgba(255, 205, 86, 0.2)',    // Amarillo claro
            'rgba(75, 192, 192, 0.2)',    // Verde agua claro
            'rgba(54, 162, 235, 0.2)',    // Azul claro
            'rgba(153, 102, 255, 0.2)',   // Morado claro
            'rgba(201, 203, 207, 0.2)',   // Gris claro
            'rgba(255, 99, 71, 0.2)',     // Rojo tomate claro
            'rgba(144, 238, 144, 0.2)',   // Verde claro
            'rgba(173, 216, 230, 0.2)',   // Azul cielo claro
            'rgba(255, 218, 185, 0.2)',   // Melocotón claro
            'rgba(221, 160, 221, 0.2)'    // Ciruela claro
          ],
          borderColor: [
            'rgb(255, 99, 132)',    // Rojo
            'rgb(255, 159, 64)',    // Naranja
            'rgb(255, 205, 86)',    // Amarillo
            'rgb(75, 192, 192)',    // Verde agua
            'rgb(54, 162, 235)',    // Azul
            'rgb(153, 102, 255)',   // Morado
            'rgb(201, 203, 207)',   // Gris
            'rgb(255, 99, 71)',     // Rojo tomate
            'rgb(144, 238, 144)',   // Verde
            'rgb(173, 216, 230)',   // Azul cielo
            'rgb(255, 218, 185)',   // Melocotón
            'rgb(221, 160, 221)'    // Ciruela
          ],
          borderWidth: 1
        }]
      };


      this.chart = new Chart('barChart', {
        type: 'bar' as ChartType,
        data: data2,
        options: {
          scales: {
            y: {
              beginAtZero: true
            }
          }
        },
      })


    })


  }
}
