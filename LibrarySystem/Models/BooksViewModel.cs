using LibrarySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class BookViewModel
    {
        public Book Book { get; set; }

        public bool IsAvailable { get; set; }
    }
}
