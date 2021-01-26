using System;
using System.ComponentModel.DataAnnotations;

namespace TPFinal.Models

{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public bool Status { get; set; }
    }
}


