using PetShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetData.Interfaces
{
    public interface IAgendamentoRepository
    {
        Task<Agendamento?> GetById(int id);
        Task<IEnumerable<Agendamento>> GetAll();
        Task<IEnumerable<Agendamento>> GetByProfissional(int profissionalId, DateTime data);
        Task<int> Create(Agendamento agendamento);
        Task Update(Agendamento agendamento);
        Task Cancel(int id);
    }
}
