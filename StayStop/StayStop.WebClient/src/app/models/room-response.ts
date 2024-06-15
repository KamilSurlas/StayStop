import { RoomType } from "./room-type";

export interface RoomResponseDto {
    roomId: number;
    description: string;
    roomType: RoomType;
    coverImage: string;
    images: string[];
    isAvailable: boolean;
    priceForAdult: number;
    priceForChild: number;
}