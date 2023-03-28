using FinalniTest.Models;
using FinalniTest.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace FinalniTest.Interfaces
{
    public interface IKuririRepository
    {
        IQueryable<Kurir> GetAll();
        Kurir GetById(int id);
        IEnumerable<StanjeDTO> GetBrojPaketa();
        IEnumerable<DostaveDTO> GetDostave(decimal granica);
        IQueryable<Kurir> GetAllByIme(string ime);
        void Add(Kurir kurir);
        void Update(Kurir kurir);
        void Delete(Kurir kurir);
    }
}
