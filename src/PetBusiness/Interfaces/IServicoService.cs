using PetShared.DTOs;
using PetShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetBusiness.Interfaces
{
    public interface IServicoService
    {
        Task<Servico?> GetById(int id);
        Task<IEnumerable<Servico>> GetAll();
        Task<Servico> Create(ServicoDTO dto);
        Task Update(int id, ServicoDTO dto);
        Task Delete(int id);
    }
}
