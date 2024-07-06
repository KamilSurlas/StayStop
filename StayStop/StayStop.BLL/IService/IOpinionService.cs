using StayStop.BLL.Dtos.Opinion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.IService
{
    public interface IOpinionService
    {
        int Create(int reservationId, OpinionRequestDto opinionDto);
        void UpdateByOpinionId(int reservationId, OpinionUpdateRequestDto opinionDto);
        OpinionResponseDto GetByReservationId(int reservationId);
        void Delete(int reservationId);
        List<OpinionResponseDto> GetUserOpinions();
        void DeleteByOpinionId(int opinionId);
        OpinionResponseDto GetByOpinionId(int opinionId);
    }
}
