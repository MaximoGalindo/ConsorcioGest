import { ToastService } from "./ToastService";

export class Utils {
  private static toastService: ToastService;

  static initialize(toastService: ToastService) {
    Utils.toastService = toastService;
  }

  static success(message: string, delay: number = 1500) {
    if (Utils.toastService) {
      Utils.toastService.success(message, delay);
    } else {
      console.error('ToastService is not initialized.');
    }
  }

  static error(message: string, delay: number = 1500) {
    if (Utils.toastService) {
      Utils.toastService.error(message, delay);
    } else {
      console.error('ToastService is not initialized.');
    }
  }

  static parseDate(date: Date): string {       
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2);
    const day = ('0' + date.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
  }
}
