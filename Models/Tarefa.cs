using System.ComponentModel.DataAnnotations;

namespace StudyFlowAPI.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Descricao { get; set; } = string.Empty;

        public string Disciplina { get; set; } = string.Empty;
        public DateTime DataEntrega { get; set; }
        public ImportanciaTarefa Importancia { get; set; } = ImportanciaTarefa.Rotineiro;
        public StatusTarefa Status { get; set; }
    }
}
