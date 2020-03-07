using LibrarySystem.Entities.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Rental = new HashSet<Rental>();
        }

        [Key]
        [DisplayName("Number")]
        public Guid Id { get; set; }
        
        [DisplayName("Name")]
        [Required]
        public string FullName { get; set; }

        public virtual ICollection<Rental> Rental { get; set; }
    }
}
