namespace Models.SIS
{
    using Infra.Base.Interface;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Table("USUARIO")]
    public partial class SIS_USUARIO : IEntidadeBase
    {
        [Key]
        [Column("USUAICOD")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        [Required]
        [Column("USUAA60LOGIN")]
        public string NOME { get; set; }
        [Required]
        [Column("USUAA5SENHA")]
        public string PWD { get; set; }
        [NotMapped]
        public string ADMIN { get; set; }
        [NotMapped]
        public virtual dynamic Empresas { get; set; }
        [NotMapped]
        public virtual dynamic Grupos { get; set; }        
        [NotMapped]
        public string CEMP { get; set; }
    }
}
