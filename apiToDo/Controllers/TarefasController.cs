using apiToDo.DTO;
using apiToDo.Models;
using apiToDo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace apiToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaRepository _trefaRepository;

        public TarefasController(ITarefaRepository trefaRepository)
        {
            _trefaRepository = trefaRepository;
        }

        [Authorize]
        [HttpGet("lstTarefas")]
        public ActionResult lstTarefas()
        {
            try
            {
                var tarefaDTO = _trefaRepository.lstTarefas().Select(x => new TarefaDTO
                {
                   ID_TAREFA = x.Id,
                   DS_TAREFA = x.Descricao
                }).ToList();

                //Retorna a lista de tarefas com o CODE 200
                return Ok(tarefaDTO);
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}"});
            }
        }

        [HttpPost("InserirTarefas")]
        public ActionResult InserirTarefas([FromBody] TarefaDTO Request)
        {
            try
            {
                //Cria uma nova tarefa
                var novaTarefa = new Tarefas
                {
                    Id = Request.ID_TAREFA,
                    Descricao = Request.DS_TAREFA
                };

                //Inseri a nova tarefa
                _trefaRepository.InserirTarefa(novaTarefa);

                //Retorna a lista de tarefas com o CODE 200
                return Ok(_trefaRepository.lstTarefas());


            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpPost("DeletarTarefa")]
        public ActionResult DeleteTask([FromQuery] int ID_TAREFA)
        {
            try
            {
                //Deleta a tarefa com base no Id passado
                _trefaRepository.DeletarTarefa(ID_TAREFA);

                //Retorna a lista de tarefas com o CODE 200
                return Ok(_trefaRepository.lstTarefas());
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }
    }
}
