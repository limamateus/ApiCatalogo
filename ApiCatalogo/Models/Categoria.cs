using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Models
{
    public class Categoria
    {   [Key]
        public int CategoriaId { get; set; }

        
        public string? Nome { get; set; }


        public ICollection<Produto>? Produtos { get; set; } // Metodo de muitos para 1, ou seja , eu posso ter vairios produtos dentro de um categoria.



    }
}
