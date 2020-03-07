using LibrarySystem.Entities.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Entities
{
    public partial class Rental
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Date from")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        
        [DisplayName("Date to")]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate")]
        public DateTime? EndDate { get; set; }
        
        [DisplayName("Customer")]
        public Guid CustomerId { get; set; }
        
        [DisplayName("Book")]
        public int BookId { get; set; }
        
        [DisplayName("Book")]
        
        public virtual Book Book { get; set; }
        
        [DisplayName("Customer")]
        public virtual Customer Customer { get; set; }
    }
}
