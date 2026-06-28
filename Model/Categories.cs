using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Model
{
    public class Categorise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategores { get; set; }
        public List<Product> Products { get; set; }
        public string NameCategories { get; set; }
        public string ImageCategories { get; set; }
    }
}
