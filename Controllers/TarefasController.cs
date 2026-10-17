using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyFlowAPI.Data;
using StudyFlowAPI.DTO;
using StudyFlowAPI.Models;

namespace StudyFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarefasController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var tarefas = await _context.Tarefas
                .Select(t => new TarefaResponseDTO
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Disciplina = t.Disciplina,
                    DataEntrega = t.DataEntrega,
                    Status = t.Status.ToString()
                })
                .ToListAsync();

            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa is null)
                return NotFound();

            var response = new TarefaResponseDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Disciplina = tarefa.Disciplina,
                DataEntrega = tarefa.DataEntrega,
                Status = tarefa.Status.ToString()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TarefaCreateDTO dto)
        {
            var tarefa = new Tarefa
            {
                Titulo = dto.Titulo,
                Disciplina = dto.Disciplina,
                DataEntrega = dto.DataEntrega,
                Status = StatusTarefa.Pendente
            };

            _context.Tarefas.Add(tarefa);

            await _context.SaveChangesAsync();

            var response = new TarefaResponseDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Disciplina = tarefa.Disciplina,
                DataEntrega = tarefa.DataEntrega,
                Status = tarefa.Status.ToString()
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = tarefa.Id },
                response
                );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(
            int id,
            [FromBody] TarefaUpdateDTO dto
        )
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa is null)
                return NotFound();

            if (!Enum.TryParse<StatusTarefa>(dto.Status, true, out var status))
                return BadRequest("O status informado é inválido.");

            tarefa.Titulo = dto.Titulo;
            tarefa.Disciplina = dto.Disciplina;
            tarefa.DataEntrega = dto.DataEntrega;
            tarefa.Status = status;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa is null)
                return NotFound();

            _context.Tarefas.Remove(tarefa);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
