using PetShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetData.Interfaces
{
    public interface IProfissionalRepository
    {
        Task<Profissional?> GetById(int id);
        Task<IEnumerable<Profissional>> GetAll();
        Task<int> Create(Profissional profissional);
        Task Update(Profissional profissional);
        Task Delete(int id);
    }
}
