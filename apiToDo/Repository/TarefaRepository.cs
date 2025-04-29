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
    }
}
