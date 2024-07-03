import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Utils } from 'src/app/Helpers/Utils';
import { RegisterUserDTO } from 'src/app/Models/DTO/RegisterUserDTO';
import { ConsortiumModel } from 'src/app/Models/HelperModel/ConsortiumModel';
import { DocumentTypeModel } from 'src/app/Models/HelperModel/DocumentTypeModel';
import { ConsortiumService } from 'src/app/Services/consortium.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  loading: boolean = false;

  registerForm: FormGroup = new FormGroup({});
  confirmPassword: string = '';
  documentTypes: DocumentTypeModel[] = [];
  consortiums: ConsortiumModel[] = [];

  adminRegister: boolean = false;
  residentRegister: boolean = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private consortiumService:ConsortiumService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const userType = params['userType'];
      if (userType == 'Residente') {
        this.residentRegister = true;
        this.adminRegister = false;
      }
      else if (userType == 'Admin') {
        this.adminRegister = true;
        this.residentRegister = false;
      }
    });

    if (this.residentRegister) {
      this.registerForm = this.fb.group({
        Name: ['', Validators.required],
        LastName: ['', Validators.required],
        Email: ['', [Validators.required, Validators.email]],
        Password: ['', Validators.required],
        ConfirmPassword: ['', Validators.required],
        Phone: ['', Validators.required],
        Document: ['', Validators.required],
        UserType: ['', Validators.required],
        DocumentType: ['', Validators.required],
        Consortium: ['', Validators.required],
      });
    }
    else if (this.adminRegister) {
      this.registerForm = this.fb.group({
        Name: ['', Validators.required],
        LastName: ['', Validators.required],
        Email: ['', [Validators.required, Validators.email]],
        Password: ['', Validators.required],
        ConfirmPassword: ['', Validators.required],
        Phone: ['', Validators.required],
        Document: ['', Validators.required],
        DocumentType: ['', Validators.required],
      });
    }

    this.LoadDocumentTypes();
    this.LoadConsortiums();
  }

  SubmitForm() {
    if (this.registerForm.invalid) {
      this.markFormGroupTouched(this.registerForm);
      return;
    }


    if (this.registerForm.valid
      && this.registerForm.get('Password')?.value === this.registerForm.get('ConfirmPassword')?.value) {

      const user: RegisterUserDTO = {
        AdminRegister: this.adminRegister,
        Name: this.registerForm.get('Name')?.value,
        LastName: this.registerForm.get('LastName')?.value,
        Email: this.registerForm.get('Email')?.value,
        Password: this.registerForm.get('Password')?.value,
        ConfirmPassword: this.registerForm.get('ConfirmPassword')?.value,
        Phone: this.registerForm.get('Phone')?.value,
        Document: parseInt(this.registerForm.get('Document')?.value),
        UserType: this.registerForm.get('UserType')?.value,
        ConsortiumID: this.registerForm.get('Consortium')?.value ? parseInt(this.registerForm.get('Consortium')?.value) : 0,
        DocumentType: this.registerForm.get('DocumentType')?.value
      };  
     
      this.loading = true;
      this.userService.CreateUser(user).subscribe({
        next: data => {
          if (data != null && data.success){
            Utils.success("El usuario se ha creado correctamente");
            this.loading = false;
            this.router.navigate(['/login'])
          }
          else{
            Utils.error(data.message);
            this.loading = false;
          }
        }
      })

    } else {
      Utils.error("Faltan campos por rellenar");
    }
  }

  isFieldInvalid(field: string): boolean {
    const control = this.registerForm.get(field);
    return control ? !control.valid && control.touched : false;
  }

  private markFormGroupTouched(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }

  Cancel(){
    this.router.navigate(['/login'])
  }

  LoadDocumentTypes() {
    this.userService.GetDocumentTypes().subscribe({
      next: data => {
        for (var item of data) {
          this.documentTypes.push({ Id: item.id, Name: item.name });
        }
      }
    })
  }

  LoadConsortiums() {
    this.consortiumService.GetConsortiums().subscribe({
      next: data => {
        for (var item of data) {
          this.consortiums.push({ Id: item.id, Name: item.name, Location: item.location });        
        }
      }
    })
  }

}
