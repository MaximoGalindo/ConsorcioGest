import { Component } from '@angular/core';
import Chart, { ChartType } from 'chart.js/auto';
import { StatsService } from 'src/app/Services/stats.service';

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.css']
})
export class LineChartComponent {
  public chart: Chart | undefined;

  constructor(private statsService: StatsService) {

  }

  ngOnInit(): void {

    this.statsService.GetNumberOfClaimsPerMonths().subscribe(stats => {
      const monthMap: { [key: string]: string } = {
        "January": "Ene",
        "February": "Feb",
        "March": "Mar",
        "April": "Abr",
        "May": "May",
        "June": "Jun",
        "July": "Jul",
        "August": "Ago",
        "September": "Sep",
        "October": "Oct",
        "November": "Nov",
        "December": "Dic"
      };

      const labels: string[] = Object.keys(stats.data).map(month => monthMap[month as keyof typeof monthMap]);
      const values: number[] = Object.values(stats.data).map(Number);

      const data2 = {
        labels: labels,
        datasets: [{
          label: 'Cantidad de Reclamos Por Mes',
          data: values,
          fill: false,
          borderColor: 'rgb(75, 192, 192)',
          tension: 0.1
        }]
      };


      this.chart = new Chart('LineChart', {
        type: 'line' as ChartType,
        data: data2,
        options: {
          plugins: {
            legend: {
              display: false // Esto elimina la leyenda completa del gráfico
            }
          },
          scales: {
            x: {
              ticks: {
                color: 'white' 
              },
              grid: {
                color: 'rgba(255, 255, 255, 0.1)' 
              }
            },
            y: {
              beginAtZero: true,
              ticks: {
                color: 'white'
              },
              grid: {
                color: 'rgba(255, 255, 255, 0.1)'
              }
            }
          }
        }
      })


    })


  }
}
