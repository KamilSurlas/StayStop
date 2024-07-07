import { ReservationPositionResponseDto } from "./reservation-position-response";
import { ReservationStatus } from "./reservation-status";

export interface ReservationResponseDto {
    reservationId: number;
    startDate: string;
    endDate: string;
    price: number;
    reservationPositions: ReservationPositionResponseDto[];
    reservationStatus: ReservationStatus;
    userId: number;
}