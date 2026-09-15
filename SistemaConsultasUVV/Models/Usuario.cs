using System.ComponentModel.DataAnnotations;

namespace GestaoConsultasUVV.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode passar de 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Relacionamento: Um usuário pode ter VÁRIAS consultas
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    }
}