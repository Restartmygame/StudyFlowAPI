namespace StudyFlowAPI.DTO
{
    public class TarefaCreateDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;
        public DateTime DataEntrega { get; set; }
    }
}
