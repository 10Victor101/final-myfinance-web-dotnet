using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace final_my_finance.Models
{
    public class TransacaoSemPlanoConta
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal Valor { get; set; }
        [StringLength(100)]
        public string Historico { get; set; }
        [Required]
        public int PlanoContaId { get; set; }
    }
}
