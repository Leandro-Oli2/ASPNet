using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace Exemplo4.Models
{
    [Table("software")]
    public class Software
    {
        [Key]
        [Column("id_software")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_soft { get; set; }

        [Column("produto")]
        public string Produto { get; set; } = string.Empty;

        [Column("harddisk")]
        public int HardDisk { get; set; }

        [Column("memoria_ram")]
        public int MemoriaRam { get; set; }

        // Chave estrangeira para a Maquina
        [Column("fk_maquina")]
        public int FkMaquina { get; set; }

        // Definindo o relacionamento com a Maquina
        [ForeignKey("FkMaquina")]
        public virtual Maquina Maquina { get; set; }
    }
}