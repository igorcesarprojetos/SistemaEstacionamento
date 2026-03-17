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

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayName("Preço Hora Adicional")]
        public decimal? PrecoHoraAdicional { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [DataType(DataType.Text)]
        [DisplayName("Data Entrada")]
        public DateTime DataEntrada { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Data Saída")]
        public DateTime? DataSaida { get; set; }

        [DisplayName("Quantidade de Horas")]
        public decimal? QuantidadeHoras { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [DisplayName("Valor Total")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [StringLength(7, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres.", MinimumLength = 7)]
        [DisplayName("Placa do Veículo")]
        public string PlacaVeiculo { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo precisa ter entre {2} e {1} caracteres.", MinimumLength = 4)]
        [DisplayName("Modelo de Veículo")]
        public string ModeloVeiculo { get; set; }

        [DisplayName("Pago")]
        public bool Pago { get; set; }
    }
}
