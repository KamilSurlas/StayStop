import { HotelType } from "./hotel-type";
import { RoomResponseDto } from "./room-response";

export interface HotelResponseDto {
    hotelId: number;
    rooms: RoomResponseDto[];
    hotelType: HotelType;
    stars: number;
    country: string;
    city: string;
    street: string;
    zipCode: string;
    emailAddress: string;
    phoneNumber: string;
    name: string;
    description: string;
    coverImage: string;
    images: string[];
}