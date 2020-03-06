using System;
using System.Collections.Generic;

namespace LibrarySystem.Entities
{
    public partial class Rental
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid CustomerId { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
