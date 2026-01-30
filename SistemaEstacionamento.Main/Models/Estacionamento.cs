using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaEstacionamento.Main.Models
{
    [Table("Estacionamento")]
    public class Estacionamento
    {
        [Key]
        [Column("Id_Estacionamento")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecoInicial { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]       
        public decimal PrecoHora { get; set; }
        
        public int? QuantidadeHoras { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]        
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [StringLength(7, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres.", MinimumLength = 7)]        
        public string PlacaVeiculo { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]        
        public string ModeloVeiculo { get; set; }

        public bool Pago { get; set; }
    }
}
