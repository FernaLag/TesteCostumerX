using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TesteCostumerX.Models
{
    public class Contato
    {
        [Key]
        public int ID_Contato { get; set; }

        [Display(Name = "Nome do Contato")]
        [StringLength(255, MinimumLength = 2)]
        [Required(ErrorMessage = "Informe o nome do Contato")]
        public string NM_Contato { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Informe um Email para Contato")]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Informe um Telefone para Contato")]
        public string Telefone { get; set; }

        [Display(Name = "Cliente Vinculado")]
        [Required(ErrorMessage = "Informe qual Cliente esse contato representa")]
        public int FK_Cliente { get; set; }

        [ForeignKey("clienteForeignKey")]
        public Cliente Cliente { get; set; }

        public Contato()
        {
            ID_Contato = 0;
            NM_Contato = "";
            Email = "";
            Telefone = "";
            FK_Cliente = 0;
        }
    }
}
