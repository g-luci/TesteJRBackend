using apiToDo.DTO;
using apiToDo.Mappers;
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

        
        [HttpGet("lstTarefas")]
        public ActionResult lstTarefas()
        {
            try
            {
                //Lista contendo as tarefas como DTO
                var tarefaDTO = _trefaRepository.lstTarefas().Select(x => x.toDTO()).ToList();

                //Retorna a lista de tarefaDTO com o CODE 200
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

                //Lista contendo as tarefas como DTO
                var tarefaDTO = _trefaRepository.lstTarefas().Select(x => x.toDTO()).ToList();

                //Retorna a lista de tarefaDTO com o CODE 200
                return Ok(tarefaDTO);


            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpDelete("DeletarTarefa")]
        public ActionResult DeleteTask([FromQuery] int ID_TAREFA)
        {
            try
            {
                //Deleta a tarefa com base no Id passado
                _trefaRepository.DeletarTarefa(ID_TAREFA);

                //Lista contendo as tarefas como DTO
                var tarefaDTO = _trefaRepository.lstTarefas().Select(x => x.toDTO()).ToList();

                //Retorna a lista de tarefaDTO com o CODE 200
                return Ok(tarefaDTO);
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }

        [HttpPatch("AtualzarTarefa")]
        public ActionResult AtualizarTarefa([FromBody] TarefaDTO tarefa)
        {
            try
            {
                //Chama o metodo AtualizarTarefa e atualiza a tarefa com base na entidade passada
                _trefaRepository.AtualizarTarefa(tarefa);

                //Lista contendo as tarefas como DTO
                var tarefaDTO = _trefaRepository.lstTarefas().Select(x => x.toDTO()).ToList();

                //Retorna a lista de tarefaDTO com o CODE 200
                return Ok(tarefaDTO);
            }
            catch(Exception ex) 
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            } 
        }

        [HttpGet("BuscarTarefa")]
        public ActionResult BuscarTarefa([FromQuery] int ID_TAREFA)
        {
            try
            {
                //Busca a tarefa com base no Id e logo em seguida, transforma em um DTO
                var tarefaDTO = _trefaRepository.BuscarTarefa(ID_TAREFA).toDTO();

                //Retorna o tarefaDTO com o CODE 200
                return Ok(tarefaDTO);
            }
            catch (Exception ex) 
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }


        //Rota criada apenas para exemplificar o Authorize
        [Authorize]
        [HttpGet("Auth/lstTarefas")]
        public ActionResult lstTarefasAuth()
        {
            try
            {
                //Lista contendo as tarefas como DTO
                var tarefaDTO = _trefaRepository.lstTarefas().Select(x => x.toDTO()).ToList();

                //Retorna a lista de tarefaDTO com o CODE 200
                return Ok(tarefaDTO);
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { msg = $"Ocorreu um erro em sua API {ex.Message}" });
            }
        }
    }
}
