namespace StudyFlowAPI.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;
        public DateTime DataEntrega { get; set; }
        public StatusTarefa Status { get; set; }
    }
}
