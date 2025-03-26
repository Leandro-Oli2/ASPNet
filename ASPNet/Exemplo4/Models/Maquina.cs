using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exemplo4.Models
{
    [Table("maquina")]
    public class Maquina
    {
        [Key]
        [Column("id_maquina")]
        public int Id_maquina { get; set; }  // Renomeado para manter consistência

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

        [Column("fk_usuario")]
        public int FkUsuario { get; set; }  


        [ForeignKey("FkUsuario")] 
        public virtual Usuario Usuario { get; set; }  
    }
}
