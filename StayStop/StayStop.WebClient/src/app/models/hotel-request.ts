import { HotelType } from "./hotel-type";

export interface HotelRequestDto {
    hotelType: HotelType | null;
    stars: number | null;
    country: string |null;
    city: string|null;
    street: string|null;
    zipCode: string|null;
    emailAddress: string|null;
    phoneNumber: string|null;
    name: string|null;
    description: string|null;
    coverImage:string|null;
    images:string[]|null;
}