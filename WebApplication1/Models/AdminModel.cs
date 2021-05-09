using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class AdminModel
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
