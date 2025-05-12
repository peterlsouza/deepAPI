using PetShared.DTOs;
using PetShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetBusiness.Interfaces
{
    public interface IProfissionalService
    {
        Task<Profissional?> GetById(int id);
        Task<IEnumerable<Profissional>> GetAll();
        Task<Profissional> Create(ProfissionalDTO dto);
        Task Update(int id, ProfissionalDTO dto);
        Task Delete(int id);
    }
}
