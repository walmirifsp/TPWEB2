using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;


namespace TPFinal.Models
{

    public class DevelopersContext : DbContext
    {
        public DevelopersContext(DbContextOptions<DevelopersContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
    }
}