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
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepo;
        private readonly IProfissionalRepository _profissionalRepo;
        private readonly IServicoRepository _servicoRepo;

        public AgendamentoService(
            IAgendamentoRepository agendamentoRepo,
            IProfissionalRepository profissionalRepo,
            IServicoRepository servicoRepo)
        {
            _agendamentoRepo = agendamentoRepo;
            _profissionalRepo = profissionalRepo;
            _servicoRepo = servicoRepo;
        }

        public async Task<Agendamento> Agendar(AgendamentoDTO dto)
        {
            // Validações
            await ValidarDisponibilidade(dto.ProfissionalId, dto.DataHora);

            var profissional = await _profissionalRepo.GetById(dto.ProfissionalId)
                ?? throw new KeyNotFoundException("Profissional não encontrado");

            var servico = await _servicoRepo.GetById(dto.ServicoId)
                ?? throw new KeyNotFoundException("Serviço não encontrado");

            var agendamento = new Agendamento
            {
                ProfissionalId = dto.ProfissionalId,
                ServicoId = dto.ServicoId,
                DataHora = dto.DataHora,
                Observacoes = dto.Observacoes,
                Status = "Agendado"
            };

            agendamento.Id = await _agendamentoRepo.Create(agendamento);
            return agendamento;
        }

        public Task Cancelar(int id)
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<Agendamento>> ListarPorProfissional(int profissionalId, DateTime data)
        {
            throw new NotImplementedException();
        }

        public async Task ValidarDisponibilidade(int profissionalId, DateTime dataHora)
        {
            var agendamentos = await _agendamentoRepo.GetByProfissional(profissionalId, dataHora.Date);

            if (agendamentos.Any(a => Math.Abs((a.DataHora - dataHora).TotalMinutes) < 30))
            {
                throw new InvalidOperationException("Profissional já possui agendamento próximo a este horário");
            }
        }

        public async Task<Agendamento?> GetById(int id)
        {
            return await _agendamentoRepo.GetById(id);
        }

        // Implementar outros métodos
    }
}
