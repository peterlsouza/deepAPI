using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShared.DTOs
{
    public class AgendamentoDTO
    {
        [Required(ErrorMessage = "Profissional é obrigatório")]
        public int ProfissionalId { get; set; }

        [Required(ErrorMessage = "Serviço é obrigatório")]
        public int ServicoId { get; set; }

        [Required(ErrorMessage = "Data/hora é obrigatória")]
        [FutureDate(ErrorMessage = "Data deve ser futura")]
        public DateTime DataHora { get; set; }

        public string? Observacoes { get; set; }
    }

    // Validação customizada para data futura
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is DateTime date && date > DateTime.Now;
        }
    }
}
