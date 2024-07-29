using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Bookmark
    {
        public string MemberId { get; set; } = ""; 

        public int EntryId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public AppUser? Member { get; set; }

        [ForeignKey(nameof(EntryId))]
        public Entry? Entry { get; set; }

        // controller'da kişi kendi post'unu bookmarka ekleyemesin

    }
}
