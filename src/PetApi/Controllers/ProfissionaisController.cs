using Microsoft.AspNetCore.Mvc;
using PetBusiness.Interfaces;
using PetShared.DTOs;
using PetShared.Models;

namespace PetApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfissionaisController : ControllerBase
    {
        private readonly IProfissionalService _service;

        public ProfissionaisController(IProfissionalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profissional>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profissional>> GetById(int id)
        {
            var profissional = await _service.GetById(id);
            if (profissional == null) return NotFound();
            return Ok(profissional);
        }

        [HttpPost]
        public async Task<ActionResult<Profissional>> Create(ProfissionalDTO dto)
        {
            var profissional = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = profissional.Id }, profissional);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProfissionalDTO dto)
        {
            await _service.Update(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}
