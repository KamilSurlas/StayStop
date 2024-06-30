import { ReservationPositionRequestDto } from "./reservation-position-request";

export interface ReservationRequestDto {
    startDate: string;
    endDate: string;
    reservationPositions: ReservationPositionRequestDto[];
}