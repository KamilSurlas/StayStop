import { RoomType } from "./room-type";


export interface RoomUpdateRequestDto {
    description: string | null;
    roomType: RoomType | null;
    numberOfChildren: number | null;
    numberOfAdults: number | null;
    isAvailable: boolean | null;
    priceForAdult: number | null;
    priceForChild: number | null;
    coverImage: string|null;
    images:string[]|null;
}