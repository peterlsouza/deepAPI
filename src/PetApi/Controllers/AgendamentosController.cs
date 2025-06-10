using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PetBusiness.Interfaces;
using PetShared.DTOs;
using PetShared.Models;
using static Dapper.SqlMapper;
using System.Data;
using System.Net.NetworkInformation;
using System.Security.Cryptography.Xml;
using System;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentosController : ControllerBase
    {
        private readonly IAgendamentoService _service;

        public AgendamentosController(IAgendamentoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Agendamento>> Agendar(AgendamentoDTO dto)
        {
            try
            {
                var agendamento = await _service.Agendar(dto);
                return CreatedAtAction(
                    actionName: nameof(ObterPorId), // ← Alterado para o método existente
                    routeValues: new { id = agendamento.Id },
                    value: agendamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Adicione este novo método ↓
        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> ObterPorId(int id)
        {
            var agendamento = await _service.GetById(id); // Você precisará implementar GetById no serviço
            if (agendamento == null) return NotFound();
            return Ok(agendamento);
        }

        [HttpGet("profissional/{profissionalId}")]
        public async Task<ActionResult<IEnumerable<Agendamento>>> ListarPorProfissional(
            int profissionalId,
            [FromQuery] DateTime data)
        {
            return Ok(await _service.ListarPorProfissional(profissionalId, data));
        }

        [HttpPatch("{id}/cancelar")]
        public async Task<IActionResult> Cancelar(int id)
        {
            await _service.Cancelar(id);
            return NoContent();
        }
    }

}
