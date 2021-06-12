using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TesteCostumerX.Models
{
    public class Cliente
    {
        [Key]
        public int ID_Cliente { get; set; }

        [Display(Name = "Nome do Cliente")]
        [StringLength(255, MinimumLength = 2)]
        [Required(ErrorMessage = "Por favor, informe o nome do Cliente")]
        public string NM_Cliente { get; set; }

        [Display(Name = "Email")]        
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DT_Cad { get; set; }

        [Display(Name = "Contatos")]
        public List<Contato> Contatos_Cliente { get; set; }

        public Cliente()
        {
            ID_Cliente = 0;
            NM_Cliente = "";
            Email = "";
            Telefone = "";
            DT_Cad = DateTime.Now;
        }
    }
}
