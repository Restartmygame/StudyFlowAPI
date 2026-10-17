namespace StudyFlowAPI.DTO
{
    public class TarefaResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Disciplina { get; set; } = string.Empty;
        public DateTime DataEntrega { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
