using System.ComponentModel.DataAnnotations;

namespace TPFinal.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public bool Status { get; set; }
        public int IdUsuarioCadastro { get; set; }
        public int IdUsuarioUpdate { get; set; }

    }
}


