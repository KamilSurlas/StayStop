import { RoomResponseDto } from "./room-response";

export interface ReservationPositionResponseDto {
    reservationPositionId: number;
    numberOfAdults: number;
    numberOfChildren: number;
    price: number;
    amount: number;
    room: RoomResponseDto;
}