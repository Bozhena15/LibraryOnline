using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class BookModel
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual AuthorModel Author { get; set; }
        public virtual GenreBookModel GenreBook { get; set; }
        public int YearOfPublic { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
    }
}
