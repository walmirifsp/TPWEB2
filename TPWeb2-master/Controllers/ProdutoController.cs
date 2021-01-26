using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TPFinal.Services;
using TPFinal.Models;

namespace TPFinal.Controllers
{
    [Route("api/Produtos")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly ILogger<ProdutoController> _logger;
        DevelopersContext _context;
        public ProdutoController(ILogger<ProdutoController> logger, DevelopersContext context)
        {
            _logger = logger;
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Produto>> index()
        {
            try
            {
                IAsyncEnumerable<Produto> produtosList = new ProdutoService(_context).GetList();
                var produtos = new List<Produto>();
                await foreach (Produto produto in produtosList)
                {
                    produtos.Add(produto);
                }
                return Json(produtos);
            }
            catch
            {
                return BadRequest(Json(new { error = "Bad Request - Database error" }));
            }
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Produto>> getById(int id)
        {
            try
            {
                Produto produto = await new ProdutoService(_context).FetchById(id);
                if (produto == null)
                    throw new Exception();
                return produto;
            }
            catch
            {
                return NotFound(
                  Json(new { error = $"Not Found - id {id}" }));

            }
        }
        [HttpPost]
        public async Task<ActionResult<Produto>> Add([FromBody] Produto produto)
        {
            try
            {
                await new ProdutoService(_context).Add(produto);
                return produto;
            }
            catch
            {
                return BadRequest(Json(new { error = "Bad Request - Database error" }));
            }
        }
        [HttpPut]
        public async Task<ActionResult<Produto>> Update([FromBody] Produto produto)
        {
            try
            {
                await new ProdutoService(_context).Update(produto);
                return produto;
            }
            catch
            {
                return BadRequest(Json(new { error = "Bad Request - Database error" }));
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Produto>> Remove(int produtoId)
        {

            if (produtoId == 0)
                return BadRequest();

            try
            {
                await new ProdutoService(_context).Remove(produtoId);
                return NoContent();
            }
            catch
            {
                return BadRequest(Json(new { error = "Bad Request - remove fails" }));
            }
        }
    }
}