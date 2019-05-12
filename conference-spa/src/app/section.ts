import { Lecture } from "./lecture";
import { UserInfo } from "./userInfo";

export class Section{
    id:number;
    //conferenceId:number;
    name:string;
    info:string;
    lectures:Lecture[]=[];
    experts:UserInfo[]=[];
}