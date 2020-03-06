using System;
using System.Collections.Generic;

namespace LibrarySystem.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Rental = new HashSet<Rental>();
        }

        public Guid Id { get; set; }
        public string FullName { get; set; }

        public virtual ICollection<Rental> Rental { get; set; }
    }
}
