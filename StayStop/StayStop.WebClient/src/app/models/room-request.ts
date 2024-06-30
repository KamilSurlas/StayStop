import { RoomType } from "./room-type";

export interface RoomRequestDto {
    description: string;
    roomType: RoomType;
    coverImage: string;
    images: string[];
    isAvailable: boolean;
    priceForAdult: number;
    priceForChild: number;
    numberOfChildren: number;
    numberOfAdults: number;
}