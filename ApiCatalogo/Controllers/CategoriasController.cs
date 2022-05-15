using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        private readonly AppDBContext _context;

        public CategoriasController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet] // Verbo Get
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categoria = _context.Categorias.ToList(); // Consulta linq onde vai me trazer todas as categorias

                if (categoria is null) // Verifico se é nulo 
                {
                    return NotFound("Categorias não encontradas..."); // Retorno 404 e uma mensagem
                }
                return Ok(categoria); // Retorno 200 com as informações da categoria consutada
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema  ao tratar  a sua solicitação");
            }
           
        }

        [HttpGet("{id:int}")] // Verbo de busca, com um paramentro de busca 
        public ActionResult Get(int id)
        {
            try
            {
                // consulta linq que vai retornar a primeira categoria encontrada incluindo os produtos 
                var categoria = _context.Categorias.Include(p => p.Produtos).FirstOrDefault(c => c.CategoriaId == id);

                if (categoria is null) // Verificaçao da categoria 
                {

                    return NotFound("Categoria não localizada"); // retorno 404 com uma mensagem
                }

                return Ok(categoria); // retorno 200 com informação da categoria 

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Um problema ocorreu ao tratar sua solicitação");
            }
           
        }

        [HttpGet("produtos")] // Verbo de busca, com um outra rota, para não dar problema em outro get
        public ActionResult<IEnumerable<Categoria>> GetComProdutos() // Metodo de busca onde vai me retornar uma lista
        {

            try
            {
                var categoria = _context.Categorias.Include(p => p.Produtos).ToList(); // Consulta linq onde vai me trazer todas as categorias com os produtos

                if (categoria is null) // Verifico se a categoria é nula
                {
                    return NotFound("Categorias não encontradas..."); // retorno 404 com uma mensagem
                }
                return Ok(categoria); // Retorno 200 com a informação da categoria passada
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Um problema ocorreu ao tratar sua solicitação");
            }
           
        }

        [HttpPost] // Verbo de criação

        public ActionResult Post(Categoria categoria) //
        {
            try
            {
                if (categoria is null) // Verifico as informações 
                {
                    return BadRequest(); // retorno 400
                }

                _context.Categorias.Add(categoria); // Salvo a categoria 
                _context.SaveChanges(); // percisto no banco

                return Ok(categoria); // retorno 200 com informação da categoria cadastrada.

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Um problema ocorreu ao tratar sua solicitação");
            }
           
        }


        [HttpPut("{id:int}")] // Verbo de atualizaçao sempre deve ter um paramentro 

         public ActionResult Put(int id,Categoria categoria)
        {
            if(id != categoria.CategoriaId)
            {
                return NotFound("Categoria não localizada...");
            }
            if(categoria is null)
            {
                return NotFound("Categoria não localizada...");
            }
            _context.Entry(categoria).State = EntityState.Modified;

            _context.SaveChanges();

            return Ok(categoria);
        }


        [HttpDelete("{id:int}")]

        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não localizada");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}
