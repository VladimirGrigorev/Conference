import { Lecture } from "./lecture";

export class Section{
    id:number;
    //conferenceId:number;
    name:string;
    info:string;
    lectures:Lecture[]=[];
}