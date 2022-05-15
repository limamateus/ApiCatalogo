using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Nome { get; set; }
        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }
        [Required]
        [Column(TypeName ="decimal(10,2)")]
        public decimal Preco { get; set; } //

        [Required]
        [StringLength(300)]
        public string? ImagemURL { get; set; }


        public float Estoque { get; set; }

        public DateTime DataCadastro { get; set; }


        public int CategoriaId { get; set; } // metodo 1 para muitos, ou 1 para n, sei lá acho que é assim que fala
        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
