using apiToDo.DTO;
using apiToDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apiToDo.Repository
{
    //Repository responsavel pela logica necessaria para acessar, inserir, atualizar ou remover dados
    public class TarefaRepository : ITarefaRepository
    {
        //Criação da entidade em forma de lista
        private readonly List<Tarefas> _lstTarefas = new List<Tarefas>();

        //Metodo que lista todas as tarefas
        public List<Tarefas> lstTarefas()
        {
            try
            {
                //retornar a lista
                return _lstTarefas;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Metodo para inserir uma nova tarefa na lista de tarefas
        public void InserirTarefa(Tarefas tarefas)
        {
            try
            {
                //Adiciona a tarefa na lista de tarefa
                _lstTarefas.Add(tarefas);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletarTarefa(int id)
        {
            try
            {
                //Variavel que armazena a tarefa alvo com base no Id
                var response = _lstTarefas.FirstOrDefault(t => t.Id == id);

                //Remove a tarefa alvo, passando a tarefa alvo armazenada na variavel response
                _lstTarefas.Remove(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
