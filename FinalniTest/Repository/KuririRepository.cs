using FinalniTest.Interfaces;
using FinalniTest.Models;
using FinalniTest.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FinalniTest.Repository
{
    public class KuririRepository : IKuririRepository
    {
        private readonly AppDbContext _context;

        public KuririRepository(AppDbContext context)
        {
            this._context = context;
        }

        public void Add(Kurir kurir)
        {
            _context.Kuriri.Add(kurir);
            _context.SaveChanges();
        }

        public void Delete(Kurir kurir)
        {
            _context.Kuriri.Remove(kurir);
            _context.SaveChanges();
        }

        public IQueryable<Kurir> GetAll()
        {
            return _context.Kuriri.OrderBy(k => k.Ime);
        }

        public IQueryable<Kurir> GetAllByIme(string ime)
        {
            return _context.Kuriri.Where(k => k.Ime.Contains(ime)).OrderByDescending(k => k.Rodjenje).OrderBy(k => k.Ime);
        }

        public IEnumerable<StanjeDTO> GetBrojPaketa()
        {
            return _context.Paketi.Include(p => p.Kurir).GroupBy(b => b.KurirId)
                .Select(r => new StanjeDTO
                {
                    KurirId = r.Key,
                    KurirIme = _context.Kuriri.Where(c => c.Id == r.Key).Select(a => a.Ime).Single(),
                    BrojPaketa = _context.Paketi.Where(a => a.KurirId == r.Key).Count()
                }).OrderByDescending(p => p.BrojPaketa).ToList();
        }

        public Kurir GetById(int id)
        {
            return _context.Kuriri.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<DostaveDTO> GetDostave(decimal granica)
        {
            return _context.Paketi.Include(p => p.Kurir).GroupBy(b => b.KurirId)
                .Select(r => new DostaveDTO
                {
                    KurirId = r.Key,
                    KurirIme = _context.Kuriri.Where(c => c.Id == r.Key).Select(a => a.Ime).Single(),
                    UkupnaTezina = _context.Paketi.Where(a => a.KurirId == r.Key).Select(a => a.Tezina).Sum()
                }).Where(p => p.UkupnaTezina < granica).OrderByDescending(p => p.KurirIme).ToList();
        }

        public void Update(Kurir kurir)
        {
            _context.Entry(kurir).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
