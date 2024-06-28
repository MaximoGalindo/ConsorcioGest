import { Component, TemplateRef } from '@angular/core';
import { ToastService } from '../ToastService';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css']
})
export class ToastComponent {

  constructor(public toastService: ToastService) {}

  isTemplate(toast: any): boolean { 
    return toast.textOrTpl instanceof TemplateRef; 
  }

  removeToast(toast:any): void {
    this.toastService.remove(toast);
  }
}

