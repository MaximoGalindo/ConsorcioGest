export class UserModel {
    id: number = 0;
    name: string = '';
    lastName: string = '';
    phone?: string;
    email: string = '';
    isOwner: boolean = false;
    isOcupant: boolean = false;
    idCondominium?: number;
    profile: ProfileModel = new ProfileModel();
    userState: StateModel = new StateModel();
    idDocumentType?: number;
    document: number = 0;
    token: string = '';
}

export class ProfileModel {
    id: number = 0;
    name: string = '';
}

export class StateModel {
    id: number = 0;
    name: string = '';
}
