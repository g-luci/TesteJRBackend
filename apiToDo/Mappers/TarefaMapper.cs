using apiToDo.DTO;
using apiToDo.Models;

namespace apiToDo.Mappers
{
    public static class TarefaMapper
    {
        //metodo de extesão que retorna um DTO com base na entidade
        public static TarefaDTO toDTO(this Tarefas tarefa)
        {
            //Retorna um DTO com base nos atributos da entidade
            return new TarefaDTO
            {
                ID_TAREFA = tarefa.Id, 
                DS_TAREFA = tarefa.Descricao
            };
        }
    }
}
