using apiToDo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace apiToDo.Models
{
    public class Tarefas
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        
        public Tarefas()
        {
        }

        public Tarefas(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

    }
}
