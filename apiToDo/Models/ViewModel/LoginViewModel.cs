using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace apiToDo.Models.ViewModel
{
    //Classe usada apenas como ViewModel
    public class LoginViewModel
    {
        [Required]
        [SwaggerSchema(Description = "Usar: admin")]
        public string Username { get; set; }
        [Required]
        [SwaggerSchema(Description = "Usar: 123")]
        public string Userpassword { get; set; }
    }
}
