using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        [Required]
        public string Cep { get; set; }
        public string? Rua { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? Uf { get; set; }
    }
}
