import { HotelResponseDto } from "./hotel-response";

export interface OpinionResponseDto {
    opinionId: number;
    hotel: HotelResponseDto;
    mark: number;
    userOpinion: string;
    details: string;
    addedOn: Date;
}