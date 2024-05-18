export class UserModelDTO {
    id:number = 0
    document: number = 0;
    name: string = '';
    condominium: string = '';
    email: string = '';
    phone: string = '';
    profile: ProfileModel | null = null;
    property: string = '';
    state: StateModel = new StateModel();
}

class ProfileModel {
    id: number = 0;
    name: string = '';
}

class StateModel {
    id: number = 0;
    name: string = '';
}
