import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs';
import { AuthService } from 'src/app/Services/auth.service';
import { StorageService } from 'src/app/Services/storage.service';

@Component({
  selector: 'app-main-page-admin',
  templateUrl: './main-page-admin.component.html',
  styleUrls: ['./main-page-admin.component.css']
})
export class MainPageAdminComponent implements OnInit, OnDestroy {

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private router:Router,
    private authService:AuthService,
    private storageService:StorageService
  ) {}
  ngOnInit(): void {
    //ES SIMPLEMENTE PARA CAMBIAR LOS COLORES DE FONDO
    this.document.body.classList.remove('backgroud-login');
    this.document.body.classList.add('background-admin');
  }

  ngOnDestroy(): void {
    //ES SIMPLEMENTE PARA CAMBIAR LOS COLORES DE FONDO
    this.document.body.classList.remove('background-admin');
    this.document.body.classList.add('backgroud-login');
  }


  BackToSelectConsortium(){
    this.authService.RemoveCurrentConsortium().subscribe({
      next: () => {this.router.navigate(['/consortium'])}
    });
  }

  Logout(){
    this.authService.Logout().pipe(
      finalize(() => {
        this.storageService.clean();
        this.router.navigate(['/login']);
      })
    ).subscribe({
      next: () => {
      }
    });
  }
}
