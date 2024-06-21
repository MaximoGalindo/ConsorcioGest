import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ConsortiumModel } from 'src/app/Models/HelperModel/ConsortiumModel';
import { AuthService } from 'src/app/Services/auth.service';
import { ConsortiumService } from 'src/app/Services/consortium.service';

@Component({
  selector: 'app-select-consortium',
  templateUrl: './select-consortium.component.html',
  styleUrls: ['./select-consortium.component.css']
})
export class SelectConsortiumComponent {

  consortiums:ConsortiumModel[] = []

  constructor(
    private consortiumService:ConsortiumService,
    private authService:AuthService,
    private router:Router
  )
  {}

  ngOnInit(): void {
    this.LoadConsortiums()
  }

  LoadConsortiums(){
    this.consortiumService.GetConsortiums().subscribe({
      next: data => {
        for (var item of data) {
          this.consortiums.push({ Id: item.id, Name: item.name, Location: item.location });
        }
      }
    })
  }

  selectConsortium(consortiumID:number){  
    this.authService.SetCurrentConsortium(consortiumID).subscribe({
      next: data => {
        this.router.navigate(['/main-page-admin'])        
      }
    })
  }

  addConsortium(){
    this.router.navigate(['/register-consortium'])     
  }
  BackLogin(){
    this.router.navigate(['/login'])     
  }
}
