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
                .AsNoTracking()
                .OrderBy(t => t.Status)
                .ThenBy(t => t.DataEntrega)
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
            var tarefa = await _context.Tarefas
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarefa is null)
                return NotFound(new ProblemDetails
                {
                    Title = "Tarefa não encontrada",
                    Detail = $"Não existe uma tarefa cadastrada com o ID {id}.",
                    Status = StatusCodes.Status404NotFound
                });

            return Ok(CriarResposta(tarefa));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TarefaCreateDTO dto)
        {
            var erroValidacao = ValidarDados(dto.Titulo, dto.Disciplina, dto.DataEntrega);

            if (erroValidacao is not null)
                return BadRequest(erroValidacao);

            var tarefa = new Tarefa
            {
                Titulo = dto.Titulo.Trim(),
                Disciplina = dto.Disciplina.Trim(),
                DataEntrega = dto.DataEntrega.Date,
                Status = StatusTarefa.Pendente
            };

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            var response = CriarResposta(tarefa);

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
            var erroValidacao = ValidarDados(dto.Titulo, dto.Disciplina, dto.DataEntrega);

            if (erroValidacao is not null)
                return BadRequest(erroValidacao);

            if (!Enum.TryParse<StatusTarefa>(dto.Status, true, out var status))
                return BadRequest("O status informado é inválido.");

            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa is null)
                return NotFound(new ProblemDetails
                {
                    Title = "Tarefa não encontrada",
                    Detail = $"Não existe uma tarefa cadastrada com o ID {id}.",
                    Status = StatusCodes.Status404NotFound
                });

            tarefa.Titulo = dto.Titulo.Trim();
            tarefa.Disciplina = dto.Disciplina.Trim();
            tarefa.DataEntrega = dto.DataEntrega.Date;
            tarefa.Status = status;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa is null)
                return NotFound(new ProblemDetails
                {
                    Title = "Tarefa não encontrada",
                    Detail = $"Não existe uma tarefa cadastrada com o ID {id}.",
                    Status = StatusCodes.Status404NotFound
                });

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static TarefaResponseDTO CriarResposta(Tarefa tarefa)
        {
            return new TarefaResponseDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Disciplina = tarefa.Disciplina,
                DataEntrega = tarefa.DataEntrega,
                Status = tarefa.Status.ToString()
            };
        }

        private static string? ValidarDados(
            string titulo,
            string disciplina,
            DateTime dataEntrega
        )
        {
            if (string.IsNullOrWhiteSpace(titulo))
                return "Informe o título da tarefa.";

            if (string.IsNullOrWhiteSpace(disciplina))
                return "Informe a disciplina.";

            if (dataEntrega == default)
                return "Informe uma data de entrega válida.";

            return null;
        }
    }
}
