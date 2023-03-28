using System.ComponentModel.DataAnnotations;

namespace FinalniTest.Models
{
    public class Paket
    {
        public int Id { get; set; }
        [Required]
        [StringLength(90, MinimumLength = 2)]
        public string Posiljalac { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 4)]
        public string Primalac { get; set; }

        [Required]
        [Range(0.1, 9.99)]
        public decimal Tezina { get; set; }
        [Required]
        [Range(250, 10000)]
        public int Postarina { get; set; }
        public int KurirId { get; set; }
        public Kurir Kurir { get; set; }
    }
}
