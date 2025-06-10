using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShared.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public int ProfissionalId { get; set; }
        public int ServicoId { get; set; }
        public DateTime DataHora { get; set; }
        public string? Observacoes { get; set; }
        public string Status { get; set; } = "Agendado"; // Agendado/Cancelado/Concluído
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Propriedades de navegação (opcional)
        public Profissional? Profissional { get; set; }
        public Servico? Servico { get; set; }
    }
}
