using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models
{
    public class AuthorModel
    { 
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
       
    }
}
