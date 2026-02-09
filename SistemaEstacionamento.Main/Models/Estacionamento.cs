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
        [DisplayName("Preço Inicial")]
        public decimal PrecoInicial { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayName("Preço da Hora")]
        public decimal PrecoHora { get; set; }

        [DisplayName("Quantidade de Horas")]
        public int? QuantidadeHoras { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayName("Valor Total")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [StringLength(7, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres.", MinimumLength = 7)]
        [DisplayName("Preço do Veículo")]
        public string PlacaVeiculo { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        [DisplayName("Modelo de Veículo")]
        public string ModeloVeiculo { get; set; }

        [DisplayName("Pago")]
        public bool Pago { get; set; }
    }
}
