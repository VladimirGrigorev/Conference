export class CheckedStatus {
    Result: string;
    IsOk: boolean;
    Warnings: Item[];
    Errors: Item[];
}

export class Item{
    Paragraph: string;
    Comment: string;
}