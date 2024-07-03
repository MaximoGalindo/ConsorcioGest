import { DOCUMENT } from '@angular/common';
import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService } from 'src/app/Helpers/ConfirmationModal';
import { ConsortiumModel } from 'src/app/Models/HelperModel/ConsortiumModel';
import { AuthService } from 'src/app/Services/auth.service';
import { ConsortiumService } from 'src/app/Services/consortium.service';

@Component({
  selector: 'app-select-consortium',
  templateUrl: './select-consortium.component.html',
  styleUrls: ['./select-consortium.component.css']
})
export class SelectConsortiumComponent {

  consortiums: ConsortiumModel[] = []

  constructor(
    private consortiumService: ConsortiumService,
    private authService: AuthService,
    private router: Router,
    @Inject(DOCUMENT) private document: Document,
    private confirmationService: ConfirmationService
  ) { }

  ngOnInit(): void {
    this.LoadConsortiums()
    this.document.body.classList.remove('background-admin');
    this.document.body.classList.add('backgroud-login');
  }

  LoadConsortiums() {
    this.consortiums = []
    this.consortiumService.GetConsortiums().subscribe({
      next: data => {
        for (var item of data) {
          this.consortiums.push({ Id: item.id, Name: item.name, Location: item.location });
        }
      }
    })
  }

  selectConsortium(consortiumID: number) {
    this.authService.SetCurrentConsortium(consortiumID).subscribe({
      next: data => {
        this.router.navigate(['/main-page-admin'])
      }
    })
  }

  RemoveConsortium(consortiumID: number) {
    this.confirmationService.confirm('¿Estás seguro que quieres borrar este consorcio?').then(confirm => {
      if (confirm) {
        this.consortiumService.DeleteConsortium(consortiumID).subscribe({
          next: data => {
            this.LoadConsortiums()
          }
        })
      }
      else {
        return
      }
    });

  }

  addConsortium() {
    this.router.navigate(['/register-consortium'])
  }
  BackLogin() {
    this.router.navigate(['/login'])
  }
  GoToUsers() {
    this.document.body.classList.remove('backgroud-login');
    this.document.body.classList.add('background-admin');
    this.router.navigate(['/user-admin-gest'])
  }
}
