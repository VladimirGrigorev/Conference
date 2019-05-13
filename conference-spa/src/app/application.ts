export class Application{
    id:number;
    topic : string;
    authors : string;
    keywords : string;
    info  : string;
    sectionId : number;
    userId : number;
    sectionName: string; 
    conferenceName: string;
    applicationStatus: ApplicationStatus;
}

export enum ApplicationStatus{
    Pending,
    RejectedDesign,
    RejectedContent,
    Accepted
}

export class ApplicationStatusInfo{
    id:number;
    applicationStatus: ApplicationStatus;
}