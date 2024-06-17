import { NgPlural } from '@angular/common';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FiltersSharedService {

  private dateFiler =  new BehaviorSubject<{ dateFrom: any; dateTo: any }>({ dateFrom: null, dateTo: null });
  DateFilter$ = this.dateFiler.asObservable();
  
  constructor() { }

  SetDateFilter(dateFrom: any, dateTo: any) {
    this.dateFiler.next({ dateFrom, dateTo });
  }
}
