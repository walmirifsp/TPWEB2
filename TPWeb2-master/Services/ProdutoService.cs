using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TPFinal.Models;

namespace TPFinal.Services
{
    public class ProdutoService
    {
        DevelopersContext context;
        public ProdutoService(DevelopersContext contexto)
        {
            this.context = contexto;
        }
        public IAsyncEnumerable<Produto> GetList()
        {
            return context.Produtos.AsAsyncEnumerable<Produto>();
        }
        public Task<int> Add(Produto produto)
        {
            context.Produtos.Add(produto);
            return context.SaveChangesAsync();

        }
        public ValueTask<Produto> FetchById(int produtoId)
        {
            return context.Produtos.FindAsync(produtoId);
        }
        public Task<int> Update(Produto produto)
        {
            context.Entry(produto).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }
        public Task<int> Remove(int numero)
        {
            var produto = context.Produtos.Single(produto => produto.ProdutoId == numero);
            context.Produtos.Remove(produto);
            return context.SaveChangesAsync();
        }
    }
}