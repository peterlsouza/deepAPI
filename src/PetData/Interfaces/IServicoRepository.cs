using PetShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetData.Interfaces
{
    public interface IServicoRepository
    {
        Task<Servico?> GetById(int id);
        Task<IEnumerable<Servico>> GetAll();
        Task<int> Create(Servico servico);
        Task Update(Servico servico);
        Task Delete(int id);
    }
}
