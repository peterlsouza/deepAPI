using PetShared.DTOs;
using PetShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetBusiness.Interfaces
{
    public interface IAgendamentoService
    {
        Task<Agendamento> Agendar(AgendamentoDTO dto);
        Task Cancelar(int id);
        Task<IEnumerable<Agendamento>> ListarPorProfissional(int profissionalId, DateTime data);
        Task ValidarDisponibilidade(int profissionalId, DateTime dataHora);
        Task<Agendamento?> GetById(int id);
    }
}
