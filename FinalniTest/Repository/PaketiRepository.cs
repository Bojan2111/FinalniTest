using FinalniTest.Interfaces;
using FinalniTest.Models;
using FinalniTest.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinalniTest.Repository
{
    public class PaketiRepository : IPaketiRepository
    {
        private readonly AppDbContext _context;

        public PaketiRepository(AppDbContext context)
        {
            this._context = context;
        }

        public void Add(Paket paket)
        {
            _context.Paketi.Add(paket);
            _context.SaveChanges();
        }

        public IQueryable<Paket> GetAll()
        {
            return _context.Paketi.OrderBy(o => o.Tezina);
        }

        public Paket GetById(int id)
        {
            return _context.Paketi.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Paket paket)
        {
            _context.Entry(paket).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Paket paket)
        {
            _context.Paketi.Remove(paket);
            _context.SaveChanges();
        }

        public IQueryable<Paket> GetAllByPrijem(string primalac)
        {
            return _context.Paketi.Where(c => c.Primalac == primalac).OrderBy(c => c.Postarina);
        }

        //public IQueryable<Paket> GetAllByKurirIme(int id)
        //{
        //    return _context.Paketi.Include(c => c.Kurir).Where(c => c.Kurir.Id == id);
        //}

        public IQueryable<Paket> GetAllByTezina(TezineDTO tezina)
        {
            return _context.Paketi.Where(x => x.Tezina > tezina.Najmanje && x.Tezina < tezina.Najvise).OrderByDescending(x => x.Tezina);
        }

        //public List<SearchDTO> GetSearch()
        //{
        //    return _context.Paketi.GroupBy(p => p.KurirId).Select(s =>
        //        new SearchDTO()
        //        {
        //            KurirIme = _context.Kuriri.Where(m => m.Id == s.Key).Select(k => k.Ime).Single(),
        //            PostarinaProsek = (decimal)s.Average(x => x.Postarina)
        //        }
        //        ).OrderBy(x => x.PostarinaProsek).ToList();
        //}
    }
}
