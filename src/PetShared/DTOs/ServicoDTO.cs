using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShared.DTOs
{
    public class ServicoDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        [DisplayName("Descrição do Serviço")]
        public string Descricao { get; set; } = string.Empty;

        [DisplayName("Preço em R$")]
        [Required(ErrorMessage = "Preço é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero")]
        public decimal Preco { get; set; }

        [DisplayName("Duração em Minutos")]
        [Required(ErrorMessage = "Duração é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Duração deve ser maior que zero")]
        public int DuracaoMinutos { get; set; }
    }
}
