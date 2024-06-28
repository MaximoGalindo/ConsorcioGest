import { Injectable, TemplateRef } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ToastService {
  toasts: any[] = [];

  show(textOrTpl: string | TemplateRef<any>, options: any = {}) {
    this.toasts.push({ textOrTpl, ...options });
  }

  remove(toast:any) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }

  success(message: string) {
    this.show(message, { classname: 'bg-success text-light', delay: 1500 });
  }

  error(message: string) {
    this.show(message, { classname: 'bg-danger text-light', delay: 1500 });
  }
}
