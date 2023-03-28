using System;

namespace FinalniTest.Models.DTO
{
    public class PaketDTO
    {
        public int Id { get; set; }
        public string Posiljalac { get; set; }
        public string Primalac { get; set; }
        public decimal Tezina { get; set; }
        public int Postarina { get; set; }
        public string KurirIme { get; set; }

        public override bool Equals(object obj)
        {
            return obj is PaketDTO paket &&
                Id == paket.Id &&
                Posiljalac == paket.Posiljalac &&
                Primalac == paket.Primalac &&
                Tezina == paket.Tezina &&
                Postarina == paket.Postarina &&
                KurirIme == paket.KurirIme;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Posiljalac, Primalac, Tezina, Postarina, KurirIme);
        }
    }
}
