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

    public class CAD_PESSOA : IEntidadeBase
    {
        [Key]
        [Column("COD_CADPESSOA")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        [Required]
        public string NOME { get; set; }
        [Required]
        public string DOCUMENTO { get; set; }
        public string CEMP { get; set; }
        public string TIPO { get; set; }
        public string ATIVO { get; set; }
        public string OBS { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Autorização")]
        public DateTime DATA_AUTORIZACAO { get; set; }
        public string OBS_AUTORIZACAO { get; set; }
        public int? DIAS_AVISO { get; set; }

    }
}
