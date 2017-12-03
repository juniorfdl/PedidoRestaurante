namespace Models.Cadastros
{
    using Infra.Base.Interface;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    [Table("CONFIG_RESTAURANTE")]
    public class CONFIG_RESTAURANTE : IEntidadeBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID")]
        public int id { get; set; }
        [Required]
        public int? MESA { get; set; }
        public string OBS { get; set; }
        [NotMapped]
        public string CEMP { get; set; }
        [NotMapped]
        public virtual dynamic Produtos { get; set; }
    }
}
