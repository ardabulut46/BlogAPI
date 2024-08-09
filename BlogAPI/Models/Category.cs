using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Category
    {

        public int Id { get; set; }

        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; } = "";

        public int EntryId { get; set; }

        [ForeignKey(nameof(EntryId))]
        public Entry? Entry { get; set; }



    }
}
