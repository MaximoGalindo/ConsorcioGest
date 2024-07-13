export class RegisterUserDTO {
  name: string = '';
  lastName: string = '';
  email: string = '';
  password: string = '';
  phone: string = '';
  document: number = 0;
  documentType: number = 0;
  consortiumID: number = 0;
  userType: string = '';
  adminRegister: boolean = false;
}
