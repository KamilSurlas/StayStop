export interface UpdateAccountModel {
    emailAddress: string | null;
    currentPassword: string | null;
    password: string | null;
    confirmedPassword: string | null;
    phoneNumber: string | null;
    name: string | null;
    lastName: string | null;
}