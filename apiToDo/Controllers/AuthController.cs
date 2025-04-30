using apiToDo.Models.ViewModel;
using apiToDo.Services;
using Microsoft.AspNetCore.Mvc;

namespace apiToDo.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("Autenticacao")]
        public ActionResult Auth([FromQuery] LoginViewModel login)
        {
            //Verfica se o usernmae e password são validos
            if (login.Username == "admin" && login.Userpassword == "123")
            {
                //Gera um token utilizando o metodo GeneretaToken
                var token = TokenService.GenerateToken(new Models.Tarefas());

                //Retorna o token
                return Ok(token);
            }

            //Se as credenciais forem inválidas, retorna um CODE 400
            return BadRequest("username or password invalid");
        }
    }
}
