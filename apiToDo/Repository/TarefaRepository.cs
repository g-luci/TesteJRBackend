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
            catch (Exception ex)
            {
                throw ex;
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

        //Metodo para deletar uma tarefa com base no Id
        public void DeletarTarefa(int id)
        {
            try
            {
                //Variavel que armazena a tarefa alvo com base no Id
                var tarefaAlvo = _lstTarefas.FirstOrDefault(t => t.Id == id);

                //Verifica se existe o Id passado existe
                if (tarefaAlvo == null)
                {
                    //Caso ele não exista, uma execeção é disparada
                    throw new Exception($"O usuario esta tentando deletar a tarefa de codigo {id}");
                }
                else
                {
                    //Remove a tarefa alvo, passando a tarefa alvo armazenada na variavel response
                    _lstTarefas.Remove(tarefaAlvo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Metodo para atualizar uma tarefa com base em um tarefa passada
        public void AtualizarTarefa(TarefaDTO tarefaAtualizada)
        {
            try
            {
                //Variavel que armazena a tarefa alvo com base no Id
                var tarefaAlvo = _lstTarefas.FirstOrDefault(t => t.Id == tarefaAtualizada.ID_TAREFA);

                //Verifica se existe o Id passado existe
                if (tarefaAlvo == null)
                {
                    //Caso a tarefa não exista, uma execeção é disparada
                    throw new Exception($"O usuario esta tentando modificar uma tarefa inexistente");
                }
                else
                {
                    //Muda o atribuo da tarefa alvo com base na tarefa passada
                    tarefaAlvo.Descricao = tarefaAtualizada.DS_TAREFA;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Metodo que busca uma tarefa com base no Id
        public Tarefas BuscarTarefa(int id) 
        {
            try
            {
                //Variavel que armazena a tarefa alvo com base no Id
                var tarefaAlvo = _lstTarefas.FirstOrDefault(t => t.Id == id);

                //Verifica se existe o Id passado existe
                if (tarefaAlvo == null)
                {
                    //Caso ele não exista, uma execeção é disparada
                    throw new Exception($"O usuario esta tentando encotrar uma tarefa inexistente");
                }
                else
                {
                    //retorna a tarefa alvo
                    return tarefaAlvo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
