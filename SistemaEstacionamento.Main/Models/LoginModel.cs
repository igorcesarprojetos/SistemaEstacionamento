using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEstacionamento.Main.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        [DisplayName("Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string Senha { get; set; }


    }
}