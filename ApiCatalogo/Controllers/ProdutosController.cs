using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly AppDBContext? _context;

        public ProdutosController(AppDBContext? context)
        {
            _context = context;
        }


        [HttpGet]
        // Como o IEnumerable vai me trazer apenas uma lista da minha table, não vou poder usar os status code
        // Por isso que temos que usar ActionResult, que vai permitir o uso do NotFound ou BadRiquest etec.
        // IEnumerable vai me trazer um lista, da minha Model de referencia Produtos e o Get é nome do Meu metodo
        public ActionResult<IEnumerable<Produto>>? Get()  // Metodo de retorno de lista
        {
            var produtos = _context.Produtos.Include(c => c.Categoria).ToList();  // Crio um varia para definir um consulta no banco

            if(produtos is null) // Verificando se está nulo e retornando erro 404
            {
                return NotFound("Produtos não encontrados");
            }

            return Ok(produtos); // Retorno 200

        }

        [HttpGet("{id:int}", Name = "ObterProduto")] // Verbo Get está sendo usado para trazer um informação, o paramentro dentro dos ()
                                // tem que ser id do tipo inteiro.
        public ActionResult<Produto> Get(int id) // Esse metodo Get vai me retorno um produto só, que vai ser definido atravez de id
        {   // Essa consulta Linq vai me trazer a primeira informção que atender ao paramentros 
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if(produto == null) // Condição se o valor do produto é null e retornondo um 404 com uma mensagem
            {
                return NotFound("Produto não encontrado");
            }
            // Retorno  do produto.
            return produto;
        }

        [HttpPost] // Verbo de Criação

        public ActionResult<Produto> Post(Produto produto) // 
        {
            if(produto is null) // Verifico se o produto é nulo
            //
            {
               return BadRequest(); // Retorno numero 400
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId},produto);
        }



        [HttpPut("{id:int}")] // Verbo de Update com um parametro predefinido

        public ActionResult Put(int id, Produto produto) // Metodo Put server para autualizar.
        {
            if(id != produto.ProdutoId) // Condição que comparação, onde eu difino que se o Id que for passado for diferente vai retornar Erro 404
            {
                return BadRequest(); // Retorno Erro 400
            }

            _context.Entry(produto).State = EntityState.Modified; // Estou definindo que o estado do produto vai ser modificado

            _context.SaveChanges();

            return Ok(produto); // Retorno Status 200 com dados que foi modificado, obs posso enviar um 204 NoContent(), mas o melhor é 200
        }


        [HttpDelete("{id:int}")] // Verbo de exclusão, passando um paramentro 

        public ActionResult Delete(int id) // Difino um id para deletar
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id); // consulta ling que vai procurar o primeiro produto que atender ao paramentro
            // var produto = _context.Produtos.Find(id); // Posso essar essa consulta, mas ela vai buscar primeiro na memoria 


            if(produto == null) // verifico se o produto é nulo 
            {
                return NotFound("Produto não encontrado"); // Caso for eu retorno um erro 404
            }

            _context.Remove(produto); // Removo o produto

            _context.SaveChanges(); // Percisto a remoção no banco 

            return Ok(produto); // e retorno status 200 com a informação do produto deletado.
        }
    }
}
