import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Chart, ChartType } from 'chart.js/auto';
import { FiltersSharedService } from 'src/app/Services/Shared/filters-shared.service';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.css']
})
export class StatsComponent implements OnInit {

  dateFrom:Date = new Date();
  dateTo:Date | null = null;


  constructor(private dateFilter:FiltersSharedService) { }

  ngOnInit(): void {
      
  }
  Search(){
    this.dateFilter.SetDateFilter(this.dateFrom,this.dateTo);
  }

}
