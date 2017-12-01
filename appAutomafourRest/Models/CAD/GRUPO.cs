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
    
    [Table("GRUPO")]
    public class GRUPO : IEntidadeBase
    {
        [Key]
        [Column("GRUPICOD")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        [Required]
        public string GRUPA60DESCR { get; set; }        
        [NotMapped]
        public string CEMP { get; set; }
        [NotMapped]
        public virtual dynamic Produtos { get; set; }
    }
}
