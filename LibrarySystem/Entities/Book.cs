using System;
using System.Collections.Generic;

namespace LibrarySystem.Entities
{
    public partial class Book
    {
        public Book()
        {
            Rental = new HashSet<Rental>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }

        public virtual ICollection<Rental> Rental { get; set; }
    }
}
