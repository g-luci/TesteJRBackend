using apiToDo.DTO;
using apiToDo.Models;
using System.Collections.Generic;

namespace apiToDo.Repository
{
    //Interface que estabelece um contratro
    public interface ITarefaRepository
    {
        List<Tarefas> lstTarefas();
        void InserirTarefa(Tarefas tarefas);
        void DeletarTarefa(int id);

        void AtualizarTarefa(TarefaDTO tarefa);

        Tarefas BuscarTarefa(int id);
    }
}
