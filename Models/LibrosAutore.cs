using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDXimenaCastanedaEF.Models
{
    public partial class LibrosAutore
    {
        [Key] 
        public int Id { get; set; } 

        public int IdAutor { get; set; }

        public string Isbn { get; set; } = null!;

        public virtual Autore? IdAutorNavigation { get; set; } 

        public virtual Libro? IsbnNavigation { get; set; } 
    }
}
