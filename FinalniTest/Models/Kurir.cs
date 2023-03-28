using System;
using System.ComponentModel.DataAnnotations;

namespace FinalniTest.Models
{
    public class Kurir
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Ime { get; set; }
        [Required]
        [Range(1940, 2005)]
        public int Rodjenje { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Kurir kurir &&
                Id == kurir.Id &&
                Ime == kurir.Ime &&
                Rodjenje == kurir.Rodjenje;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Ime, Rodjenje);
        }

        public override string ToString()
        {
            return $"Kurir ID {Id}: {Ime}, god. rodjenja: {Rodjenje}";
        }
    }
}
