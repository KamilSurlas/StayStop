import { ReservationPositionResponseDto } from "./reservation-position-response";

export interface ReservationResponseDto {
    reservationId: number;
    startDate: string;
    endDate: string;
    price: number;
    reservationPositions: ReservationPositionResponseDto[];
}