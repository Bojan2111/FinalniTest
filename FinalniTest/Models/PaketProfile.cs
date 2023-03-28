using AutoMapper;
using FinalniTest.Models.DTO;

namespace FinalniTest.Models
{
    public class PaketProfile : Profile
    {
        public PaketProfile()
        {
            CreateMap<Paket, PaketDTO>();
        }
    }
}
