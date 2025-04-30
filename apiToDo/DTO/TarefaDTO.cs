using System.ComponentModel.DataAnnotations;

namespace apiToDo.DTO
{
    public class TarefaDTO
    {
        [Required]
        public int ID_TAREFA { get; set; }
        [Required]
        public string DS_TAREFA { get; set; }

    }
}
