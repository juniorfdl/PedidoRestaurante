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

    [Table("EMPRESA")]
    public class CAD_EMPRESA : IEntidadeBase
    {
        [Key]
        [Column("EMPRICOD")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        [Required]
        [Column("EMPRA60RAZAOSOC")]
        public string NOME { get; set; }
        [Required]
        [Column("EMPRA60NOMEFANT")]
        public string FANTASIA { get; set; }
        [NotMapped]
        public string CEMP { get; set; }        
    }
}
