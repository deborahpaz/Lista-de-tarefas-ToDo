using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace ListaToDo.Models
{
    public class Tarefa
    {
        public int TarefaId { get; set; }
        [Required]
        public string? NomeTarefa { get; set; }
        public Boolean StatusTarefa { get; set; } = false;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataInicioTarefa { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataFinalTarefa { get; set; }

    }
}
