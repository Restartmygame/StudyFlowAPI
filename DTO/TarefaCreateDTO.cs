using System.ComponentModel.DataAnnotations;

namespace StudyFlowAPI.DTO
{
    public class TarefaCreateDTO
    {
        [Required(ErrorMessage = "Informe o título da tarefa.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a disciplina.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "A disciplina deve ter entre 2 e 60 caracteres.")]
        public string Disciplina { get; set; } = string.Empty;

        public DateTime DataEntrega { get; set; }
    }
}
