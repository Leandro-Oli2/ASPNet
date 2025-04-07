using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace Exemplo4.Models
{
    [Table("maquina")]
    public class Maquina
    {
        [Key]
        [Column("id_maquina")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_maquina { get; set; } 

        [Column("tipo")]
        public string Tipo { get; set; } = string.Empty;

        [Column("velocidade")]
        public int Velocidade { get; set; }

        [Column("harddisk")]
        public int HardDisk { get; set; }

        [Column("placa_rede")]
        public int Placa { get; set; }

        [Column("memoria_ram")]
        public int Memoria { get; set; }

        [ForeignKey("Usuario")]
        [Column("fk_usuario")]
        public int FkUsuario {get; set;}
    }
}
