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
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _repository;

        public ServicoService(IServicoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Servico?> GetById(int id) => await _repository.GetById(id);

        public async Task<IEnumerable<Servico>> GetAll() => await _repository.GetAll();

        public async Task<Servico> Create(ServicoDTO dto)
        {
            var servico = new Servico
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco,
                DuracaoMinutos = dto.DuracaoMinutos,
                DataCadastro = DateTime.Now
            };

            servico.Id = await _repository.Create(servico);
            return servico;
        }

        public async Task Update(int id, ServicoDTO dto)
        {
            var servico = await _repository.GetById(id);
            if (servico == null) throw new KeyNotFoundException("Servico não encontrado");

            servico.Nome = dto.Nome;
            servico.Descricao = dto.Descricao;
            servico.Preco = dto.Preco;
            servico.DuracaoMinutos = dto.DuracaoMinutos;

            await _repository.Update(servico);
        }

        public async Task Delete(int id) => await _repository.Delete(id);
    }
}
