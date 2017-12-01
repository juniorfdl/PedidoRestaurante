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
    
    [Table("PRODUTO")]
    public class PRODUTO : IEntidadeBase
    {
        [Key]
        [Column("PRODICOD")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        [Required]
        public string PRODA60DESCR { get; set; }  
        public int? GRUPICOD { get; set; }
        [NotMapped]
        public string CEMP { get; set; }        
    }
}
