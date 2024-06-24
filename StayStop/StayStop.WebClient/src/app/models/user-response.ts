import { HotelResponseDto } from "./hotel-response";
import { ReservationResponseDto } from "./reservation-response";

export interface UserResponseDto {
    userId: number;
    email: string;
    phoneNumber: string;
    name: string;
    lastName: string;
    userReservations: ReservationResponseDto[] | null;
    managedHotels: HotelResponseDto[] | null;
    ownedHotels: HotelResponseDto[] | null;
}