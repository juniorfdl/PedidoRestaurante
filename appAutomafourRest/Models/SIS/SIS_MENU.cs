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

    public partial class SIS_MENU : IEntidadeBase
    {
        [Key]
        [Column("CODIGOSISMENU")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        [Required]
        public int GRUPO { get; set; }
        [StringLength(70)]
        [Display(Name = "Descrição")]
        public string DESCRICAO { get; set; }

        [StringLength(70)]
        [Display(Name = "DLL")]
        public string DLL { get; set; }

        [StringLength(70)]
        [Display(Name = "Menu")]
        public string MENU { get; set; }

        [StringLength(1)]
        [Display(Name = "Multi Empresa")]
        public string EMP { get; set; }

        [NotMapped]
        public string CEMP { get; set; }

        [StringLength(1)]
        [Required]
        [Display(Name = "Grava Log")]
        public string GRAVA_LOG { get; set; }

        [StringLength(1)]
        [Display(Name = "Programa Auxiliar")]
        public string PROGRAMA_AUXILIAR { get; set; }

        [StringLength(100)]
        [Display(Name = "Caminho do Banco")]
        public string CAMINHO_BANCO { get; set; }

        [Required]
        public int ORDEM { get; set; }
    }
}
