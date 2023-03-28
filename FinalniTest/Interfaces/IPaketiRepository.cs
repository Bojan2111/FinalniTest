using FinalniTest.Models;
using FinalniTest.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace FinalniTest.Interfaces
{
    public interface IPaketiRepository
    {
        IQueryable<Paket> GetAll();
        Paket GetById(int id);
        void Add(Paket paket);
        void Update(Paket paket);
        void Delete(Paket paket);
        IQueryable<Paket> GetAllByTezina(TezineDTO tezine);
        IQueryable<Paket> GetAllByPrijem(string primalac);
    }
}
