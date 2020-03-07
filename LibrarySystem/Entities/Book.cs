using LibrarySystem.Entities.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Entities
{
    public partial class Book
    {
        public Book()
        {
            Rental = new HashSet<Rental>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [DisplayName("ISBN")]
        [Required]
        public string Isbn { get; set; }

        public virtual ICollection<Rental> Rental { get; set; }
    }
}
