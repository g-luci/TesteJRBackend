using apiToDo.Models;
using apiToDo.Repository;
using System.Linq;

namespace apiToDo.Seeder
{
    public class TarefaSeeder
    {
        private readonly ITarefaRepository _repository;

        public TarefaSeeder(ITarefaRepository repository)
        {
            _repository = repository;
        }

        //Metodo para popular a lista
        public void Popular()
        {
            //Verifica se a lista esta vazia
            if(!_repository.lstTarefas().Any())
            {
                //Adiona novas tarefas a lista
                _repository.InserirTarefa(new Tarefas { Id = 1, Descricao = "Fazer Compras" });
                _repository.InserirTarefa(new Tarefas { Id = 2, Descricao = "Fazer Atividad Faculdade" });
                _repository.InserirTarefa(new Tarefas { Id = 3, Descricao = "Subir Projeto de Teste no GitHub" });
            }
        }
    }
}
