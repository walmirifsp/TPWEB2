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
    public class UsuarioService
    {
        DevelopersContext context;
        public UsuarioService(DevelopersContext contexto)
        {
            this.context = contexto;
        }
        public IAsyncEnumerable<Usuario> GetList()
        {
            return context.Usuarios.AsAsyncEnumerable<Usuario>();
        }
        public Task<int> Add(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            return context.SaveChangesAsync();

        }
        public ValueTask<Usuario> FetchById(int usuarioId)
        {
            return context.Usuarios.FindAsync(usuarioId);
        }
        public Task<int> Update(Usuario usuario)
        {
            context.Entry(usuario).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }
        public Task<int> Remove(int numero)
        {
            var usuario = context.Usuarios.Single(usuario => usuario.UsuarioId == numero);
            context.Usuarios.Remove(usuario);
            return context.SaveChangesAsync();
        }

        public Task<Usuario> Session(string nome, string senha)
        {
            return context.Usuarios.FirstAsync(u => u.Nome == nome && u.Senha == senha );
        }
    }
}