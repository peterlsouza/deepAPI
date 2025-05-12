using PetBusiness.Interfaces;
using PetData.Interfaces;
using PetShared.DTOs;
using PetShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetBusiness.Services
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly IProfissionalRepository _repository;

        public ProfissionalService(IProfissionalRepository repository)
        {
            _repository = repository;
        }

        public async Task<Profissional?> GetById(int id) => await _repository.GetById(id);

        public async Task<IEnumerable<Profissional>> GetAll() => await _repository.GetAll();

        public async Task<Profissional> Create(ProfissionalDTO dto)
        {
            var profissional = new Profissional
            {
                Nome = dto.Nome,
                Especialidade = dto.Especialidade,
                Telefone = dto.Telefone,
                Email = dto.Email,
                DataCadastro = DateTime.Now
            };

            profissional.Id = await _repository.Create(profissional);
            return profissional;
        }

        public async Task Update(int id, ProfissionalDTO dto)
        {
            var profissional = await _repository.GetById(id);
            if (profissional == null) throw new KeyNotFoundException("Profissional não encontrado");

            profissional.Nome = dto.Nome;
            profissional.Especialidade = dto.Especialidade;
            profissional.Telefone = dto.Telefone;
            profissional.Email = dto.Email;

            await _repository.Update(profissional);
        }

        public async Task Delete(int id) => await _repository.Delete(id);
    }
}
