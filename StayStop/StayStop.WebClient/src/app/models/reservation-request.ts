import { ReservationPositionRequestDto } from "./reservation-position-request";
import { ReservationStatus } from "./reservation-status";

export interface ReservationRequestDto {
    startDate: string;
    endDate: string;
    reservationPositions: ReservationPositionRequestDto[];
    reservationStatus: ReservationStatus;

}