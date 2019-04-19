import { UserInfo } from "./userInfo";


export class Lecture {
    id: number;
    //sectionId: number;
    topic: string;
    info: string;

    speakers:UserInfo[]=[];
}