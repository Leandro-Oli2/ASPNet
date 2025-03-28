using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Exemplo4.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; } 

        [Column("password")]
        public string Senha { get; set; } = string.Empty;

        [Column("nome_usuario")]
        public string Nome { get; set; } = string.Empty;

        [Column("ramal")]
        public int Ramal { get; set; }  

        [Column("especialidade")]
        public string Especialidade { get; set; } = string.Empty;
    }
}