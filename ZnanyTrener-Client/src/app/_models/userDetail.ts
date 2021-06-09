import { CertificateToAdd } from "./certificateToAdd";

export interface UserDetail {
    id: number;
    userName: string;
    firstName: string;
    lastName: string;
    description: string;
    specialization: string;
    phoneNumber: string;
    city: string;
    email: string;
    role: string;
    token: string;
    certificates: CertificateToAdd[];
    photoUrl: string;
    photoCloudinaryPublicId: string;

}
