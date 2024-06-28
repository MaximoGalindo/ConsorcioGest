import { Injectable, TemplateRef } from '@angular/core';

import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Injectable({ providedIn: 'root' })
export class ConfirmationService {

  constructor(private modalService: NgbModal) { }

  confirm(message: string): Promise<boolean> {
    const modalRef = this.modalService.open(ConfirmationDialogComponent);
    modalRef.componentInstance.message = message;

    return modalRef.result.then(result => result === 'confirm');
  }
}
